using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">int/float</typeparam>
    /// <typeparam name="TSize">Size/SizeF</typeparam>
    public interface ISize<T, TSize>
    {
        /// <summary>
        /// Get/Set size
        /// </summary>
        TSize Size { get; set; }
        /// <summary>
        /// Get/Set width
        /// </summary>
        T Width { get; set; }
        /// <summary>
        /// Get/Set height
        /// </summary>
        T Height { get; set; }
        /// <summary>
        /// If change size relation the containing
        /// </summary>
        bool AutoSize { get; set; }

        /// <summary>
        /// Occurs when size Changed
        /// </summary>
        event EventHandler<TSize> SizeChanged;
        /// <summary>
        /// Occurs when AutoSize changed
        /// </summary>
        event EventHandler<bool> AutoSizeChanged;
    }
}
