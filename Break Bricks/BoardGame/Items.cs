using Break_Bricks.BoardGame.Interface;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.BoardGame
{
    public abstract class Items: Game.Characters.Item
    {
        #region Variables
        /// <summary>
        /// The user's plank
        /// </summary>
        private IPlank plank;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs before plank change
        /// </summary>
        public event EventHandler<IPlank> PlankBeforeChange;
        /// <summary>
        /// Occurs when plank changed
        /// </summary>
        public event EventHandler<IPlank> PlankChanged;
        #endregion Events

        #region Raises events methods
        /// <summary>
        /// Raises the PlankBeforeChanged event
        /// </summary>
        /// <param name="plank"></param>
        protected virtual void OnPlankBeforeChanged(IPlank plank)
        {

            PlankBeforeChange?.Invoke(this, plank);
        }
        /// <summary>
        /// Raises the PlankChanged event
        /// </summary>
        /// <param name="plank"></param>
        protected virtual void OnPlankChanged(IPlank plank)
        {

            PlankChanged?.Invoke(this, plank);
        }
        #endregion Raises events methods

        #region Properties
        /// <summary>
        /// The user's plank
        /// </summary>
        public IPlank Plank
        {
            get
            {
                return plank;
            }

            set
            {
                if (Plank != null)
                {
                    ResetPlankEvents();
                }
                OnPlankBeforeChanged(Plank);
                plank = value;
                if (Plank != null)
                {
                    RestartPlankEvents();
                }
                OnPlankChanged(Plank);
            }
        }
        #endregion Properties

        #region Reset/Restart properties events
        /// <summary>
        /// Occurs before plank changed
        /// </summary>
        protected virtual void ResetPlankEvents()
        {
            if (Plank != null)
            {
            }
        }

        /// <summary>
        /// Occurs after plank changed
        /// </summary>
        protected virtual void RestartPlankEvents()
        {
            if (Plank != null)
            {
            }
        }
        #endregion Reset/Restart properties events

    }
}
