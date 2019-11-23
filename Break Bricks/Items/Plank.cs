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
    public class Plank : Square, IPlank
    {
        public Plank()
        {
            Brush = Brushes.Red;
            Pen = Pens.Red;
        }

        protected override Game.Characters.Item Copy()
        {
            Plank plank = new Plank();
            base.CopyTo(plank);
            CopyTo(plank);
            return plank;
        }

        private void CopyTo(Plank item)
        { }
    }
}
