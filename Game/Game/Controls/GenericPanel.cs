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
    public partial class GenericPanel : UserControl
    {
        public event EventHandler<Direct> ChangeButtonDirect;

        private Direct controlDirect;
        private ImageLayout backgroundImageLayoutOfAllControls;
        private Image backgroundImageOfAllControls;
        private Padding marginOfAllControls;

        public GenericPanel()
        {
            InitializeComponent();
        }

        private void GenericPanel_Load(object sender, EventArgs e)
        {

        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            ControlOrderNavigation();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            ControlOrderNavigation();
        }

        protected virtual void OnVisibleOfControlChanged(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                switch (controlDirect)
                {
                    case Direct.TopBottom:
                        ResizePanel_TopBottom();
                        break;
                    case Direct.RightLeft:
                    case Direct.LeftRight:
                        ResizePanel_RightLeft();
                        break;
                }
            }
        }

        /// <summary>
        /// משנה את גוגל הפאנל לפי הכפתורים המשומשים ומימין לשמאל
        /// </summary>
        protected virtual void ResizePanel_RightLeft()
        {
            int newWidth = 0;
            foreach (Control control in Controls)
            {
                if (control.Visible)
                    newWidth += control.Width + control.Margin.Right + control.Margin.Left;
            }
            Width = newWidth;
        }

        /// <summary>
        /// משנה את גוגל הפאנל לפי הכפתורים המשומשים ומלמעלה למטה
        /// </summary>
        protected virtual void ResizePanel_TopBottom()
        {
            int newHeight = 0;
            foreach (Control control in Controls)
            {
                if (control.Visible)
                    newHeight += control.Height + control.Margin.Top + control.Margin.Bottom;
            }
            Height = newHeight;
        }

        protected virtual void OnControlDirectChanged(Direct buttonsDirect)
        {
            ControlOrderNavigation();
            ChangeButtonDirect?.Invoke(this, buttonsDirect);
        }

        protected virtual void GenericPanel_Resize(object sender, EventArgs e)
        {
            ControlOrderNavigation();
        }

        protected virtual void ControlOrderNavigation()
        {
            switch (controlDirect)
            {
                case Direct.TopBottom:
                    UpdateSizeAndLocationOfControl_TopBottom();
                    break;
                case Direct.RightLeft:
                case Direct.LeftRight:
                    UpdateSizeAndLocationOfControl_RightLeft(controlDirect);
                    break;
            }
        }

        /// <summary>
        /// מסדר את גודל הכפתורים ביחסם הקבוע והראשוני אל הפאנל המכיל אותם
        /// </summary>
        protected virtual void UpdateSizeAndLocationOfControl_TopBottom()
        {
            int theMaximumHeightOfButton = 0; // הגובה אותו אמורים הכפתורים לתפוס(בלי הרווחים ביניהם)
            int sumOfMarginTopBottom = 0;
            int newHeightOfPanel = 0;
            int sumOfVisibleControls = 0;
            bool isFirstControl = true;
            Control theLastControl = new Control();
            foreach (Control control in Controls)
            {
                if (control.Visible)
                {
                    sumOfVisibleControls++;
                    sumOfMarginTopBottom += control.Margin.Top + control.Margin.Bottom;
                }
            }
            theMaximumHeightOfButton = Height - sumOfMarginTopBottom;
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].Visible)
                {
                    Controls[i].Height = theMaximumHeightOfButton / sumOfVisibleControls; //הגובה שכל כפתור יכול לתפוס
                    Controls[i].Width = Width - Controls[i].Margin.Left - Controls[i].Margin.Right; //מסדר את הגודל של הכפתורים ביחס לפאנל לפי המרחק הקבוע
                    Controls[i].Top = Controls[i].Margin.Top;
                    Controls[i].Left = Controls[i].Margin.Left;
                    if (!isFirstControl)
                    {
                        Controls[i].Top += theLastControl.Top + theLastControl.Height + theLastControl.Margin.Bottom;
                    }
                    else isFirstControl = false;
                    newHeightOfPanel += Controls[i].Height + Controls[i].Margin.Top + Controls[i].Margin.Bottom;
                    theLastControl = Controls[i];
                }
            }
            if (Height != newHeightOfPanel) Height = newHeightOfPanel;
        }

        protected virtual void UpdateSizeAndLocationOfControl_RightLeft(Direct controlDirect)
        {
            int theMaximumWidthOfButton = 0; // הגובה אותו אמורים הכפתורים לתפוס(בלי הרווחים ביניהם)
            int sumOfMarginRightLeft = 0;
            int newWidthOfPanel = 0;
            int sumOfVisibleControls = 0;
            bool isFirstControl = true;
            Control theLastControl = new Control();
            foreach (Control control in Controls)
            {
                if (control.Visible)
                {
                    sumOfVisibleControls++;
                    sumOfMarginRightLeft += control.Margin.Right + control.Margin.Left;
                }
            }
            theMaximumWidthOfButton = Width - sumOfMarginRightLeft;

            int startIndex = 0;
            int lastIndex = 0;
            int operatorForIndex = 0;
            switch (controlDirect)
            {
                case Direct.RightLeft:
                    startIndex = Controls.Count - 1;
                    lastIndex = -1;
                    operatorForIndex = -1;
                    break;
                case Direct.LeftRight:
                    startIndex = 0;
                    lastIndex = Controls.Count;
                    operatorForIndex = 1;
                    break;
            }
            for (int i = startIndex; i != lastIndex; i += operatorForIndex)
            {
                if (Controls[i].Visible)
                {
                    Controls[i].Width = theMaximumWidthOfButton / sumOfVisibleControls; //הגובה שכל כפתור יכול לתפוס
                    Controls[i].Height = Height - Controls[i].Margin.Top - Controls[i].Margin.Bottom; //מסדר את הגודל של הכפתורים ביחס לפאנל לפי המרחק הקבוע
                    Controls[i].Top = Controls[i].Margin.Top;
                    Controls[i].Left = Controls[i].Margin.Left;
                    if (!isFirstControl)
                    {
                        Controls[i].Left += theLastControl.Left + theLastControl.Width + theLastControl.Margin.Right;
                    }
                    else isFirstControl = false;
                    newWidthOfPanel += Controls[i].Width + Controls[i].Margin.Right + Controls[i].Margin.Left;
                    theLastControl = Controls[i];
                }
            }
            if (Width != newWidthOfPanel) Width = newWidthOfPanel;
        }


        public Padding MarginOfAllControls
        {
            get
            {
                return marginOfAllControls;
            }

            set
            {
                marginOfAllControls = value;
                SetMarginOfAllControls(value);
            }
        }

        public Direct ControlDirect
        {
            get
            {
                return controlDirect;
            }

            set
            {
                controlDirect = value;
                OnControlDirectChanged(controlDirect);
            }
        }

        public Image ImageOfAllCntrols
        {
            get
            {
                return backgroundImageOfAllControls;
            }

            set
            {
                backgroundImageOfAllControls = value;
                SetBackImageOfAllControls(backgroundImageOfAllControls);
            }
        }

        public ImageLayout ImageLayoutOfAllControls
        {
            get
            {
                return backgroundImageLayoutOfAllControls;
            }

            set
            {
                backgroundImageLayoutOfAllControls = value;
                SetImageLayoutOfAllControls(backgroundImageLayoutOfAllControls);
            }
        }

        public Color BackColorOfAllControls
        {
            get
            {
                if (Controls.Count > 0)
                    return Controls[0].BackColor;
                else return default(Color);
            }

            set
            {
                SetBackColorOfAllControls(value);
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;
                SetForeColorOfAllControls(value);
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
                foreach (Control control in Controls)
                {
                    control.Font = value;
                }
            }
        }


        /// <summary>
        /// משנה את המרחק שבין כל פקד לפקד
        /// </summary>
        /// <param name="margin">המרחק החדש</param>
        public virtual void SetMarginOfAllControls(Padding margin)
        {
            foreach (Control control in Controls)
            {
                control.Margin = margin;
                if (margin.Left < control.Left) control.Left = margin.Left;
            }
            UpdateSizeAndLocationOfControl_TopBottom();
        }

        /// <summary>
        /// משנה את תמונת הרקע של כל הפקדים
        /// </summary>
        /// <param name="image"></param>
        public virtual void SetBackImageOfAllControls(Image image)
        {
            foreach (Control control in Controls)
            {
                control.BackgroundImage = image;
            }
        }

        /// <summary>
        /// משנה את הסגנון בה התמונה תופיע ברקע התמונה לכל הפקדים
        /// </summary>
        /// <param name="imageLayout"></param>
        public virtual void SetImageLayoutOfAllControls(ImageLayout imageLayout)
        {
            foreach (Control control in Controls)
            {
                control.BackgroundImageLayout = imageLayout;
            }
        }

        /// <summary>
        /// משנה את צבע הרקע של כל הפקדים
        /// </summary>
        /// <param name="backColor"></param>
        public virtual void SetBackColorOfAllControls(Color backColor)
        {
            foreach (Control control in Controls)
            {
                control.BackColor = backColor;
            }
        }

        /// <summary>
        /// משנה את צבע הטקסט של כל הפקדים
        /// </summary>
        /// <param name="foreColor"></param>
        public virtual void SetForeColorOfAllControls(Color foreColor)
        {
            foreach (Control control in Controls)
            {
                control.ForeColor = foreColor;
            }
        }

    }
}
