using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Characters;

namespace Break_Bricks.Items
{
    public class Square : Game.Characters.GameObject
    {
        public Square() : base()
        { }

        protected override Game.Characters.Item Copy()
        {
            Square square = new Square();
            CopyTo(square);
            return square;
        }

        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            g.FillRectangle(Brush, Left + extraX, Top + extraY, Width + extraWidth, Height + extraHeight);
        }

        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            g.DrawRectangle(Pen, Left + extraX, Top + extraY, Width + extraWidth, Height + extraHeight);
        }

        protected void CopyTo(Square item)
        { }
    }
}
