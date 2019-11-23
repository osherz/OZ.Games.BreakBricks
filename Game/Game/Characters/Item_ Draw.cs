using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Characters
{
    public abstract class Item_Draw : Item, IItemF, IVisible
    {
        #region Variables
        private Brush brush;
        private Pen pen;
        private Image image;
        private DrawStyle style;
        private bool autoSizeImage;
        private bool visible = true;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when bush changed
        /// </summary>
        public event EventHandler<Brush> BrushChanged;
        /// <summary>
        /// Occurs when pen changed
        /// </summary>
        public event EventHandler<Pen> PenChanged;
        /// <summary>
        /// Occurs when style changed
        /// </summary>
        public event EventHandler<DrawStyle> StyleChanged;
        /// <summary>
        /// Occurs when image chaned
        /// </summary>
        public event EventHandler<Image> ImageChanged;
        /// <summary>
        /// Occurs when autoSizeImahe changed;
        /// </summary>
        public event EventHandler<bool> AutoSizeImageChanged;
        /// <summary>
        /// Occurs when Visible changed
        /// </summary>
        public event EventHandler<bool> VisibleChanged;
        #endregion Events

        #region Raises events methods
        /// <summary>
        /// Raises the BrushChanged event
        /// </summary>
        /// <param name="newBrush"></param>
        protected virtual void OnBrushChanged(Brush newBrush)
        {

            BrushChanged?.Invoke(this, newBrush);
        }
        /// <summary>
        /// Raises the PenChanged event
        /// </summary>
        /// <param name="newPen"></param>
        protected virtual void OnPenChanged(Pen newPen)
        {

            PenChanged?.Invoke(this, newPen);
        }
        /// <summary>
        /// Raises the PenChanged event
        /// </summary>
        /// <param name="style"></param>
        protected virtual void OnStyleChanged(DrawStyle style)
        {

            StyleChanged?.Invoke(this, style);
        }
        /// <summary>
        /// Occurs when image changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newImage"></param>
        protected virtual void OnImageChanged(Image newImage)
        {

            ImageChanged?.Invoke(this, newImage);
        }
        /// <summary>
        /// Raise the AutoSizeImageChanged event
        /// </summary>
        /// <param name="autoImageChanged"></param>
        protected virtual void OnAutoSizeImageChanged(bool autoImage)
        {

            AutoSizeImageChanged?.Invoke(this, autoImage);
        }
        /// <summary>
        /// Raise the VisibleChanged event
        /// </summary>
        /// <param name="visible"></param>
        protected virtual void OnVisibleChanged(bool visible)
        {

            VisibleChanged?.Invoke(this, visible);
        }
        #endregion Raises events methods

        #region Properties
        public virtual Brush Brush
        {
            get
            {
                return brush;
            }

            set
            {
                brush = value;
                OnBrushChanged(Brush);
            }
        }

        public virtual Pen Pen
        {
            get
            {
                return pen;
            }

            set
            {
                pen = value;
                OnPenChanged(Pen);
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
                Size = Size;
                OnImageChanged(Image);
            }
        } 

        /// <summary>
        /// The draw shape style
        /// </summary>
        public virtual DrawStyle Style
        {
            get
            {
                return style;
            }

            set
            {
                style = value;
                OnStyleChanged(Style);
            }
        }

        public virtual bool AutoSizeImage
        {
            get
            {
                return autoSizeImage;
            }

            set
            {
                autoSizeImage = value;
                OnAutoSizeImageChanged(AutoSize);
                if (AutoSizeImage && Image != null)
                {
                    Size = UpdateSizeImage(Size);
                }
            }
        }

        public override SizeF Size
        {
            get
            {
                return base.Size;
            }

            set
            {
                if (AutoSizeImage && Image != null)
                {
                    SizeF size = UpdateSizeImage(value);
                    if(!size.Equals(Size))
                    {
                        value = size;
                    }
                }
                base.Size = value;
            }
        }

        public override float Width
        {
            get
            {
                return base.Width;
            }

            set
            {
                if (AutoSizeImage)
                {
                    SizeF size = UpdateSizeImage(new SizeF(value, value - 10)); //Because the methods return size by the big width or height
                    if(!size.Equals(Size))
                    {
                        Size = size;
                    }
                }
                else
                    base.Width = value;
            }
        }

        public override float Height
        {
            get
            {
                return base.Height;
            }

            set
            {
                if (AutoSizeImage)
                {
                    SizeF size = UpdateSizeImage(new SizeF(value - 10, value)); //Because the methods return size by the big width or height
                    if (!size.Equals(Size))
                    {
                        Size = size;
                    }
                }
                else
                    base.Height = value;
            }
        }


        /// <summary>
        /// Get/Set if the item will visible
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
                OnVisibleChanged(Visible);
            }
        }

        private SizeF UpdateSizeImage(SizeF size)
        {
            if(size.Width>size.Height)
            {
                return new SizeF(size.Width, Image.Height / (image.Width / size.Width));
            }
            else
            {
                return new SizeF(Image.Width / (Image.Height / size.Height), size.Height);
            }
        }
        #endregion Properties

        #region Draw methods
        /// <summary>
        /// Draw fill shape
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraX"></param>
        /// <param name="extraY"></param>
        /// <param name="extraWidth"></param>
        /// <param name="extraHeight"></param>
        protected abstract void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0);

        /// <summary>
        /// Draw not fill shape
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraX"></param>
        /// <param name="extraY"></param>
        /// <param name="extraWidth"></param>
        /// <param name="extraHeight"></param>
        protected abstract void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0);

        /// <summary>
        /// Draw image
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraX"></param>
        /// <param name="extraY"></param>
        /// <param name="extraWidth"></param>
        /// <param name="extraHeight"></param>
        protected virtual void DrawImage(Graphics g, float extraX, float extraY, float extraWidth, float extraHeight)
        {
            g.DrawImage(Image, Left + extraX, Top + extraY, Width + extraWidth, Height + extraHeight);
        }

        public override void Draw(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            if (Visible)
            {
                switch (Style)
                {
                    case DrawStyle.Fill:
                        DrawFill(g, extraX, extraY, extraWidth, extraHeight);
                        break;
                    case DrawStyle.NotFill:
                        DrawNotFill(g, extraX, extraY, extraWidth, extraHeight);
                        break;
                    case DrawStyle.Image:
                        DrawImage(g, extraX, extraY, extraWidth, extraHeight);
                        break;
                }
                base.Draw(g, extraX, extraY, extraWidth, extraHeight);
            }
        }
        #endregion Draw methods

        public override object Clone()
        {
            Item_Draw item = (Item_Draw)base.Clone();
            if(Pen!=null) item.Pen = (Pen)Pen.Clone();
            if(Brush!=null)item.Brush = (Brush)Brush.Clone();
            if(Image!=null) item.Image = (Image)Image.Clone();
            item.Visible = Visible;
            item.Style = Style;
            return item;
        }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }
    }
}
