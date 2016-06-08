using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientDemo
{
    class Department
    {
        public string coding;   //"330106001" 
        public string domainId; //"330106" 
        public string id;       //"1" 
        public string name;     //"root"
        public string type;     //"...."

        public List<Department> dChilds;
        public List<Device> devices;

        public Department()
        {
            dChilds = new List<Department>();
            devices = new List<Device>();
        }

        public void SetDeviceStatus(string s)
        {
        }
     }

    class Device
    {
        public string status;

        public int alert;
        public int alertout;
        public int channel;
        public string coding;
        public string desc;
        public string domainId;
        public string id;
        public string ip;
        public string manufacturer;
        public string model;
        public string password;
        public string port;
        public string rights;
        public string title;
        public string type;
        public string typeid;
        public string user;

        public List<Channel> channels;
        public List<Alert> alerts;
        public List<Alarmout> alarmouts;

        public CuRequestVideoRsp requestVideoRsp;
        public CuStopVideoRsp stopVideoRsp;

        public Device()
        {
            channels = new List<Channel>();
            alerts = new List<Alert>();
            alarmouts = new List<Alarmout>();
            requestVideoRsp = new CuRequestVideoRsp();
            stopVideoRsp = new CuStopVideoRsp();
        }
    }
    
    class Channel
    {
        public string camera;
        public string id;
        public string num;
        public string title;
        public string type;
    }

    class Alert
    {
    }

    class Alarmout
    {
        public string alertdev;
        public string alerttype;
        public string num;
        public string title;
    }
}
