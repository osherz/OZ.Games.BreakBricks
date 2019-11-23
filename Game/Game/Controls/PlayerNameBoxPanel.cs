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
    public partial class PlayerNameBoxPanel : GenericPanel
    {
        public event EventHandler<TextBox> ChangePlayerTextBox;
        public event EventHandler<TextBox> BeforeChangePlayerTextBox;
        public event EventHandler<int> ChangeSumOfPlayers;

        private TextBox playerTextBox;
        private List<TextBox> playersTextBoxList;

        public PlayerNameBoxPanel()
        {
            playersTextBoxList = new List<TextBox>();
            playerTextBox = new TextBox();
            RestartPlayerTextBoxEvent();

            InitializeComponent();

        }

        private void PlayerNameBoxPanel_Load(object sender, EventArgs e)
        {

        }

        public int SumOfPlayers
        {
            get
            {
                return playersTextBoxList.Count;
            }

            set
            {
                if (SumOfPlayers < value)
                {
                    for(int i = SumOfPlayers; i<value;i++)
                    {
                        TextBox textBox = new TextBox();
                        textBox.BackColor = playerTextBox.BackColor;
                        textBox.Font = playerTextBox.Font;
                        textBox.ForeColor = playerTextBox.ForeColor;
                        textBox.Text = playerTextBox.Text + " " + (SumOfPlayers+1);
                        playersTextBoxList.Add(textBox);
                        Controls.Add(textBox);
                    }
                }
                else if (SumOfPlayers > value)
                {
                    for(int i=SumOfPlayers-1; SumOfPlayers > value; i--)
                    {
                        TextBox textBox = playersTextBoxList[i];
                        playersTextBoxList.Remove(textBox);
                        Controls.Remove(textBox);
                    }
                }
                OnChangeSumOfPlayers(SumOfPlayers);
            }
        }

        /// <summary>
        /// all TextBox will be like this one
        /// the text only will change with number 1
        /// </summary>
        public TextBox PlayerTextBox
        {
            get
            {
                return playerTextBox;
            }

            set
            {
                OnBeforePlayerTextBoxChanged(this, new EventArgs());
                BeforeChangePlayerTextBox?.Invoke(this, value);
                playerTextBox = value;
                RestartPlayerTextBoxEvent();
                OnPlayerTextBoxChanged(this, new EventArgs());
            }
        }

        public string TextPlayerTextBox
        {
            get
            {
                return playerTextBox.Text;
            }

            set
            {
                playerTextBox.Text = value;
            }
        }

        public Color BackColorPlayerTextBox
        {
            get
            {
                return playerTextBox.BackColor;
            }

            set
            {
                playerTextBox.BackColor = value;
            }
        }

        public string[] GetPlayersNames()
        {
            string[] playersNames = new string[SumOfPlayers];
            for (int i = 0; i < SumOfPlayers; i++)
            {
                playersNames[i] = playersTextBoxList[i].Text;
            }
            return playersNames;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            PlayerTextBox.ForeColor = ForeColor;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            PlayerTextBox.Font = Font;
        }

        protected virtual void OnChangeSumOfPlayers(int sumOfPlayers)
        {
            
            ChangeSumOfPlayers?.Invoke(this, sumOfPlayers);
        }

        private void RestartPlayerTextBoxEvent()
        {
            playerTextBox.ForeColorChanged += OnPlayerTextBoxChanged;
            playerTextBox.BackColorChanged += OnPlayerTextBoxChanged;
            playerTextBox.FontChanged += OnPlayerTextBoxChanged;
            playerTextBox.TextChanged += OnPlayerTextBoxChanged;
        }

        private void ResetPlayerTextBoxEvent()
        {
            playerTextBox.ForeColorChanged -= OnPlayerTextBoxChanged;
            playerTextBox.BackColorChanged -= OnPlayerTextBoxChanged;
            playerTextBox.FontChanged -= OnPlayerTextBoxChanged;
            playerTextBox.TextChanged -= OnPlayerTextBoxChanged;
        }

        protected virtual void OnPlayerTextBoxChanged(object sender, EventArgs e)
        {
            for (int i = 0; i<playersTextBoxList.Count;i++)
            {
                playersTextBoxList[i].ForeColor = PlayerTextBox.ForeColor;
                playersTextBoxList[i].BackColor = PlayerTextBox.BackColor;
                playersTextBoxList[i].Font = PlayerTextBox.Font;
                playersTextBoxList[i].Text = PlayerTextBox.Text + " " + (i + 1);
            }
            ChangePlayerTextBox?.Invoke(this, playerTextBox);
        }

        protected virtual void OnBeforePlayerTextBoxChanged(PlayerNameBoxPanel playerNameBoxPanel, EventArgs eventArgs)
        {
            ResetPlayerTextBoxEvent();
        }

    }
}
