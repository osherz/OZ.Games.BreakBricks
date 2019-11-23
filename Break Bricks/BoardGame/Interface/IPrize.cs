using Game.Characters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.BoardGame.Interface
{
    public interface IPrize<TBoard> : IItemF, IMoveAutoF, IHitByArea<RectangleF>, ICloneable, IRefCount
    {
        /// <summary>
        /// Active prize
        /// </summary>
        void Active(TBoard board);

        /// <summary>
        /// Finish the effect of prize
        /// </summary>
        void FinishActive();

        void Pause();

        void Continue();

        #region Events
        /// <summary>
        /// Occurs when prize activated
        /// </summary>
        event EventHandler Activated;
        /// <summary>
        /// Occurs when prize finish activated
        /// </summary>
        event EventHandler FinishedActive;
        /// <summary>
        /// Occurs when MaxOfPrizeCanFall changed
        /// </summary>
        event EventHandler<int> MaxOfPrizeCanFallChanged;
        /// <summary>
        /// Occurs when PrizeFell
        /// </summary>
        event EventHandler<int> SumOfPrizeFellChanged;

        event EventHandler Paused;

        event EventHandler Continued;
        #endregion Events

        #region Properties
        /// <summary>
        /// Max number of prizes that can fall from each prize
        /// </summary>
        int MaxOfPrizeCanFall { get; set; }
        /// <summary>
        /// Sum of prize that fell
        /// </summary>
        int SumOfPrizeFell { get; set; }

        bool IsPause { get; }
        #endregion Properties

    }
}
