using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.Classes;

namespace Game.Controls
{
    public partial class PlayersDetailsPanel : GenericPanel
    {
        public PlayersDetailsPanel()
        {
            InitializeComponent();
        }

        private void PlayersDetailsPanel_Load(object sender, EventArgs e)
        {

        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if(!(e.Control is PlayerTable))
            {
                Controls.Remove(e.Control);
            }
        }

        public Player[] PlayerArry
        {
            get
            {
                return GetPlayers();
            }

            set
            {
                foreach (Player player in value)
                {
                    PlayerTable playerTable = new PlayerTable();
                    playerTable.Player = player;
                    Controls.Add(playerTable);
                }
            }
        }

        public PlayerTable[] PlayerTableArry
        {
            get
            {
                return GetPlayerTables();
            }

            set
            {
                foreach (PlayerTable playerTable in value)
                {
                    Controls.Add(playerTable);
                }
            }
        }

        public Player[] GetPlayers()
        {
            Player[] playerArry = new Classes.Player[Controls.Count];
            for(int i = 0;i<playerArry.Length;i++)
            {
                playerArry[i] = ((PlayerTable)Controls[i]).Player;
            }
            return playerArry;
        }

        public PlayerTable[] GetPlayerTables()
        {
            PlayerTable[] playerTableArry = new PlayerTable[Controls.Count];
            for (int i = 0; i < playerTableArry.Length; i++)
            {
                playerTableArry[i] = (PlayerTable)Controls[i];
            }
            return playerTableArry;
        }
    }
}
