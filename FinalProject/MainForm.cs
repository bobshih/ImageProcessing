using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class MainForm : Form
    {
        Capture _capture;

        public MainForm()
        {
            InitializeComponent();
            _capture = new Capture();
            _capture.ImageGrabbed += GetPic;
            _capture.Start();
        }

        private void GetPic(object sender, EventArgs e)
        {
            Image<Bgr, byte> temp = _capture.RetrieveBgrFrame();
            temp._Flip(FLIP.HORIZONTAL);
            _cImageBoxTracing.Image = temp;
        }

        private void ClickGameButton(object sender, EventArgs e)
        {
            if (!_cImageBoxTracing.Tracing)
            {
                MessageBox.Show("先找個追蹤物件嘿！", "別急~");
            }
            else
            {
                Game gameWindow = new Game(_cImageBoxTracing.MidPoint);
                EventHandler getpoint = (s, ev) =>
                {
                    Size gameSize = gameWindow.ClientSize;
                    Size picSize = _cImageBoxTracing.Image.Size;
                    Point mid = _cImageBoxTracing.MidPoint;
                    gameWindow.SetInputLocation(mid.X / (double)picSize.Width, mid.Y / (double)picSize.Height);
                };
                _capture.ImageGrabbed += getpoint;
                gameWindow.ShowDialog();
                _capture.ImageGrabbed -= getpoint;
            }
        }
    }
}
