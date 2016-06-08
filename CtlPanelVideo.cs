using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace ClientDemo
{
    /// <summary>
    /// Summary description for CtlPanelVideo.
    /// </summary>
    public class CtlPanelVideo : System.Windows.Forms.Control
    {
        public Bitmap frame = new Bitmap("1.jpg");

        #region private properties

        private static Pen recordingPen = new Pen(Color.Red, 5);
        private static Pen detectingPen = new Pen(Color.Red, 2);
        private static Pen recBorderPen = new Pen(Color.Black, 1);
        private static Pen markBorderPen = new Pen(Color.Red, 2);
        private static Font errorinfoFont = new Font("Arial", 10);
        private static SolidBrush errorinfoBrush = new SolidBrush(Color.White);
        private static Font camerainfoFont = new Font("Arial", 10);
        private static SolidBrush camerainfoBrush = new SolidBrush(Color.Red);

        private bool autosize = false;
        private bool needSizeUpdate = false;
        private bool firstFrame = true;
        private bool isMarked = false;

        #endregion


        #region constructor

        // Constructor
        public CtlPanelVideo()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            this.AllowDrop = true;
        }

        #endregion


        #region private methods

        // Paint control
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (frame != null)
            {
                try
                {
                    Graphics g = pe.Graphics;
                    Rectangle rc = this.ClientRectangle;
                    // draw frame
                    g.DrawImage(frame, rc.X, rc.Y, rc.Width, rc.Height);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(Thread.CurrentThread.Name + "============= CtlPanelVideo.OnPaint(): " + ex.Message);
                }
            }
            base.OnPaint(pe);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CtlPanelVideo
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ResumeLayout(false);

        }

        // On new frame ready
        private void OnNewFrame(object sender, System.EventArgs e)
        {
            Invalidate();
        }

        #endregion
    }
}
