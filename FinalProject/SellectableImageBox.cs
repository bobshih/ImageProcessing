using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    class SellectableAndTracableImageBox : ImageBox
    {
        private SelectPair _selection;
        private Trace _trace;
        /// <summary>
        /// 取得影像實際縮放比
        /// </summary>
        public double ImageVisualScale
        {
            get
            {
                double scale;
                //scale based on two axis(showed/ori)
                double scaleX = this.Size.Width / (double)this.DisplayedImage.Size.Width;
                double scaleY = this.Size.Height / (double)this.DisplayedImage.Size.Height;
                //Always Pick The Smaller One
                scale = Math.Min(scaleX, scaleY);
                /*
                if (scaleX < 1.0 || scaleY < 1.0)
                {
                    //Image Is Shrinked, Pick Up The Smaller One
                    scale = Math.Min(scaleX, scaleY);
                }
                else
                {
                    //Image Is Expended, Pick Up The Bigger One
                    scale = Math.Max(scaleX, scaleY);
                }
                 * */
                return scale;
            }
        }
        /// <summary>
        /// 取得顯示的影像顯示的大小
        /// </summary>
        public Size ImageVisualSize
        {
            get
            {
                double scale = this.ImageVisualScale;
                return new Size((int)(this.Image.Size.Width * scale), (int)(this.Image.Size.Height * scale));
            }

        }
        /// <summary>
        /// 取得顯示的影像在這個元件中的範圍
        /// </summary>
        public Rectangle ImageVisualRectangle
        {
            get
            {
                var csize = this.ClientSize;
                Size visualSize = this.ImageVisualSize;
                int locationX = (Size.Width - visualSize.Width) / 2;
                int locationY = (Size.Height - visualSize.Height) / 2;
                return new Rectangle(locationX, locationY, visualSize.Width, visualSize.Height);
            }
        }
        /// <summary>
        /// 是否正在選擇
        /// </summary>
        public bool Selecting
        {
            get
            {
                return _selection.AnyPointAssigned;
            }
        }
        /// <summary>
        /// 是否正在追蹤
        /// </summary>
        public bool Tracing
        {
            get
            {
                return _trace != null;
            }
        }
        /// <summary>
        /// 取得追蹤物件在影像上的中心點
        /// </summary>
        public Point MidPoint
        {
            get
            {
                Rectangle rect = Window;
                return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            }
        }
        /// <summary>
        /// 取得追蹤物件的在影像上的 Bounding Box
        /// </summary>
        public Rectangle Window
        {
            get
            {
                return _trace == null ? new Rectangle() : _trace.TrackingWindow;
            }
        }

        /// <summary>
        /// 建構函式
        /// </summary>
        public SellectableAndTracableImageBox()
            : base()
        {
            this.DoubleBuffered = true;
            _trace = null;
            _selection = new SelectPair();
        }

        /// <summary>
        /// Select The First Point
        /// </summary>
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _selection.FirstPoint = e.Location;
        }

        /// <summary>
        /// Drag The Selected Window
        /// </summary>
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _selection.SecondPoint = e.Location;
            Invalidate();
        }

        /// <summary>
        /// Select The Second Point
        /// </summary>
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _selection.SecondPoint = e.Location;
            if (_selection.AllPointsAssigned)
            {

                Rectangle rect = GetSelectedRectOnImage();
                try
                {
                    _trace = new Trace(this.Image as Image<Bgr, byte>, rect);
                }
                catch (Emgu.CV.Util.CvException)
                {
                    MessageBox.Show("乖~~別做傻事喔！", "別框選到外面喔！");
                    _trace = null;
                }
            }
            _selection.Reset();
            Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //Selecting,Draw The Selected Region
            if (Selecting && _selection.AllPointsAssigned)
            {
                using (Pen pen = new Pen(Color.Goldenrod, 2))
                {
                    pe.Graphics.DrawRectangle(pen, _selection.SelectedRectangle);
                }
            }
            else if (Tracing)
            {
                using (Pen pen = new Pen(Color.Beige, 2))
                {
                    //Update Trace And Get The Window
                    Rectangle rect = _trace.Tracking(this.Image as Image<Bgr, byte>);
                    //rect = new Rectangle(0, 0, 10, 10);
                    double scale = this.ImageVisualScale;
                    Rectangle full = this.ClientRectangle;
                    Rectangle pic = this.ImageVisualRectangle;

                    rect.X = (int)(rect.X * scale);
                    rect.Y = (int)(rect.Y * scale);
                    rect.Width = (int)(rect.Width * scale);
                    rect.Height = (int)(rect.Height * scale);
                    rect.X += pic.X;
                    rect.Y += pic.Y;

                    pe.Graphics.DrawRectangle(pen, rect);
                }
            }
        }


        /// <summary>
        /// 取得選取範圍對應到影像上面的區域
        /// </summary>
        /// <returns></returns>
        private Rectangle GetSelectedRectOnImage()
        {
            double scale = this.ImageVisualScale;
            Rectangle full = this.ClientRectangle;
            Rectangle pic = this.ImageVisualRectangle;
            Rectangle rect = _selection.SelectedRectangle;

            rect.X -= pic.X;
            rect.Y -= pic.Y;

            rect.X = (int)(rect.X / scale);
            rect.Y = (int)(rect.Y / scale);
            rect.Width = (int)(rect.Width / scale);
            rect.Height = (int)(rect.Height / scale);

            //rect.Intersect(this.ImageVisualRectangle);
            return rect;
        }
    }
}
