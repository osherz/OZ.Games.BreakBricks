using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Characters
{
    public class Ball : GameObject, IGetAreaF
    {
        #region Variables
        #endregion Variables

        #region Events
        #endregion Events

        #region Raises events methods
        #endregion Raises events methods

        #region Properties
        #endregion Properties

        #region Methods
        /// <summary>
        /// Draw fill ball
        /// </summary>
        /// <param name="g"></param>
        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            g.FillEllipse(Brush, Left + extraX, Top + extraY, Width + extraWidth, Height + extraHeight);
        }

        /// <summary>
        /// Draw not fill ball
        /// </summary>
        /// <param name="g"></param>
        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            g.DrawEllipse(Pen, Left + extraX, Top + extraY, Width + extraWidth, Height + extraHeight);
        }

        public override Region GetRegion()
        {
            return new Region(GetPath());
        }

        public GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            path.AddEllipse(GetRectangleF());
            return path;
        }

        protected override Item Copy()
        {
            Ball ball =  new Ball();
            CopyTo(ball);
            return ball;
        }

        protected void CopyTo(Ball item)
        {
            
        }
        #endregion Methods
    }
}
