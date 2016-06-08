using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Security.Cryptography;

namespace ClientDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            try
            {
                TestCases tc = new TestCases(this);

                //tc.TcAllResquests();
                tc.LoginAndGetDevices();
                //tc.RequestAndStopVideo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\r\n");
            }
        }

        public void RefreshTree()
        {
            TreeViewDevices.Load();
        }
    }
}
