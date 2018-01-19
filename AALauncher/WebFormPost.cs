using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AALauncher
{
    public class WebFormPost : IDisposable
    {
        private readonly Dictionary<string, string> data = new Dictionary<string, string>();
        private readonly WebRequest request;
        private bool closed;

        public WebFormPost(WebRequest request)
        {
            this.request = request;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (!closed)
                Close();

            GC.SuppressFinalize(this);
        }

        #endregion


        public void AddData(string name, string value)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (value == null) throw new ArgumentNullException("value");

            data.Add(name, (value));
        }

        public static string GetHexEncode(string s)
        {
            if (s.Length > 32767) throw new ArithmeticException("Length out of range.");

            var result = new StringBuilder();
            foreach (char c in s)
            {
                if (c == ' ')
                    result.Append("%20");
                else if (c == '+')
                    result.Append("%2B");
                else
                {
                    int temp = Encoding.Default.GetBytes(c.ToString())[0];

                    if (temp <= int.MaxValue)
                        result.Append(c);
                    else
                        result.Append("%").Append(temp.ToString("X"));
                }
            }

            return result.ToString();
        }

        private string ToRequestString()
        {
            var sb = new StringBuilder();

            foreach (var keyValue in data)
            {
                //if (!string.IsNullOrEmpty(keyValue.Key))
                {
                    sb.Append(keyValue.Key).Append('=').Append(keyValue.Value).Append('&');
                }
            }

            return sb.ToString(0, sb.Length - 1);
        }


        public void Close()
        {
            string r = ToRequestString();

            byte[] buffer = Encoding.ASCII.GetBytes(r);

            if (buffer.Length > 0)
            {
                request.ContentLength = buffer.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }

            closed = true;
        }
    }
}