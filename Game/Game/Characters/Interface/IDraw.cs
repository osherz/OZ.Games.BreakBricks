using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface IDraw<T>
    {
        /// <summary>
        /// Add location to original location, without change him, just for drawing
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraX"></param>
        /// <param name="extraY"></param>
        void DrawWithExtraLocation(Graphics g, T extraX, T extraY);
        /// <summary>
        /// Add size to original size, without change him, just for drawing
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraWidth"></param>
        /// <param name="extraHeight"></param>
        void DrawWithExtraSize(Graphics g, T extraWidth, T extraHeight);
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraX"></param>
        /// <param name="extraY"></param>
        /// <param name="extraWidth"></param>
        /// <param name="extraHeight"></param>
        void Draw(Graphics g, T extraX = default(T), T extraY = default(T), T extraWidth = default(T), T extraHeight = default(T));


        /// <summary>
        /// Occurs after node drew
        /// </summary>
        event EventHandler Drew;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        event EventHandler LookChanged;
    }

    public interface IDraw
    {
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);


        /// <summary>
        /// Occurs after node drew
        /// </summary>
        event EventHandler Drew;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        event EventHandler LookChanged;
    }

}
