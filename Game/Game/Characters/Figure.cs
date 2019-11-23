using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public abstract class Figure : Item_Draw, IItemF, IGetRectangleF, IGetRegion, IHit<Region>
    {
        #region Event
        /// <summary>
        /// Occurs when hitted
        /// </summary>
        public event EventHandler<Region> Hitted;
        #endregion Event

        #region Raises events methods
        /// <summary>
        /// Reises the Hitted event
        /// </summary>
        /// <param name="regionHitted"></param>
        protected virtual void OnHitted(Region regionHitted)
        {

            Hitted?.Invoke(this, regionHitted);
        }
        #endregion Raises events methods

        #region Get area methods
        public virtual RectangleF GetRectangleF()
        {
            return new RectangleF(Location, Size);
        }

        public virtual Region GetRegion()
        {
            return new Region(GetRectangleF());
        }
        #endregion Get area methods

        #region IsHit methods
        public virtual bool IsHit(Rectangle rect)
        {
            if (GetRegion().IsVisible(rect))
            {
                OnHitted(new Region(rect));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if hit by reg and figure rectangle
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        public virtual bool IsHit(Region reg)
        {
            if (reg.IsVisible(GetRectangleF()))
            {
                OnHitted(reg);
                return true;
            }
            return false;

        }

        /// <summary>
        /// Check if hit by path and figure reg.
        /// check all points of path, with foreach
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        public virtual bool IsHit(GraphicsPath path)
        {
            PointF[] points = path.PathPoints;
            Region reg = GetRegion();
            foreach(PointF point in points)
            {
                if (reg.IsVisible(point))
                {
                    {
                        OnHitted(new Region(path));
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual bool IsHit(RectangleF rectF)
        {
            if (GetRegion().IsVisible(rectF))
            {
                OnHitted(new Region(rectF));
                return true;
            }
            return false;
        }

        public virtual bool IsHit(PointF pointF)
        {
            return IsHit(pointF.X, pointF.Y);
        }

        public virtual bool IsHit(Point point)
        {
            if (GetRegion().IsVisible(point))
            {
                OnHitted(new Region(new RectangleF(point.X, point.Y, 0.1f, 0.1f)));
                return true;
            }
            return false;
        }

        public virtual bool IsHit(float x, float y)
        {
            if (GetRegion().IsVisible(x,y))
            {
                OnHitted(new Region(new RectangleF(x, y, 0.1f, 0.1f)));
                return true;
            }
            return false;
        }

        public virtual bool IsHit(int x, int y)
        {
            if (GetRegion().IsVisible(x, y))
            {
                OnHitted(new Region(new RectangleF(x, y, 0.1f, 0.1f)));
                return true;
            }
            return false;
        }
        #endregion IsHit methods
    }
}
