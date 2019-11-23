using Break_Bricks.Levels.Interface;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.Levels.Prizes
{
    public abstract class PrizeBar : Item_Draw
    {
        #region Variables
        private IOnTime prize;
        private StatusBar bar;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs before Prize will change
        /// </summary>
        public event EventHandler<IOnTime> PrizeBeforeChange;
        /// <summary>
        /// Occurs when Prize changed
        /// </summary>
        public event EventHandler<IOnTime> PrizeChanged;
        #endregion Events

        #region Raise events methods
        /// <summary>
        /// Raise the PrizeBeforeChange event
        /// </summary>
        /// <param name="prize"></param>
        protected virtual void OnPrizeBeforeChange(IOnTime prize)
        {

            PrizeBeforeChange?.Invoke(this, prize);
        }

        /// <summary>
        /// Raise the PrizeChanged event
        /// </summary>
        /// <param name="prize"></param>
        protected virtual void OnPrizeChanged(IOnTime prize)
        {

            PrizeChanged?.Invoke(this, prize);
        }
        #endregion Raise events methods

        #region Reset/Restart events
        #endregion Reset/Restart events

        #region Properties
        #endregion Properties

        #region Methods
        #endregion Methods

    }
}
