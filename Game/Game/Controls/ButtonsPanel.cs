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
    public partial class ButtonsPanel : GenericPanel
    {
        /// <summary>
        /// מופעל בעת לחיצה על כפתור משחק
        /// </summary>
        public event EventButtonGameClick ButtonGameClick;

        public ButtonsPanel()
        {
            InitializeComponent();

            foreach (Control control in Controls)
            {
                control.VisibleChanged += OnVisibleOfControlChanged;
            }
        }

        private void ButtonsPanel_Load(object sender, EventArgs e)
        {
            ///
            /// מעדכן את הערך שיכיל כל כפתור, ערך זה יוחזר לחלון הראשי בעת לחיצה על הכפתור
            ///
            button_NewGame.Tag = GameOption.New;
            button_Refresh.Tag = GameOption.Refresh;
            button_Exit.Tag = GameOption.Exit;
            button_Continue.Tag = GameOption.Continue;
            button_Back.Tag = GameOption.Back;
            ///
            ///מעדכן את אירוע הלחיצה של כל הכפתורים
            ///
            foreach (Control control in Controls)
            {
                if (control is Button) control.Click += new EventHandler(OnButtonClick);
            }
        }

        public void SetEnabledButton(params GameOption[] options)
        {
            foreach(Control control in Controls)
            {
                if(control.Tag is GameOption)
                {
                    control.Enabled = Array.IndexOf(options, (GameOption)(control.Tag)) >= 0;
                }
            }
        }

        public void SetVisibleButton(params GameOption[] options)
        {
            foreach (Control control in Controls)
            {
                if (control.Tag is GameOption)
                {
                    control.Visible = Array.IndexOf(options, (GameOption)(control.Tag)) >= 0;
                }
            }
        }


        /// <summary>
        /// בעת לחיצה על כפתור משחק, מעלה אירוע שנלחץ הכפתור ושולח את הערך של הכפתור
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Tag is GameOption)
            {
                ButtonGameClick?.Invoke(this, (GameOption)button.Tag);
            }
        }

        public void EnabledSpecificButtons(params GameOption[] options)
        {
            List<GameOption> optionList = new List<Game.Controls.GameOption>(options);
            foreach(Control control in Controls)
            {
                if(control is Button && control.Tag is GameOption)
                {
                    if(optionList.IndexOf((GameOption)control.Tag)>=0)
                    {
                        control.Enabled = true;
                    }
                    else
                    {
                        control.Enabled = false;
                    }
                }
            }
        }

        public void VisibleSpecificButtons(params GameOption[] options)
        {
            List<GameOption> optionList = new List<Game.Controls.GameOption>(options);
            foreach (Control control in Controls)
            {
                if (control is Button && control.Tag is GameOption)
                {
                    if (optionList.IndexOf((GameOption)control.Tag) >= 0)
                    {
                        control.Visible = true;
                    }
                    else
                    {
                        control.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// get/set את סנגון כל הכפתורים
        /// </summary>
        public FlatStyle FlatStyleOfAllButtons
        {
            get
            {
                return button_NewGame.FlatStyle;
            }

            set
            {
                foreach (Control control in Controls)
                {
                    if (control is Button)
                    {
                        ((Button)control).FlatStyle = value;
                    }
                }
            }
        }

        public bool VisibleNewGame
        {
            get
            {
                return button_NewGame.Visible;
            }

            set
            {
                button_NewGame.Visible = value;
            }
        }

        public bool VisibleRefresh
        {
            get
            {
                return button_Refresh.Visible;
            }

            set
            {
                button_Refresh.Visible = value;
            }
        }
        public bool VisibleExit
        {
            get
            {
                return button_Exit.Visible;
            }

            set
            {
                button_Exit.Visible = value;
            }
        }
        public bool VisibleContinue
        {
            get
            {
                return button_Continue.Visible;
            }

            set
            {
                button_Continue.Visible = value;
            }
        }
        public bool VisibleBack
        {
            get
            {
                return button_Back.Visible;
            }

            set
            {
                button_Back.Visible = value;
            }
        }

        public bool EnabledNewGame
        {
            get
            {
                return button_NewGame.Enabled;
            }

            set
            {
                button_NewGame.Enabled = value;
            }
        }

        public bool EnabledRefresh
        {
            get
            {
                return button_Refresh.Enabled;
            }

            set
            {
                button_Refresh.Enabled = value;
            }
        }
        public bool EnabledExit
        {
            get
            {
                return button_Exit.Enabled;
            }

            set
            {
                button_Exit.Enabled = value;
            }
        }
        public bool EnabledContinue
        {
            get
            {
                return button_Continue.Enabled;
            }

            set
            {
                button_Continue.Enabled = value;
            }
        }
        public bool EnabledBack
        {
            get
            {
                return button_Back.Enabled;
            }

            set
            {
                button_Back.Enabled = value;
            }
        }


    }
}
