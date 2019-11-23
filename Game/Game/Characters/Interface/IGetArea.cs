using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface IGetArea : IGetRectangle, IGetRegion, IGetPath
    {
    }

    public interface IGetAreaF : IGetRectangleF, IGetRegion, IGetPath
    {
    }

    public interface IGetRectangle
    {
        Rectangle GetRectangle();
    }

    public interface IGetRectangleF
    {
        RectangleF GetRectangleF();
    }

    public interface IGetRegion
    {
        Region GetRegion();
    }

    public interface IGetPath
    {
        GraphicsPath GetPath();
    }

}
