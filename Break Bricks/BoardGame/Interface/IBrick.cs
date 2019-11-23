using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.BoardGame.Interface
{
    public interface IBrick : IItemF, IGetRectangleF, ICloneable
    {
        #region Properties
        /// <summary>
        /// מכיל את מספר השלבים שיש ללבנה עד שתשבר לגמרי
        /// </summary>
        int SumOfLvls { get; }

        /// <summary>
        /// Is brick broke completly
        /// </summary>
        bool IsBrokeCompletly { get; set; }

        /// <summary>
        /// Is brick can to break
        /// </summary>
        bool IsCanToBreak { get; set; }
        #endregion Properties

        #region Events
        /// <summary>
        /// Occurs when one lvl of brick broke
        /// </summary>
        event EventHandler BrokeLvl;
        /// <summary>
        /// Occurs when the brick broke completly
        /// </summary>
        event EventHandler BrokedCompletly;
        #endregion Events

        #region Methods
        /// <summary>
        /// Break one lvl of brick
        /// </summary>
        void BreakLvl();
        /// <summary>
        /// Break all lvls of brick
        /// </summary>
        void BreakCompletly();

        void Reset();
        #endregion Methods

    }
}
