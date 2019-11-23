using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Txy">int/float</typeparam>
    /// <typeparam name="TPoint">Point/PointF</typeparam>
    public interface ILocation<Txy, TPoint>
    {
        /// <summary>
        /// Get/Set location
        /// </summary>
        TPoint Location { get; set; }
        /// <summary>
        /// Get/Set left
        /// </summary>
        Txy Left { get; set; }
        /// <summary>
        /// Get/Set top
        /// </summary>
        Txy Top { get; set; }
        /// <summary>
        /// Get/Set right
        /// </summary>
        Txy Right { get; set; }
        /// <summary>
        /// Get/Set bottom
        /// </summary>
        Txy Bottom { get; set; }

        /// <summary>
        /// Occurs wnen location changed
        /// </summary>
        event EventHandler<TPoint> LocationChanged;
    }
}
