using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    /// <summary>
    /// the object will move by this point
    /// </summary>
    /// <typeparam name="T">int/float</typeparam>
    /// <typeparam name="TPoint">Point/PointF</typeparam>
    public interface IDirection<T, TPoint>
    {
        #region Properties
        /// <summary>
        /// The direction that willl move by him
        /// </summary>
        TPoint Direction { get; set; }

        /// <summary>
        /// The direction that will move to by X
        /// </summary>
        T DirectionX { get; set; }

        /// <summary>
        /// The direction that will move to by Y
        /// </summary>
        T DirectionY { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Move by direction
        /// </summary>
        void Move();
        #endregion Methods

        #region Events
        event EventHandler<TPoint> DirectionChanged;
        #endregion Events
    }

    public interface IDirection : IDirection<int, Point> { }

    public interface IDirectionF : IDirection<float, PointF> { }

}
