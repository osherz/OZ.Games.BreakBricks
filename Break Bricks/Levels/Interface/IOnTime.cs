using Break_Bricks.BoardGame.Interface;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.Levels.Interface
{
    public interface IOnTime: IItemF
    {
        #region Properties
        int TimeOfEffect { get; set; }
        int TimeLeftOfEffect { get; set; }
        #endregion Properties

        #region Methods
        #endregion Methods

        #region Events
        event EventHandler<int> TimeOfEffectChanged;
        event EventHandler<int> TimeLeftOfEffectChanged;
        #endregion Events
    }
}
