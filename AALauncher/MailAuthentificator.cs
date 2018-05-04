using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace AALauncher
{
    public class MailAuthentificator
    {
        private string domain;
        private string login;
        private string password;


        public MailAuthentificator(string email, string password)
        {
            if (email.Length == 0)
            {
                MessageBox.Show("please input your email", "Account error");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("password can not be blank", "Account error");
                return;
            }
                

            this.password = password;

            string[] tokens = email.Split('@');
            if (tokens.Length <= 1)
            {
                throw new Exception("Invalid email adddress");
            }
            else
            {
                if (!string.IsNullOrEmpty(tokens[0]))
                    login = tokens[0];
                else
                    throw new Exception("Invalid email adddress");

                if (!string.IsNullOrEmpty(tokens[1]))
                    domain = tokens[1];
                else
                    throw new Exception("Invalid email adddress");
            }
        }

        public event EventHandler<AuthorizationCompletedEventArgs> Completed;

        protected virtual void OnCompleted(AuthorizationCompletedEventArgs e)
        {
            EventHandler<AuthorizationCompletedEventArgs> handler = Completed;
            if (handler != null) handler(this, e);
        }

        public void StartAuthAsync()
        {
            ThreadPool.QueueUserWorkItem(StartAuthInternal);
        }

        private void StartAuthInternal(object nothing)
        {
            HttpWebResponse response;

            string cookie = null;
            string uid = null;
            string token = null;

            try
            {
                if (TryAuthorization(login, domain, password, out response))
                {
                    response.Close();

                    if (response.Headers != null)
                    {
                        string tmp = response.Headers["Set-Cookie"];
                        if (string.IsNullOrEmpty(tmp))
                            throw new Exception("Authorization failed!");

                        //mpop=fffff;
                        int startpop = tmp.IndexOf('=');
                     
                        if (startpop <= 0)
                        {
                            throw new Exception("Invalid cookie");
                        }
                        else
                        {
                            ++startpop;
                            int endpop = tmp.IndexOf(';');
                            if (endpop <= 0)
                            {
                                throw new Exception("Invalid cookie");
                            }
                            else
                            {
                                cookie = tmp.Substring(startpop, endpop - startpop);


                                string xml = RequestKey(cookie);
                                var xdoc = new XmlDocument();
                                xdoc.LoadXml(xml);
                                var node = xdoc.SelectSingleNode("//AutoLogin");
                                if (node != null)
                                {
                                    if (node.Attributes != null)
                                    {
                                        uid = node.Attributes["PersId"].Value;
                                        token = node.Attributes["Key"].Value;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Authorization failed!");
                }
            }
            catch (Exception e)
            {
                OnCompleted(new AuthorizationCompletedEventArgs(e, uid, token, cookie));
            }
            finally
            {
                OnCompleted(new AuthorizationCompletedEventArgs(null, uid, token, cookie));
            }
        }


        private static bool TryAuthorization(string login, string domain, string password,
                                                 out HttpWebResponse response)
        {
            response = null;

            try
            {
                var request = (HttpWebRequest) WebRequest.Create("http://auth.yanlongli.com");

                request.UserAgent = "Opera/9.80 (Windows NT 6.2; WOW64) Presto/2.12.388 Version/12.16";
                request.Accept =
                    "text/html, application/xml;q=0.9, application/xhtml+xml, image/png, image/webp, image/jpeg, image/gif, image/x-xbitmap, */*;q=0.1";
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.9,en;q=0.8");
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Referer = "http://blog.yanlongli.com/";
                request.ServicePoint.Expect100Continue = false;

                using (var post = new WebFormPost(request))
                {
                    post.AddData("Domain", domain);
                    post.AddData("Login", login);
                    post.AddData("Password", password);
                }

                response = (HttpWebResponse) request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse) e.Response;
                else return false;
            }
            catch (Exception)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;
        }


        private static string RequestKey(string cookie)
        {
            HttpWebResponse response;

            if (Request_authdl_mail_ru(cookie, out response))
            {
                string xml = HttpHelper.ReadStringContent(response, Encoding.UTF8);

                response.Close();

                return xml;
            }

            return null;
        }

        private static bool Request_authdl_mail_ru(string cookie, out HttpWebResponse response)
        {
            response = null;

            try
            {
                var request = (HttpWebRequest) WebRequest.Create("https://authdl.mail.ru/sz.php?hint=AutoLogin");

                request.Accept = "*/*";
                request.UserAgent = "Downloader/3780";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body =
                    string.Format(
                        "<?xml version=\"1.0\" encoding=\"UTF-8\"?><AutoLogin ProjectId=\"3001\" SubProjectId=\"0\" ShardId=\"0\" Mpop=\"{0}\"/>",
                        cookie);
                byte[] postBytes = Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                }

                response = (HttpWebResponse) request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse) e.Response;
                else return false;
            }
            catch (Exception)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;
        }
    }

    public class AuthorizationCompletedEventArgs : EventArgs
    {
        private readonly string cookie;
        private readonly Exception exception;
        private readonly string token;
        private readonly string uid;

        public AuthorizationCompletedEventArgs(Exception exception, string uid, string token, string cookie)
        {
            this.exception = exception;
            this.uid = uid;
            this.token = token;
            this.cookie = cookie;
        }

        public string Uid
        {
            get
            {
                if (exception != null)
                    throw exception;
                return uid;
            }
        }

        public string Token
        {
            get
            {
                if (exception != null)
                    throw exception;
                return token;
            }
        }


        public string Cookie
        {
            get
            {
                if (exception != null)
                    throw exception;
                return cookie;
            }
        }

        public Exception Exception
        {
            get { return exception; }
        }
    }
}