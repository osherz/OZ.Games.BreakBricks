using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public enum ImageStyle
    {
        Stretch,
        Particle
    }

    public class StatusBar : Item_Draw
    {
        #region Variables
        /// <summary>
        /// By this var the rectangle will fill
        /// </summary>
        private float precent;
        /// <summary>
        /// Just if Style is Image
        /// </summary>
        private ImageStyle imageStyle;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when Precent changed
        /// </summary>
        public event EventHandler<float> PrecentChanged;
        /// <summary>
        /// Occurs when ImageStyle changed
        /// </summary>
        public event EventHandler<ImageStyle> ImageStyleChanged;
        #endregion Events

        #region Raise events methods
        /// <summary>
        /// Raise the PrecentChanged event
        /// </summary>
        /// <param name="precent"></param>
        protected virtual void OnPrecentChanged(float precent)
        {

            PrecentChanged?.Invoke(this, precent);
        }
        /// <summary>
        /// Raise the ImageStyleChanged event
        /// </summary>
        /// <param name="precent"></param>
        protected virtual void OnImageStyleChanged(ImageStyle imageStyle)
        {

            ImageStyleChanged?.Invoke(this, imageStyle);
        }

        #endregion Raise events methods

        #region Properties
        public virtual float Precent
        {
            get
            {
                return precent;
            }

            set
            {
                if (value >= 0 && value <= 1)
                {
                    precent = value;
                    OnPrecentChanged(Precent);
                }
            }
        }

        /// <summary>
        /// Get/Set Pen of frame
        /// </summary>
        public override Pen Pen
        {
            get
            {
                return base.Pen;
            }

            set
            {
                base.Pen = value;
            }
        }

        public virtual ImageStyle ImageStyle
        {
            get
            {
                return imageStyle;
            }

            set
            {
                imageStyle = value;
                OnImageStyleChanged(ImageStyle);
            }
        }

        /// <summary>
        /// Can't be not fill
        /// </summary>
        public override DrawStyle Style
        {
            get
            {
                return base.Style;
            }

            set
            {
                if (value != DrawStyle.NotFill)
                {
                    base.Style = value;
                }
            }
        }
        #endregion Properties

        #region Methods
        #endregion Methods

        #region Draw methods
        private PointF LocationOfFill()
        {
            return new PointF(Left + Pen.Width, Top + Pen.Width);
        }

        private SizeF SizeOfFill()
        {
            return new SizeF(Width - Pen.Width * 2, Height - Pen.Width * 2);
        }

        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            DrawNotFill(g, extraX, extraY, extraWidth, extraHeight);
            PointF locationOfFill = LocationOfFill();
            SizeF sizeOfFill = SizeOfFill();
            g.FillRectangle(Brush, locationOfFill.X + extraX, locationOfFill.Y + extraY, (sizeOfFill.Width + extraWidth) * Precent, sizeOfFill.Height + extraHeight);
        }

        protected override void DrawImage(Graphics g, float extraX, float extraY, float extraWidth, float extraHeight)
        {
            DrawNotFill(g, extraX, extraY, extraWidth, extraHeight);
            PointF locationOfFill = LocationOfFill();
            locationOfFill.X += extraX;
            locationOfFill.Y += extraY;
            SizeF sizeOfFill = SizeOfFill();
            sizeOfFill.Width += extraWidth;
            sizeOfFill.Height += extraHeight;
            if(ImageStyle == ImageStyle.Stretch)
            {
                g.DrawImage(Image, locationOfFill.X, locationOfFill.Y, sizeOfFill.Width * Precent, sizeOfFill.Height);
            }
            else if (ImageStyle == ImageStyle.Particle)
            {
                int precent = (int)(Precent * 100);
                float width = sizeOfFill.Width / 100;
                while (precent > 0)
                {
                    g.DrawImage(Image, locationOfFill.X, locationOfFill.Y, width, sizeOfFill.Height);
                    locationOfFill.X += width;
                    precent--;
                }
            }
            //base.DrawImage(g, extraX, extraY, extraWidth, extraHeight);
        }

        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            g.DrawRectangle(Pen, Left + extraX, Top + extraY, Width + extraWidth, Height + extraHeight);
        }
        #endregion Draw methods
        protected override Item Copy()
        {
            throw new NotImplementedException();
        }

    }
}
