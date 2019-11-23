using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    [Flags]
    public enum Direction
    {
        None = 0x0,
        Top = 0x1,
        Bottom= 0x2,
        Left = 0x4,
        Right= 0x8,
    }
}
