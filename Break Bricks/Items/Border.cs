using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Characters;
using Break_Bricks.BoardGame.Interface;

namespace Break_Bricks.Items
{
    class Border : Square, IBorder, IItemF
    {
        public Border() : base()
        {
            Brush = System.Drawing.Brushes.Green;
            Pen = System.Drawing.Pens.Green;
        }

        protected override Game.Characters.Item Copy()
        {
            return new Border();
        }

        public override void Draw(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            //base.Draw(g, extraX, extraY, extraWidth, extraHeight);
        }
    }
}
