using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game.Classes
{
    public class PlayerIcon
    {
        /// 
        /// events
        /// 
        public event EventHandler<IconType> ChangeType;
        public event EventHandler<Image> ChangeImage;
        public event EventHandler<Font> ChangeFont;
        public event EventHandler<Color> ChangeColor;
        public event EventHandler<string> ChangeText;
        public event EventHandler<Point> ChangeLocation;
        public event EventHandler<Size> ChangeSizeImage;
        public event PaintEventHandler Paint;
        /// 
        /// variable
        /// 
        private Image image;
        private string text;
        public const string DefaultText = "II";
        private Font textFont;
        private Color textColor;
        private IconType type;
        private Point location;
        private Size sizeImage;

        public PlayerIcon()
        {
            TextColor = Color.Black;
            TextFont = new Font("David", 20);
        }

        public virtual IconType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                ChangeType?.Invoke(this, type);
            }
        }

        public virtual Image Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                ChangeImage?.Invoke(this, image);
            }
        }

        /// <summary>
        /// מציג עד 2 תווים
        /// </summary>
        public virtual string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                ChangeText?.Invoke(this, text);
            }
        }

        public virtual Font TextFont
        {
            get
            {
                return textFont;
            }

            set
            {
                textFont = value;
                ChangeFont?.Invoke(this, textFont);
            }
        }

        public virtual Color TextColor
        {
            get
            {
                return textColor;
            }

            set
            {
                textColor = value;
                ChangeColor?.Invoke(this, TextColor);
            }
        }

        public virtual Point Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                ChangeLocation?.Invoke(this, Location);
            }
        }

        public virtual Size SizeImage
        {
            get
            {
                return sizeImage;
            }

            set
            {
                sizeImage = value;
                ChangeSizeImage?.Invoke(this, SizeImage);
            }
        }

        public PlayerIcon Clone()
        {
            PlayerIcon icon = new PlayerIcon();
            icon.Image = Image;
            icon.Text = Text;
            icon.TextColor = TextColor;
            icon.TextFont = TextFont;
            icon.Type = Type;
            icon.Location = Location;
            icon.SizeImage = SizeImage;
            return icon;
        }

        public SizeF GetSizeOfText(Graphics g)
        {
            if (Type == IconType.Text)
                return g.MeasureString(Text, TextFont);
            else
            {
                return g.MeasureString(DefaultText, TextFont);

            }
        }

        public virtual void Paint_Icon(PaintEventArgs e)
        {
            switch(Type)
            {
                case IconType.Image:
                    e.Graphics.DrawImage(Image, new Rectangle(Location, SizeImage));
                    break;
                case IconType.Text:
                    string str;
                    if (text.Length > 2) str = text.Substring(0, 2);
                    else str = text;
                    e.Graphics.DrawString(str, TextFont, new SolidBrush(TextColor), Location);
                    break;
                case IconType.None:
                    e.Graphics.DrawString(DefaultText, TextFont, new SolidBrush(TextColor), Location);
                    break;
            }
            Paint?.Invoke(this, e);
        }
    }
}
