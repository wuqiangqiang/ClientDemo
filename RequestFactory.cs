using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Security.Cryptography;

namespace ClientDemo
{
    class RequestFactory
    {
        private string url = "";

        public RequestFactory(string serverIp, string serverPort)
        {
            url = "http://" + serverIp + ":" + serverPort;
        }

        // post
        public string cuLogin()
        {
            return url + "/cuLogin";
        }

        // get
        public string cuGetUserGroups(string id)
        {
            return url + "/cuGetUserGroups?id=" + id;
        }

        // get
        public string cuGetFile(string id, string type, string filename)
        {
            return url + "/cuGetFile?id=" + id + "&type=" + type + "&filename=" + filename;
        }

        // post
        public string cuGetDeviceStatus()
        {
            return url + "/cuGetDeviceStatus";
        }

        // post
        public string cuRequestVideo()
        {
            return url + "/cuRequestVideo";
        }

        // post
        public string cuHeartbeat()
        {
            return url + "/cuHeartbeat";
        }

        // post
        public string cuStopVideo()
        {
            return url + "/cuStopVideo";
        }

        private static string ToHex(byte[] me)
        {
            string result = "";
            for (int i = 0; i < me.Length; i++)
            {
                result += me[i].ToString("X2");
            }
            return result;
        }

    }
}
