using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface IMoveDirection<T>
    {
        /// <summary>
        /// Move up
        /// </summary>
        void GoUp();
        /// <summary>
        /// Move down
        /// </summary>
        void GoDown();
        /// <summary>
        /// Move right
        /// </summary>
        void GoRight();
        /// <summary>
        /// Move left
        /// </summary>
        void GoLeft();
        /// <summary>
        /// THE PARAMTER ARE NOT LOCATION!!
        /// x and y using for move's direction
        /// for example:
        /// 1. if you want character move down: x=0 - y=1.
        /// 2. move left: x=-1, y=0.
        /// 3. move up-left: x=-1, y=-1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void Move(T x, T y);
    }

    public interface IMoveTo<in TPoint>
    {
        /// <summary>
        /// Move to spesific point
        /// </summary>
        /// <param name="point"></param>
        void MoveTo(TPoint point);
    }

    public interface IMoveBy<in TSize>
    {
        /// <summary>
        /// Move by specific size
        /// </summary>
        /// <param name="sizeToMove"></param>
        void MoveBy(TSize sizeToMove);
    }

    public interface IMoveEvent<TEventArgs>
    {
        /// <summary>
        /// Occurs when nove
        /// </summary>
        event EventHandler<TEventArgs> Moved;
    }

    public interface IMoveEvent : IMoveEvent<EventArgs>
    { }

    public interface IMove<T, TEventArgs> : IMoveDirection<T>, IMoveEvent<TEventArgs>
    { }

    public interface IMove: IMove<int, Point>
    { }

    public interface IMoveF : IMove<float, PointF>
    { }

}
