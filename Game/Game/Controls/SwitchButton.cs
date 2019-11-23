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
    public partial class SwitchButton : Label
    {
        private Color color_Default;
        private Color color_OnMouseHover;
        private Color color_OnMouseDown;
        private bool isActionColorSwitch;

        public SwitchButton()
        {
            InitializeComponent();

            isActionColorSwitch = true;
        }

        public Color ColorMouseHover
        {
            get
            {
                return color_OnMouseHover;
            }

            set
            {
                color_OnMouseHover = value;
            }
        }

        public Color ColorMouseDown
        {
            get
            {
                return color_OnMouseDown;
            }

            set
            {
                color_OnMouseDown = value;
            }
        }

        public Color DefaultColor
        {
            get
            {
                return color_Default;
            }

            set
            {
                color_Default = value;
                BackColor = color_Default;
            }
        }

        public bool IsActionColorSwitch
        {
            get
            {
                return isActionColorSwitch;
            }

            set
            {
                isActionColorSwitch = value;
            }
        }

        private void SwitchButton_MouseHover(object sender, EventArgs e)
        {
            if(isActionColorSwitch) BackColor = color_OnMouseHover;
        }

        private void SwitchButton_MouseLeave(object sender, EventArgs e)
        {
            if (isActionColorSwitch) BackColor = color_Default;
        }

        private void SwitchButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (isActionColorSwitch) BackColor = color_OnMouseDown;
        }

        private void SwitchButton_MouseEnter(object sender, EventArgs e)
        {
            if (isActionColorSwitch) BackColor = color_OnMouseHover;
        }

        private void SwitchButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isActionColorSwitch) BackColor = color_Default;
        }

        private void SwitchButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isActionColorSwitch && e.Button != MouseButtons.Left)
                BackColor = color_OnMouseHover;
        }
    }
}
