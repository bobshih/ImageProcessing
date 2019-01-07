using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FinalProject
{
    public class DodgeGame
    {
        //world
        List<RoundBullet> _bullets = new List<RoundBullet>();
        Rectangle _fieldRectangle;

        //ufo
        Point _ufo;
        int _lifes;
        int _levelUpCounter;
        Bitmap _ufoPic;
        Bitmap _earthPic;
        Bitmap _gameover;

        //other
        Random r = new Random(Guid.NewGuid().GetHashCode());

        //檢查玩家死了沒
        public bool Died
        {
            get
            {
                return _lifes <= 0;
            }
        }
        //level
        public int Level
        {
            get;
            private set;
        }
        private int MaxBulletCount
        {
            get
            {
                return Level * Level * 5;
            }
        }
        private int LevelTime//frame
        {
            get
            {
                return Level * 20;
            }
        }
        private int LevelBulletMaxSpeed
        {
            get
            {
                int v = 20 - (int)(Level * 1.5);
                return v < 5 ? 5 : v;
            }
        }


        public DodgeGame(Size rect)
        {
            Level = 1;
            _levelUpCounter = LevelTime;

            _lifes = 5;
            _fieldRectangle = new Rectangle(new Point(0, 0), rect);
            _ufoPic = new Bitmap(FinalProject.Properties.Resources.UFO);
            _earthPic = new Bitmap(FinalProject.Properties.Resources.earth);
            _gameover = new Bitmap(FinalProject.Properties.Resources.GameOver);
        }

        ~DodgeGame()
        {
            _ufoPic.Dispose();
            _earthPic.Dispose();
            _gameover.Dispose();
        }
        public void Update(Point playerPosition)
        {
            if (_lifes == 0)
                return;
            CheckLevel();
            MoveBullets();
            UpdateUFOPosition(playerPosition);
            DetectCollapsion();
            //GenerateBullets();
            GenerateNewBulletSpecial();
        }

        void CheckLevel()
        {
            --_levelUpCounter;
            if (_levelUpCounter == 0)
            {
                Level++;
                _lifes++;
                _levelUpCounter = Level * 30;
            }
        }

        private void MoveBullets()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].Move();
                if (!_fieldRectangle.Contains(_bullets[i].center))
                {
                    _bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        private void UpdateUFOPosition(Point playerPosition)
        {
            _ufo = playerPosition;
        }

        private void DetectCollapsion()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (CheckCollision(_bullets[i].center, _ufo, 10))
                {
                    _bullets.RemoveAt(i);
                    _lifes--;
                    break;
                }
            }
        }

        private void GenerateBullets()
        {
            int emptyBullet = MaxBulletCount - _bullets.Count;
            while (emptyBullet != 0)
            {
                emptyBullet--;
                if (r.Next() % 100 < 5)
                {
                    GenerateNewBullet();
                }
            }
        }

        private void GenerateNewBullet()
        {
            RoundBullet temp = new RoundBullet();
            temp.center.X = (int)(r.NextDouble() * _fieldRectangle.Width);
            temp.center.Y = (int)(r.NextDouble() * _fieldRectangle.Height);

            //Too Close
            if (CheckCollision(temp.center, _ufo, LevelBulletMaxSpeed * 5))
                return;

            temp.speed.X = (int)((1 - r.NextDouble() * r.NextDouble()) * LevelBulletMaxSpeed);
            temp.speed.Y = (int)((1 - r.NextDouble() * r.NextDouble()) * LevelBulletMaxSpeed);
            temp.speed.X *= (r.Next() % 2 == 0) ? 1 : -1;
            temp.speed.Y *= (r.Next() % 2 == 0) ? 1 : -1;
            temp.color = Color.FromArgb(r.Next() % 128 + 64, r.Next() % 128 + 64, r.Next() % 128 + 64);
            temp.radius = 10;
            _bullets.Add(temp);
        }
        private void GenerateNewBulletSpecial()
        {
            RoundBullet temp = new RoundBullet();
            temp.center.X = (int)(r.NextDouble() * _fieldRectangle.Width);
            temp.center.Y = (int)(r.NextDouble() * _fieldRectangle.Height) / 5;

            //Too Close
            if (CheckCollision(temp.center, _ufo, LevelBulletMaxSpeed * 2))
                return;

            temp.speed.X = (int)((1 - r.NextDouble() * r.NextDouble()) * LevelBulletMaxSpeed);
            temp.speed.Y = (int)((1 - r.NextDouble()) * LevelBulletMaxSpeed) + 5;
            temp.speed.X *= (r.Next() % 2 == 0) ? 1 : -1;
            //temp.speed.Y *= (r.Next() % 2 == 0) ? 1 : -1;
            temp.color = Color.FromArgb(r.Next() % 128 + 64, r.Next() % 128 + 64, r.Next() % 128 + 64);
            temp.radius = 10;
            _bullets.Add(temp);
        }
        private bool CheckCollision(Point a, Point b, int r)
        {
            r += 5;
            return (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y) < r * r;
        }

        public void Paint(Graphics g)
        {
            if (_lifes != 0)
                DrawStage(g);
            else
                DrawGameOver(g);
        }

        private void DrawGameOver(Graphics g)
        {
            g.Clear(Color.Red);
            g.DrawImage(_gameover, _fieldRectangle);
        }

        private void DrawStage(Graphics g)
        {
            g.Clear(Color.Black);
            foreach (RoundBullet bullet in _bullets)
            {
                using (Brush b = new SolidBrush(bullet.color))
                {
                    g.FillEllipse(b, bullet.center.X - bullet.radius, bullet.center.Y - bullet.radius, bullet.radius + bullet.radius, bullet.radius + bullet.radius);
                }
            }
            for (int i = 0; i < _lifes; i++)
            {
                g.FillRectangle(Brushes.DarkSalmon, 32 + i * 18, _fieldRectangle.Height - 32 - 32, 16, 20);
            }
            for (int i = 0; i < Level; i++)
            {
                g.FillRectangle(Brushes.Violet, _fieldRectangle.Width - 32 - i * 8, _fieldRectangle.Height - 32 - 32, 4, 16);
            }
            //g.FillRectangle(Brushes.AliceBlue, new Rectangle(_ufo, new Size(10, 10)));
            var position = new Rectangle(_ufo, new Size(30, 30));
            position.X -= 15;
            position.Y -= 15;
            g.DrawImage(_earthPic, position);
            //g.FillRectangle(Brushes.Black, new Rectangle(0, -100, _fieldRectangle.Width, 150));
            //g.DrawImage(_ufoPic, new Rectangle(0, -100, _fieldRectangle.Width, 300));

            int bulletGenerateLine = _fieldRectangle.Height / 5;
            g.FillRectangle(Brushes.Black, new Rectangle(0, -bulletGenerateLine, _fieldRectangle.Width, bulletGenerateLine * 2));
            g.DrawImage(_ufoPic, new Rectangle(0, -bulletGenerateLine, _fieldRectangle.Width, bulletGenerateLine * 3));
            //g.DrawImage(_hero, new PointF(_character.X - _hero.Width/2, _character.Y - _hero.Height/2));
        }

        public void SetFormSize(Size rect)
        {
            _fieldRectangle = new Rectangle(new Point(0, 0), rect);
        }
    }
}
