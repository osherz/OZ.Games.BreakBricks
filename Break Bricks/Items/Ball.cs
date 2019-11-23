using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Characters;
using Break_Bricks.BoardGame.Interface;

namespace Break_Bricks.Items
{
    public class Ball : Game.Characters.Ball, IBall
    {
        public Ball() : base()
        {
            Brush = Brushes.Blue;
            Pen = new Pen(Color.Blue, 1);
        }

        protected override Game.Characters.Item Copy()
        {
            Ball ball = new Ball();
            base.CopyTo(ball);
            CopyTo(ball);
            return ball;
        }

        protected void CopyTo(Ball ball)
        {

        }

        public override float Speed
        {
            get
            {
                return base.Speed;
            }

            set
            {
                base.Speed = value;
            }
        }

        public override bool IsHit(float x, float y)
        {
            if(GetRectangleF().Contains(x,y))
            {
                OnHitted(new Region(new RectangleF(x, y, 0.1f, 0.1f)));
                return true;
            }
            return false;
            //return base.IsHit(x, y);
        }
    }
}
