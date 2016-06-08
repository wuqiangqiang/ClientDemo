using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientDemo
{
    class DataParam
    {
        public string version;
        public string operation;
        public string session;
        public string sequence;
        public string unit;
        public string id;


        public DataParam(string str)
        {
            int start = str.IndexOf("=");
            int end = str.IndexOf("&");
            version = str.Substring(start + 1, end - start - 1);

            str = str.Substring(end + 1, str.Length - end - 1);
            start = str.IndexOf("=");
            end = str.IndexOf("&");
            operation = str.Substring(start + 1, end - start - 1);

            str = str.Substring(end + 1, str.Length - end - 1);
            start = str.IndexOf("=");
            end = str.IndexOf("&");
            session = str.Substring(start + 1, end - start - 1);

            str = str.Substring(end + 1, str.Length - end - 1);
            start = str.IndexOf("=");
            end = str.IndexOf("&");
            sequence = str.Substring(start + 1, end - start - 1);

            str = str.Substring(end + 1, str.Length - end - 1);
            start = str.IndexOf("=");
            end = str.IndexOf("&");
            unit = str.Substring(start + 1, end - start - 1);

            str = str.Substring(end + 1, str.Length - end - 1);
            start = str.IndexOf("=");
            end = str.Length;
            id = str.Substring(start + 1, end - start - 1);
        }

        public DataParam(string ver, string op, string ses, int seq, string uni, string identify)
        {
            version = ver;
            operation = op;
            session = ses;
            sequence = seq.ToString();
            unit = uni;
            id = identify;
        }

        public string GetString()
        {
            return "version=" + version + 
                   "&operation=" + operation + 
                   "&session=" + session +
                   "&sequence=" + sequence +
                   "&unit=" + unit + 
                   "&id=" + id;
        }
    }
}
