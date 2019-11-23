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
    public partial class PlayerTable : UserControl
    {
        /// 
        /// events
        /// 
        public event EventHandler<string> ChangeSpaceBetween;
        public event EventHandler<Padding> ChangeMarginOfAllControls;
        public event EventHandler<Player> ChangePlayer;
        public event EventHandler<Player> BeforeChangePlayer;
        public event EventHandler<PlayerIcon> ChangeIcon;
        public event EventHandler<PlayerIcon> BeforeChangeIcon;
        /// 
        /// variables
        /// 
        private Player player;
        private PlayerIcon icon;
        private string spaceBetween;

        public PlayerTable()
        {
            spaceBetween = " - ";

            InitializeComponent();

            player = new Player();
            icon = new PlayerIcon();
        }

        private void PlayerTable_Load(object sender, EventArgs e)
        {
        }

        public Player Player
        {
            get
            {
                return player;
            }

            set
            {
                OnBeforeChangePlayer(value);
                player = value;
                OnChangePlayer(value);
            }
        }

        public string PlayerName
        {
            get
            {
                return player.Name;
            }

            set
            {
                player.Name = value;
            }
        }

        public int Score
        {
            get
            {
                return player.Score;
            }

            set
            {
                player.Score = value;
            }
        }

        /// <summary>
        /// מחזיר ומקבל את העך שיהיה בין שם השחקן לניקוד שלו
        ///  חייב להיות קטן מ-4
        /// </summary>
        public string SpaceBetween
        {
            get
            {
                return spaceBetween;
            }

            set
            {
                if (value.Length < 4)
                {
                    spaceBetween = value;
                    ChangeSpaceBetween?.Invoke(this, spaceBetween);
                }
            }
        }

        public Padding MarginOfAllControls
        {
            get
            {
                return Controls[0].Margin;
            }

            set
            {
                foreach(Control control in Controls)
                {
                    control.Margin = value;
                }
                ChangeMarginOfAllControls?.Invoke(this, value);
            }
        }

        /// 
        /// propertis - Icon
        /// 
        public PlayerIcon Icon
        {
            get
            {
                return icon;
            }

            set
            {
                OnBeforeChangeIcon(icon);
                icon = value;
                OnChangeIcon(icon);
            }
        }

        public Image IconImage
        {
            get
            {
                return icon.Image;
            }

            set
            {
                icon.Image = value;
            }
        }

        public string TextIcon
        {
            get
            {
                return icon.Text;
            }

            set
            {
                icon.Text = value;
            }
        }

        public IconType IconType
        {
            get
            {
                return icon.Type;
            }

            set
            {
                icon.Type = value;
            }
        }

        public Color IconTextColor
        {
            get
            {
                return icon.TextColor;
            }

            set
            {
                icon.TextColor = value;
            }
        }

        ///
        /// events - Icon
        ///
        public event EventHandler<IconType> ChangeIconType
        {
            add
            {
                icon.ChangeType += value;
            }

            remove
            {
                icon.ChangeType -= value;
            }
        }

        public event EventHandler<Image> ChangeIconImage
        {
            add
            {
                icon.ChangeImage += value;
            }

            remove
            {
                icon.ChangeImage -= value;
            }
        }

        public event EventHandler<Font> ChangeIconFont
        {
            add
            {
                icon.ChangeFont += value;
            }

            remove
            {
                icon.ChangeFont -= value;
            }
        }

        public event EventHandler<Color> ChangeIconColor
        {
            add
            {
                icon.ChangeColor += value;
            }

            remove
            {
                icon.ChangeColor -= value;
            }
        }

        public event EventHandler<string> ChangeIconText
        {
            add
            {
                icon.ChangeText += value;
            }

            remove
            {
                icon.ChangeText -= value;
            }
        }

        public event EventHandler<Point> ChangeIconLocation
        {
            add
            {
                icon.ChangeLocation += value;
            }

            remove
            {
                icon.ChangeLocation -= value;
            }
        }

        public event EventHandler<Size> ChangeIconSizeImage
        {
            add
            {
                icon.ChangeSizeImage += value;
            }

            remove
            {
                icon.ChangeSizeImage -= value;
            }
        }

        public event PaintEventHandler IconPaint
        {
            add
            {
                icon.Paint += value;
            }

            remove
            {
                icon.Paint -= value;
            }
        }

        /// 
        /// events - Player
        /// 
        public event EventHandler<string> ChangePlayerName
        {
            add
            {
                player.ChangeName += value;
            }

            remove
            {
                player.ChangeName += value;
            }
        }

        public event EventHandler<int> ChangePlayerScore
        {
            add
            {
                player.ChangeScore += value;
            }

            remove
            {
                player.ChangeScore -= value;
            }
        }

        public event EventHandler<string> PlayerErrorScore
        {
            add
            {
                player.ErrorScore += value;
            }

            remove
            {
                player.ErrorScore -= value;
            }
        }

        private void UpdatePlayerToLabel()
        {
            int width;
            string playerName = PlayerName;
            label_PlayerDetail.Text = string.Format("{0} {1} {2}", Score, SpaceBetween, playerName);
            do
            {
                width = label_PlayerDetail.Width + MarginOfAllControls.Left * 2 + MarginOfAllControls.Right * 2;
                if (icon.Type == IconType.Image) width += icon.SizeImage.Width;
                else width += (int)icon.GetSizeOfText(CreateGraphics()).Width;
                if (MaximumSize.Width != 0 && MaximumSize.Width < width)
                {
                    playerName = playerName.Remove(playerName.Length - 1, 1);
                    label_PlayerDetail.Text = string.Format("{0} {1} {2}", Score, SpaceBetween, playerName);
                }
            } while (MaximumSize.Width != 0 && MaximumSize.Width < width);

        }

        private void UpdateLocationOfPlayerLabel()
        {
            Padding marg = MarginOfAllControls;
            label_PlayerDetail.Left = marg.Left;
            label_PlayerDetail.Top = marg.Top;
        }

        private void UpdatePanelSize()
        {
            Size size = label_PlayerDetail.Size;
            Height = size.Height + MarginOfAllControls.Top + MarginOfAllControls.Bottom;
            Width = size.Width + MarginOfAllControls.Left + MarginOfAllControls.Right;
            switch (IconType)
            {
                case IconType.Image:
                    Width += icon.SizeImage.Width;
                    break;
                case IconType.Text:
                case IconType.None:
                    SizeF sizef = icon.GetSizeOfText(CreateGraphics());
                    Width += Size.Round(sizef).Width;
                    break;
            }
            Width += MarginOfAllControls.Left + MarginOfAllControls.Right;
        }

        private void UpdateLocationOfIcon()
        {
            Point location = label_PlayerDetail.Location;
            location.X += label_PlayerDetail.Width + MarginOfAllControls.Right + MarginOfAllControls.Left;
            icon.Location = location;
        }

        private void UpdateSizeOfIcon()
        {
            if (icon.Image != null)
            {
                Size size = new Size();
                size.Height = label_PlayerDetail.Height;
                float yachas = (float)size.Height / icon.Image.Height;
                size.Width = (int)(Icon.Image.Width * yachas);
                if(icon.SizeImage != size) icon.SizeImage = size;
            }
        }

        /// <summary>
        /// מעדכן את הלייבל של הפלייר, האייקון והגודל שך הפאנל
        /// </summary>
        private void UpdateAll()
        {
            UpdateSizeOfIcon();
            UpdatePlayerToLabel();
            UpdateLocationOfPlayerLabel();
            UpdateLocationOfIcon();
            UpdatePanelSize();
            Invalidate();
        }

        private void UpdatePlayerEvents()
        {
            if(player!=null)
            {
                player.ChangeName += OnChangePlayerName;
                player.ChangeScore += OnChangePlayerScore;
            }
        }

        private void ResetPlayerEvents()
        {
            if (player != null)
            {
                player.ChangeName -= OnChangePlayerName;
                player.ChangeScore -= OnChangePlayerScore;
            }
        }

        private void UpdateIconEvents()
        {
            if (icon != null)
            {
                icon.ChangeColor += OnChangeIconColor;
                icon.ChangeFont += OnChangeIconFont;
                icon.ChangeImage += OnChangeIconImage;
                icon.ChangeLocation += OnChangeIconLocation;
                icon.ChangeSizeImage += OnChangeIconSize;
                icon.ChangeText += OnChangeIconText;
                icon.ChangeType += OnChangeIconType;
            }
        }

        private void ResetIconEvents()
        {
            if (icon != null)
            {
                icon.ChangeColor -= OnChangeIconColor;
                icon.ChangeFont -= OnChangeIconFont;
                icon.ChangeImage -= OnChangeIconImage;
                icon.ChangeLocation -= OnChangeIconLocation;
                icon.ChangeSizeImage -= OnChangeIconSize;
                icon.ChangeText -= OnChangeIconText;
                icon.ChangeType -= OnChangeIconType;
            }
        }

        private void OnChangeIconColor(object sender, Color e)
        {
            if(icon.Type== IconType.Text || icon.Type == IconType.None) Invalidate();
        }

        private void OnChangeIconFont(object sender, Font e)
        {
            if (icon.Type == IconType.Text || icon.Type == IconType.None) Invalidate();
        }

        private void OnChangeIconImage(object sender, Image e)
        {
            if(icon.Type == IconType.Image) UpdateAll();
        }

        private void OnChangeIconLocation(object sender, Point e)
        {
            Invalidate();
        }

        private void OnChangeIconSize(object sender, Size e)
        {
            if (icon.Type == IconType.Image) UpdateAll();
        }

        private void OnChangeIconText(object sender, string e)
        {
            if (icon.Type == IconType.Text) Invalidate();
        }

        private void OnChangeIconType(object sender, IconType e)
        {
            UpdateSizeOfIcon();
            UpdatePanelSize();
            Invalidate();
        }

        ///
        /// methods - on event
        ///
        private void OnChangePlayerName(object sender, string name)
        {
            OnChangePlayerName(name);
        }

        private void OnChangePlayerScore(object sender, int score)
        {
            OnChangePlayerScore(score);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (icon != null)
                icon.Paint_Icon(e);
        }

        private void OnBeforeChangePlayer(Player player)
        {
            ResetPlayerEvents();
            BeforeChangePlayer?.Invoke(this, player);
        }

        private void OnChangePlayer(Player player)
        {
            UpdatePlayerEvents();
            UpdateAll();
            ChangePlayer?.Invoke(this, player);
        }

        private void OnChangePlayerName(string name)
        {
            UpdateAll();
        }

        private void OnChangePlayerScore(int score)
        {
            if (score % 10 != Score % 10)
            {
                UpdateAll();
            }
            else
            {
                UpdatePlayerToLabel();
            }
        }

        private void OnBeforeChangeIcon(PlayerIcon icon)
        {
            ResetIconEvents();
            BeforeChangeIcon?.Invoke(this, icon);
        }

        private void OnChangeIcon(PlayerIcon icon)
        {
            UpdateIconEvents();
            UpdateAll();
            ChangeIcon?.Invoke(this, icon);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            icon.TextFont = Font;
            label_PlayerDetail.Font = Font;
            UpdateAll();
        }

    }
}
