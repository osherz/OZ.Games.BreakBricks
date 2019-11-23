using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface IMoveAuto<T, TPoint> : ISpeed<T>, IMoveEvent<TPoint>
    {
        #region Properties
        #endregion

        #region Behavior
        void StartMove();
        void StartMove(Direction direction);
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
        void StartMove(T x, T y);
        void StopMove();
        #endregion

        #region Events

        #endregion
    }

    public interface IMoveAuto : IMoveAuto<int, Point>
    { }

    public interface IMoveAutoF : IMoveAuto<float, PointF>
    { }
}
