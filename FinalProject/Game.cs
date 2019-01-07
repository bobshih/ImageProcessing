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
    public partial class Game : Form
    {
        DodgeGame game;
        private Point pt;
        System.Timers.Timer forUpdate;


        public Game(Point start)
        {
            InitializeComponent();
            game = new DodgeGame(ClientSize);
            this.DoubleBuffered = true;

            pt = start;
            forUpdate = new System.Timers.Timer(33);
            forUpdate.AutoReset = true;
            forUpdate.Elapsed += UpdateForm;
            forUpdate.Start();
        }
        bool running = false;
        object gameActionlockkey = new object();
        void UpdateForm(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (running)
                return;
            running = true;
            lock (gameActionlockkey)
            {
                if (game == null)
                    return;
                game.Update(pt);
                Invalidate();
            }
            running = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lock (gameActionlockkey)
            {
                if (game == null)
                    return;
                base.OnPaint(e);
                game.Paint(e.Graphics);
                if (game.Died)
                {
                    _cLabelLevelUGet.Text = "恭喜你到達第 " + game.Level.ToString() + " 關！！";
                }
            }
        }

        private void ChangeClientSize(object sender, EventArgs e)
        {
            lock (gameActionlockkey)
            {
                if (game == null)
                    return;
                game.SetFormSize(ClientSize);
            }
        }

        public void SetInputLocation(double x, double y)
        {
            pt.X = (int)(this.Size.Width * x);
            pt.Y = (int)(this.Size.Height * y);
        }
    }
}
