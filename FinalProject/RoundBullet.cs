using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class RoundBullet
    {
        public Point center;
        public Color color;
        public Point speed;
        public int radius;

        /// <summary>
        /// 移動子彈位置
        /// </summary>
        public void Move()
        {
            center.X += speed.X;
            center.Y += speed.Y;
        }
    }
}
