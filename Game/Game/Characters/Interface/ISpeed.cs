using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface ISpeed<T> : ISpeedEvent<T>
    {
        T Speed { get; set; }
    }

    public interface ISpeed : ISpeed<int>
    { }

    public interface ISpeedF : ISpeed<float>
    { }

    public interface ISpeedEvent<T>
    {
        event EventHandler<T> SpeedChanged;
    }
}
