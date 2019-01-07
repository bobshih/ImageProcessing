using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class SelectPair
    {
        Point _p1, _p2;
        bool _p1Set = false, _p2set = false;

        public void Reset()
        {
            _p1Set = false;
            _p2set = false;
        }

        public Point FirstPoint
        {
            get
            {
                return _p1;
            }
            set
            {
                _p1 = value;
                _p1Set = true;
            }
        }

        public Point SecondPoint
        {
            get
            {
                return _p2;
            }
            set
            {
                _p2 = value;
                _p2set = _p1Set;
            }
        }

        public bool AllPointsAssigned
        {
            get
            {
                return _p2set;
            }
        }

        public bool AnyPointAssigned
        {
            get
            {
                return !AllPointsNotAssigned;
            }
        }

        public bool AllPointsNotAssigned
        {
            get
            {
                return !_p1Set;
            }
        }

        public Rectangle SelectedRectangle
        {
            get
            {
                Point location = new Point(Math.Min(FirstPoint.X, SecondPoint.X), Math.Min(FirstPoint.Y, SecondPoint.Y));
                Size size = new Size(Math.Abs(FirstPoint.X - SecondPoint.X), Math.Abs(FirstPoint.Y - SecondPoint.Y));
                return new Rectangle(location, size);
            }
        }
    }
}
