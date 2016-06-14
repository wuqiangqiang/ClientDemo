using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ClientDemo
{
    public class CuLoginRsp
    {
        public string session;

        public string sessionkey;
        public string time;
        public string id;
        public string expires;
        public string addr;
    }

    public class CuGetUserGroupsRsp
    {
        public string groupid;
    }

    public class CuGetFileRsp
    {
        public string xml;
        public Department deviceTree = new Department();

        public CuGetFileRsp()
        {
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load("D:\\devxml.txt");
            //XmlNode xeRoot = xmldoc.SelectSingleNode("Organization").FirstChild;

            //ParseDepartment(xeRoot, ref deviceTree);
        }

        public CuGetFileRsp(string xmlstr)
        {
            xml = xmlstr;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            XmlNode xeRoot = xmldoc.SelectSingleNode("Organization").FirstChild;

            ParseDepartment(xeRoot, ref deviceTree);
        }

        private void ParseDepartment(XmlNode xeRoot, ref Department dn)
        {
            XmlElement xe = (XmlElement)xeRoot;
            if (xe.Name == "Department")
            {
                dn.coding = xe.GetAttribute("coding");
                dn.domainId = xe.GetAttribute("domainId");
                dn.id = xe.GetAttribute("id");
                dn.name = xe.GetAttribute("name");
                dn.type = xe.GetAttribute("type");
            }

            // 遍历
            foreach (XmlNode xn in xeRoot.ChildNodes)
            {
                xe = (XmlElement)xn;
                if (xe.Name == "Device")
                {
                    Device device = new Device();
                    device.alert = int.Parse(xe.GetAttribute("alert"));
                    device.alertout = int.Parse(xe.GetAttribute("alertout"));
                    device.channel = int.Parse(xe.GetAttribute("channel"));
                    device.coding = xe.GetAttribute("coding");
                    device.desc = xe.GetAttribute("desc");
                    device.domainId = xe.GetAttribute("domainId");
                    device.id = xe.GetAttribute("id");
                    device.ip = xe.GetAttribute("ip");
                    device.manufacturer = xe.GetAttribute("manufacturer");
                    device.model = xe.GetAttribute("model");
                    device.password = xe.GetAttribute("password");
                    device.port = xe.GetAttribute("port");
                    device.rights = xe.GetAttribute("rights");
                    device.title = xe.GetAttribute("title");
                    device.type = xe.GetAttribute("type");
                    device.typeid = xe.GetAttribute("typeid");
                    device.user = xe.GetAttribute("user");

                    foreach (XmlNode xnChild in xe.ChildNodes)
                    {
                        xe = (XmlElement)xnChild;
                        if (xe.Name == "Channel")
                        {
                            Channel ch = new Channel();
                            ch.camera = xe.GetAttribute("camera");
                            ch.id = xe.GetAttribute("id");
                            ch.num = xe.GetAttribute("num");
                            ch.title = xe.GetAttribute("title");
                            ch.type = xe.GetAttribute("type");
                            device.channels.Add(ch);
                        }
                        else if (xe.Name == "Alarmout")
                        {
                            Alarmout ao = new Alarmout();
                            ao.alertdev = xe.GetAttribute("alertdev");
                            ao.alerttype = xe.GetAttribute("alerttype");
                            ao.num = xe.GetAttribute("num");
                            ao.title = xe.GetAttribute("title");
                            device.alarmouts.Add(ao);
                        }
                        else if (xe.Name == "Alert")
                        {
                        }
                        else
                        {
                            //未知节点
                        }
                    }
                    dn.devices.Add(device);
                }
                else if (xe.Name == "Department")
                {
                    Department dChild = new Department();
                    ParseDepartment(xn, ref dChild);
                    dn.dChilds.Add(dChild);
                }
                else
                {
                    // 未知节点
                }
            }
        }
    }

    public class CuRequestVideoRsp
    {
        public string ip;
        public string port;
        public string rate;
        public string type;
        public string stum;
        public string protocol;
        public string stumport;
        public string model;
        public string manufacturer;
        public string session;
        public string token;
        public string url;
        public string username;
        public string password;
    }

    public class CuHeartbeatRsp
    {
        public string unit;
        public string time;
        public string expires;
    }

    public class CuStopVideoRsp
    {
        public string useless;
    }
}
