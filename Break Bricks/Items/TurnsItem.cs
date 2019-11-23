using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Break_Bricks.Items
{
    public class TurnsItem : Item_Draw
    {
        protected override Item Copy()
        {
            TurnsItem item = new Items.TurnsItem();
            CopyTo(item);
            return item;
        }

        protected void CopyTo(TurnsItem item)
        {

        }

        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            throw new NotImplementedException();
        }

        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            throw new NotImplementedException();
        }
    }
}
