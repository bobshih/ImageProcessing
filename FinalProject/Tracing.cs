using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace FinalProject
{
    class Trace
    {
        private DenseHistogram _histogram;
        public Rectangle TrackingWindow
        {
            get;
            private set;
        }

        public Trace(Image<Bgr, Byte> image, Rectangle ROI)
        {
            TrackingWindow = ROI;
            _histogram = CalculateHistogram(image, ROI);
        }

        public Rectangle Tracking(Image<Bgr, Byte> image)
        {
            Image<Gray, Byte> mask, hue;
            CaluateHueAndMask(image, out mask, out hue);

            var backproject = _histogram.BackProject(new Image<Gray, Byte>[] { hue }).And(mask);



            MCvConnectedComp trackcomp;
            MCvBox2D trackbox;
            try
            {
                CvInvoke.cvCamShift(backproject, TrackingWindow, new MCvTermCriteria(10, 1), out trackcomp, out trackbox);
            }
            catch (Emgu.CV.Util.CvException)
            {
                return TrackingWindow;
            }

            var newWindow = trackcomp.rect;
            // Tracking windows empty means camshift lost bounding-box, keep current bounding-box
            if (newWindow.IsEmpty || newWindow.Width == 0 || newWindow.Height == 0)
            {
                return TrackingWindow;
            }
            // update tracking window and return
            else
            {
                TrackingWindow = trackcomp.rect;

                return TrackingWindow;
            }
        }

        //計算直方圖，然後縮放比例，使最大值為255
        private static DenseHistogram CalculateHistogram(Image<Bgr, Byte> image, Rectangle ROI)
        {
            Image<Gray, Byte> mask, hue;
            DenseHistogram hist = new DenseHistogram(100, new RangeF(0, 255));
            CaluateHueAndMask(image, out mask, out hue);

            // Set tracking object's ROI
            hue.ROI = ROI;
            mask.ROI = ROI;
            hist.Calculate(new Image<Gray, Byte>[] { hue }, false, mask);

            // Scale Historgram
            float max = 0, min = 0, scale = 0;
            int[] minLocations, maxLocations;
            hist.MinMax(out min, out max, out minLocations, out maxLocations);
            if (max != 0)
            {
                scale = 255 / max;
            }
            CvInvoke.cvConvertScale(hist.MCvHistogram.bins, hist.MCvHistogram.bins, scale, 0);
            return hist;
        }

        private static void CaluateHueAndMask(Image<Bgr, Byte> image, out Image<Gray, Byte> mask, out Image<Gray, Byte> hue)
        {
            Image<Hsv, Byte> hsv = image.Convert<Hsv, Byte>();
            mask = hsv.InRange(new Hsv(0, 30, 10), new Hsv(180, 256, 255));
            hue = hsv.Split()[0];
        }
    }
}
