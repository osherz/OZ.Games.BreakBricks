using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.BoardGame.Interface
{
    public interface IBorder : IItemF, IGetRectangleF, IGetRegion, ICloneable
    {
    }
}
