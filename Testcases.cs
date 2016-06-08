
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Security.Cryptography;

namespace ClientDemo
{
    class TestCases
    {
        static private RequestFactory rf = null;
        static private string serverIp = "180.125.162.150";
        static private string serverPort = "9000";
        static private string username = "3";
        static private string password = "3";

        static private string version="1.0.0.0";
        static private int sequence = 1;
        static private string unit = "OP_CLIENT";


        static public CuLoginRsp loginRsp = new CuLoginRsp();
        static public CuGetUserGroupsRsp getUserGroupsRsp = new CuGetUserGroupsRsp();
        static public CuGetFileRsp getFileRsp = new CuGetFileRsp();
        static public CuHeartbeatRsp heartbeatRsp = new CuHeartbeatRsp();

        private MainForm mainform = null;

        public TestCases(MainForm mf)
        {
            mainform = mf;
            rf = new RequestFactory(serverIp, serverPort);
        }

        public void LoginAndGetDevices()
        {
            try
            {
                PostCuLogin();
                GetCuGetUserGroups();
                GetCuGetFile();
                PostGetDeviceStatus(getFileRsp.deviceTree);
                mainform.RefreshTree();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        public void RequestAndStopVideo()
        {
            try
            {
                PostRequestVideo(getFileRsp.deviceTree);
                PostHeartbeat();
                PostStopVideo(getFileRsp.deviceTree);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        public void TcAllResquests()
        {
            try
            {
                PostCuLogin();
                GetCuGetUserGroups();
                GetCuGetFile();
                PostGetDeviceStatus(getFileRsp.deviceTree);
                mainform.RefreshTree();
                PostRequestVideo(getFileRsp.deviceTree);
                PostHeartbeat();
                PostStopVideo(getFileRsp.deviceTree);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        #region Get

        private void GetCuGetUserGroups()
        {
            try
            {
                // create request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rf.cuGetUserGroups(loginRsp.id));

                // If required by the server, set the credentials.
                request.Method = "GET";
                request.ContentLength = 0;
                request.ContentType = "application/octet-stream";
                request.Headers.Add("Data_Param",
                    new DataParam(version, "cuGetUserGroups", loginRsp.session, sequence++, unit, loginRsp.id).GetString());
                //request.CookieContainer = cookie;
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine("cuGetUserGroups:\t" + (int)response.StatusCode + " " + response.StatusDescription);

                //response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                string retString = myStreamReader.ReadToEnd();
                Console.WriteLine(retString);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    DataParam dp = new DataParam(response.GetResponseHeader("Data_Param"));

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(retString);
                    XmlNode xn = xmldoc.SelectSingleNode("body").FirstChild;
                    XmlElement xe = (XmlElement)xn;
                    getUserGroupsRsp.groupid = xe.InnerText;
                }

                myStreamReader.Close();
                myResponseStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        private void GetCuGetFile()
        {
            try
            {
                // create request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rf.cuGetFile(loginRsp.id, "1", getUserGroupsRsp.groupid));

                // If required by the server, set the credentials.
                request.Method = "GET";
                request.ContentLength = 0; // contentStr.Length;
                request.ContentType = "application/octet-stream";
                request.Headers.Add("Data_Param",
                    new DataParam(version, "cuGetFile", loginRsp.session, sequence++, unit, loginRsp.id).GetString());
                //request.CookieContainer = cookie;
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine("cuGetFile:\t" + (int)response.StatusCode + " " + response.StatusDescription);

                //response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                string retString = myStreamReader.ReadToEnd();
                Console.WriteLine(retString);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    DataParam dp = new DataParam(response.GetResponseHeader("Data_Param"));

                    getFileRsp = new CuGetFileRsp(retString);
                }

                myStreamReader.Close();
                myResponseStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        #endregion Get


        #region Post

        private void PostCuLogin()
        {
            try
            {
                // create request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rf.cuLogin());

                String postDataStr = "id=" + "0" + "&username=" + username + "&password=" + password;

                // If required by the server, set the credentials.
                request.Method = "POST";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                request.ContentType = "application/octet-stream";
                request.Headers.Add("Data_Param",
                    new DataParam(version, "cuLogin", loginRsp.session, sequence++, unit, "0").GetString());
                //request.CookieContainer = cookie;
                request.Credentials = CredentialCache.DefaultCredentials;

                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine("cuLogin:\t" + (int)response.StatusCode + " " + response.StatusDescription);

                //response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                string retString = myStreamReader.ReadToEnd();
                Console.WriteLine(retString);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    DataParam dp = new DataParam(response.GetResponseHeader("Data_Param"));
                    loginRsp.session = dp.session;

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(retString);
                    XmlNode xn = xmldoc.SelectSingleNode("body");
                    XmlElement xe = (XmlElement)xn;
                    loginRsp.sessionkey = xe.GetAttribute("sessionkey");
                    loginRsp.time = xe.GetAttribute("time");
                    loginRsp.id = xe.GetAttribute("id");
                    loginRsp.expires = xe.GetAttribute("expires");
                    loginRsp.addr = xe.GetAttribute("addr");
                }

                myStreamReader.Close();
                myResponseStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }

        }

        private void PostGetDeviceStatus(Department d)
        {
            try
            {
                foreach (Department dept in d.dChilds)
                {
                    PostGetDeviceStatus(dept);
                }

                foreach (Device device in d.devices)
                {
                    // create request
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rf.cuGetDeviceStatus());

                    String postDataStr = "type=" + "1" + "&id=" + device.id;

                    // If required by the server, set the credentials.
                    request.Method = "POST";
                    request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                    request.ContentType = "application/octet-stream";
                    request.Headers.Add("Data_Param",
                        new DataParam(version, "cuGetDeviceStatus", loginRsp.session, sequence++, unit, loginRsp.id).GetString());
                    //request.CookieContainer = cookie;
                    request.Credentials = CredentialCache.DefaultCredentials;

                    Stream myRequestStream = request.GetRequestStream();
                    StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                    myStreamWriter.Write(postDataStr);
                    myStreamWriter.Close();

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Console.WriteLine("GetDeviceStatus:\t" + (int)response.StatusCode + " " + response.StatusDescription);

                    //response.Cookies = cookie.GetCookies(response.ResponseUri);
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                    string retString = myStreamReader.ReadToEnd();
                    Console.WriteLine(retString);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        DataParam dp = new DataParam(response.GetResponseHeader("Data_Param"));

                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(retString);
                        XmlNode xn = xmldoc.SelectSingleNode("body");
                        XmlElement xe = (XmlElement)xn;
                        device.status = xe.GetAttribute("status");
                    }

                    myStreamReader.Close();
                    myResponseStream.Close();
                    response.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        private void PostRequestVideo(Department d)
        {
            try
            {
                foreach (Department dept in d.dChilds)
                {
                    PostRequestVideo(dept);
                }

                foreach (Device device in d.devices)
                {
                    if (device.status != "1")
                    {
                        continue;
                    }

                    // create request
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rf.cuRequestVideo());

                    String postDataStr = "cameraid=" + device.id + "$0" + "&type=" + "1" + "&streamtype=" + "2";

                    // If required by the server, set the credentials.
                    request.Method = "POST";
                    request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                    request.ContentType = "application/octet-stream";
                    request.Headers.Add("Data_Param",
                        new DataParam(version, "cuRequestVideo", loginRsp.session, sequence++, unit, loginRsp.id).GetString());
                    //request.CookieContainer = cookie;
                    request.Credentials = CredentialCache.DefaultCredentials;

                    Stream myRequestStream = request.GetRequestStream();
                    StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                    myStreamWriter.Write(postDataStr);
                    myStreamWriter.Close();

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Console.WriteLine("cuRequestVideo:\t" + (int)response.StatusCode + " " + response.StatusDescription);

                    //response.Cookies = cookie.GetCookies(response.ResponseUri);
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                    string retString = myStreamReader.ReadToEnd();
                    Console.WriteLine(retString);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        DataParam dp = new DataParam(response.GetResponseHeader("Data_Param"));

                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(retString);
                        XmlNode xn = xmldoc.SelectSingleNode("body");
                        XmlElement xe = (XmlElement)xn;
                        device.requestVideoRsp.ip = xe.GetAttribute("ip");
                        device.requestVideoRsp.port = xe.GetAttribute("port");
                        device.requestVideoRsp.rate = xe.GetAttribute("rate");
                        device.requestVideoRsp.type = xe.GetAttribute("type");
                        device.requestVideoRsp.stum = xe.GetAttribute("stum");
                        device.requestVideoRsp.protocol = xe.GetAttribute("protocol");
                        device.requestVideoRsp.stumport = xe.GetAttribute("stumport");
                        device.requestVideoRsp.model = xe.GetAttribute("model");
                        device.requestVideoRsp.manufacturer = xe.GetAttribute("manufacturer");
                        device.requestVideoRsp.session = xe.GetAttribute("session");
                        device.requestVideoRsp.token = xe.GetAttribute("token");
                        device.requestVideoRsp.url = xe.GetAttribute("url");
                        device.requestVideoRsp.username = xe.GetAttribute("username");
                        device.requestVideoRsp.password = xe.GetAttribute("password");
                    }

                    myStreamReader.Close();
                    myResponseStream.Close();
                    response.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        private void PostRequestVideo(Device device)
        {
            try
            {
                foreach (Channel ch in device.channels)
                {
                    if (device.status != "1")
                    {
                        return;
                    }

                    // create request
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rf.cuRequestVideo());

                    String postDataStr = "cameraid=" + device.id + "$" + ch.num + "&type=" + "1" + "&streamtype=" + "2";

                    // If required by the server, set the credentials.
                    request.Method = "POST";
                    request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                    request.ContentType = "application/octet-stream";
                    request.Headers.Add("Data_Param",
                        new DataParam(version, "cuRequestVideo", loginRsp.session, sequence++, unit, loginRsp.id).GetString());
                    //request.CookieContainer = cookie;
                    request.Credentials = CredentialCache.DefaultCredentials;

                    Stream myRequestStream = request.GetRequestStream();
                    StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                    myStreamWriter.Write(postDataStr);
                    myStreamWriter.Close();

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Console.WriteLine("cuRequestVideo:\t" + (int)response.StatusCode + " " + response.StatusDescription);

                    //response.Cookies = cookie.GetCookies(response.ResponseUri);
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                    string retString = myStreamReader.ReadToEnd();
                    Console.WriteLine(retString);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        DataParam dp = new DataParam(response.GetResponseHeader("Data_Param"));

                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(retString);
                        XmlNode xn = xmldoc.SelectSingleNode("body");
                        XmlElement xe = (XmlElement)xn;
                        device.requestVideoRsp.ip = xe.GetAttribute("ip");
                        device.requestVideoRsp.port = xe.GetAttribute("port");
                        device.requestVideoRsp.rate = xe.GetAttribute("rate");
                        device.requestVideoRsp.type = xe.GetAttribute("type");
                        device.requestVideoRsp.stum = xe.GetAttribute("stum");
                        device.requestVideoRsp.protocol = xe.GetAttribute("protocol");
                        device.requestVideoRsp.stumport = xe.GetAttribute("stumport");
                        device.requestVideoRsp.model = xe.GetAttribute("model");
                        device.requestVideoRsp.manufacturer = xe.GetAttribute("manufacturer");
                        device.requestVideoRsp.session = xe.GetAttribute("session");
                        device.requestVideoRsp.token = xe.GetAttribute("token");
                        device.requestVideoRsp.url = xe.GetAttribute("url");
                        device.requestVideoRsp.username = xe.GetAttribute("username");
                        device.requestVideoRsp.password = xe.GetAttribute("password");
                    }

                    myStreamReader.Close();
                    myResponseStream.Close();
                    response.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        private void PostHeartbeat()
        {
            try
            {
                // create request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rf.cuHeartbeat());

                String postDataStr = "id=" + loginRsp.id + "&expires=" + "60";

                // If required by the server, set the credentials.
                request.Method = "POST";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                request.ContentType = "application/octet-stream";
                request.Headers.Add("Data_Param",
                    new DataParam(version, "cuHeartbeat", loginRsp.session, sequence++, unit, loginRsp.id).GetString());
                //request.CookieContainer = cookie;
                request.Credentials = CredentialCache.DefaultCredentials;

                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine("cuHeartbeat:\t" + (int)response.StatusCode + " " + response.StatusDescription);

                //response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                string retString = myStreamReader.ReadToEnd();
                Console.WriteLine(retString);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    DataParam dp = new DataParam(response.GetResponseHeader("Data_Param"));

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(retString);
                    XmlNode xn = xmldoc.SelectSingleNode("body");
                    XmlElement xe = (XmlElement)xn;
                    heartbeatRsp.unit = xe.GetAttribute("unit");
                    heartbeatRsp.time = xe.GetAttribute("time");
                    heartbeatRsp.expires = xe.GetAttribute("expires");
                }

                myStreamReader.Close();
                myResponseStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        private void PostStopVideo(Department d)
        {
            try
            {
                foreach (Department dept in d.dChilds)
                {
                    PostStopVideo(dept);
                }

                foreach (Device device in d.devices)
                {
                    if (device.status != "1")
                    {
                        continue;
                    }

                    // create request
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rf.cuRequestVideo());

                    String postDataStr = "session=" + device.requestVideoRsp.session + "&useless=" + "";

                    // If required by the server, set the credentials.
                    request.Method = "POST";
                    request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                    request.ContentType = "application/octet-stream";
                    request.Headers.Add("Data_Param",
                        new DataParam(version, "cuStopVideo", loginRsp.session, sequence++, unit, loginRsp.id).GetString());
                    //request.CookieContainer = cookie;
                    request.Credentials = CredentialCache.DefaultCredentials;

                    Stream myRequestStream = request.GetRequestStream();
                    StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                    myStreamWriter.Write(postDataStr);
                    myStreamWriter.Close();

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Console.WriteLine("cuStopVideo:\t" + (int)response.StatusCode + " " + response.StatusDescription);

                    //response.Cookies = cookie.GetCookies(response.ResponseUri);
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
                    string retString = myStreamReader.ReadToEnd();
                    Console.WriteLine(retString);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        DataParam dp = new DataParam(response.GetResponseHeader("Data_Param"));

                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(retString);
                        XmlNode xn = xmldoc.SelectSingleNode("body");
                        XmlElement xe = (XmlElement)xn;
                        device.stopVideoRsp.useless = xe.GetAttribute("useless");
                    }

                    myStreamReader.Close();
                    myResponseStream.Close();
                    response.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

       #endregion Post
    }
}
