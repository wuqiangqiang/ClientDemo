namespace ClientDemo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TreeViewDevices = new ClientDemo.CtlTreeViewDevice();
            PanelVideo = new ClientDemo.CtlPanelVideo();
            this.SuspendLayout();
            // 
            // TreeViewDevices
            // 
            TreeViewDevices.Location = new System.Drawing.Point(12, 12);
            TreeViewDevices.Name = "TreeViewDevices";
            TreeViewDevices.Size = new System.Drawing.Size(268, 480);
            TreeViewDevices.TabIndex = 0;
            // 
            // PanelVideo
            // 
            PanelVideo.AllowDrop = true;
            PanelVideo.Location = new System.Drawing.Point(286, 12);
            PanelVideo.Name = "PanelVideo";
            PanelVideo.Size = new System.Drawing.Size(640, 480);
            PanelVideo.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 506);
            this.Controls.Add(PanelVideo);
            this.Controls.Add(TreeViewDevices);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private CtlTreeViewDevice TreeViewDevices;
        private CtlPanelVideo PanelVideo;
    }
}

