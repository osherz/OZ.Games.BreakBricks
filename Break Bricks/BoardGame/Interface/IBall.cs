using Game.Characters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.BoardGame.Interface
{
    public interface IBall : IMoveF, IMoveBy<SizeF>, IItemF, IGetAreaF, IHit<Region>, ISpeedF, ITimer, ICloneable, IDirectionF
    {
    }
}
