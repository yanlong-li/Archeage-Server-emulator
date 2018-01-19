using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Web;

namespace AALauncher
{
    public static class HttpHelper
    {
        public static Stream GetStream(HttpWebResponse responce)
        {
            if (responce == null) throw new ArgumentNullException("responce");

            Stream streamToRead = responce.GetResponseStream();

            if (responce.ContentEncoding.IndexOf("gzip", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (streamToRead != null)
                    streamToRead = new GZipStream(streamToRead, CompressionMode.Decompress);
            }
            else if (responce.ContentEncoding.IndexOf("deflate", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (streamToRead != null)
                    streamToRead = new DeflateStream(streamToRead, CompressionMode.Decompress);
            }

            return streamToRead;
        }

        public static string ReadStringContent(HttpWebResponse responce, Encoding encoding, bool htmlDecode = true)
        {
            using (Stream streamToRead = GetStream(responce))
            {
                if (streamToRead != null)
                {
                    using (var streamReader = new StreamReader(streamToRead, encoding))
                    {
                        string text = streamReader.ReadToEnd();

                        if (htmlDecode)
                        {
                            if (!String.IsNullOrEmpty(text))
                            {
                                return HttpUtility.HtmlDecode(text);
                            }
                        }

                        return text;
                    }
                }
            }
            return null;
        }

        public static byte[] ReadData(HttpWebResponse responce)
        {
            using (Stream streamToRead = GetStream(responce))
            {
                if (streamToRead != null)
                {
                    List<byte> temp;

                    if (streamToRead.CanSeek)
                    {
                        temp = new List<byte>((int) streamToRead.Length);
                    }
                    else
                    {
                        temp = new List<byte>(16000);
                    }


                    int bt;
                    while ((bt = streamToRead.ReadByte()) != -1)
                    {
                        temp.Add((byte) bt);
                    }

                    return temp.ToArray();
                }
            }

            return null;
        }
    }
}