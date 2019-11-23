using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    /// <summary>
    /// Know draw, location and size
    /// </summary>
    /// <typeparam name="T">int/float</typeparam>
    /// <typeparam name="TPoint">Point/PointF</typeparam>
    /// <typeparam name="TSize">Size/SizeF</typeparam>
    public interface IItem<T, TPoint, TSize> : IDraw<T>, ILocation<T, TPoint>, ISize<T, TSize>
    {
        object Tag { get; set; }

        event EventHandler<object> TagChanged;
    }

    /// <summary>
    /// Know draw, location and size, all int
    /// </summary>
    public interface IItem : IItem<int, Point, Size> { }

    /// <summary>
    /// Know draw, location and size, all float
    /// </summary>
    public interface IItemF : IItem<float, PointF, SizeF> { }

}
