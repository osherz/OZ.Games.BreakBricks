using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.BoardGame.Interface
{
    public interface ILevel<TBoard> : IItemF, IGetRectangleF
    {
        #region Properties
        IPlank Plank { get; set; }
        TBoard ParentBoard { get; set; }
        #endregion Properties

        #region Events
        /// <summary>
        /// Occurs before plank changed
        /// </summary>
        event EventHandler<IPlank> PlankBeforeChange;
        /// <summary>
        /// Occurs wnen plank changed
        /// </summary>
        event EventHandler<IPlank> PlankChanged;
        /// <summary>
        /// Occurs before board changed
        /// </summary>
        event EventHandler<TBoard> ParentBeforeChange;
        /// <summary>
        /// Occurs wnen board changed
        /// </summary>
        event EventHandler<TBoard> ParentChanged;
        /// <summary>
        /// Occurs when level finished
        /// </summary>
        event EventHandler LevelFinished;
        /// <summary>
        /// Occurs when brick broke
        /// </summary>
        event EventHandler<IBrick> BrickBroke;
        /// <summary>
        /// Occurs when prize generated
        /// </summary>
        event EventHandler<IPrize<TBoard>> PrizeGenerated;
        #endregion Events

        #region Methods
        Direction CheckIfBallHitBricks(IBall ball);

        void StartLevel();

        void Pause();

        void Continue();

        void ClearFallPrize();
        #endregion Methods
    }
}
