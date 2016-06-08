using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ClientDemo
{
	public enum NodeType
	{
		Root,
        Department,
		Nvr,
		Ipc,
		Channel
	}

	/// <summary>
	/// Summary description for CtlTreeViewDevice.
	/// </summary>
	public class CtlTreeViewDevice : System.Windows.Forms.TreeView
    {
        public TreeNode root = null;

        public CtlTreeViewDevice()
		{
			InitializeComponent();
		}


        public void Load()
        {
            root = new TreeNode(TestCases.getFileRsp.deviceTree.name);
            root.Tag = null;
            AddChilds(root, TestCases.getFileRsp.deviceTree);
            this.Nodes.Add(root);
            this.ExpandAll();
        }

        private void AddChilds(TreeNode parentnode, Department dpt)
        {
            foreach(Department d in dpt.dChilds)
            {
                TreeNode child = new TreeNode(d.name);
                child.Tag = d;
                AddChilds(child, d);
                parentnode.Nodes.Add(child);
            }
            foreach (Device dev in dpt.devices)
            {
                TreeNode child = new TreeNode(dev.title);
                child.Tag = dev;
                foreach (Channel ch in dev.channels)
                {
                    TreeNode tnch = new TreeNode(ch.title);
                    tnch.Tag = ch;
                    child.Nodes.Add(tnch);
                }
                parentnode.Nodes.Add(child);
            }
        }


        private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // CtlTreeViewDevice
            // 
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CamerasTreeView_MouseDown);
            this.ResumeLayout(false);

		}

        private void CamerasTreeView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			TreeNode tn = this.GetNodeAt(e.X, e.Y);
            Channel ch = tn.Tag as Channel;
            if (ch == null)
            {
                //«Î«Û ”∆µ
            }
		}
    }
}
