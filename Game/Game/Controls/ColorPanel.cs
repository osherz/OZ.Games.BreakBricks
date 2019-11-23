using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Controls
{
    public partial class ColorPanel : ChooseLabel<Color>
    {
        public ColorPanel() :base()
        {

        }

        protected override void EditMainLabel(Color item, Label label)
        {
            base.EditMainLabel(item, label);
            label.BackColor = item;
        }
    }
}
