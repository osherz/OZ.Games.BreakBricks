using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface IHitByXY<in T>
    {
        /// <summary>
        /// Check if hit by x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool IsHit(T x, T y);
    }

    public interface IHitByPoint<in T> : IHitByArea<T>
    {
    }

    public interface IHitByArea<in T>
    {
        /// <summary>
        /// Check if hit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsHit(T value);
    }

    public interface IHit<TEventArgs> : IHitByXY<int>, IHitByXY<float>, IHitByPoint<Point>, IHitByPoint<PointF>,
        IHitByArea<Rectangle>, IHitByArea<RectangleF>, IHitByArea<Region>, IHitByArea<GraphicsPath>,
        IHitEvent<TEventArgs>
    { }

    public interface IHit : IHit<EventArgs>
    { }

    public interface IHitEvent<T>
    {
        /// <summary>
        /// Occurs when hitted
        /// </summary>
        event EventHandler<T> Hitted;
    }

    public interface IHitEvent : IHitEvent<EventArgs>
    {

    }


}
