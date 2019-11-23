using Break_Bricks.BoardGame.Interface;
using Break_Bricks.Levels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Characters;
using System.Drawing;

namespace Break_Bricks.Levels.Prizes
{
    public class Prize_AddTurns<TBoard> : Prize<TBoard>, IPrize<TBoard>
        where TBoard : IBoard
    {
        #region Variables
        private int extraTurns;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when ExtraTurns changed
        /// </summary>
        public event EventHandler<int> ExtraTurnsChanged;
        #endregion Events

        #region Raise events methods
        /// <summary>
        /// Raise the ExtraTurnsChanged event
        /// </summary>
        /// <param name="extraTurns"></param>
        protected virtual void OnExtraTurnsChanged(int extraTurns)
        {

            ExtraTurnsChanged?.Invoke(this, extraTurns);
        }
        #endregion Raise events methods

        #region Properties
        public int ExtraTurns
        {
            get
            {
                return extraTurns;
            }

            set
            {
                extraTurns = value;
                OnExtraTurnsChanged(ExtraTurns);
            }
        }
        #endregion Properties

        #region Methods
        public override void Active(TBoard board = default(TBoard))
        {
            if (board == null) board = Board;
            board.Turns += ExtraTurns;
            base.Active(board);
        }
        #endregion Methods

        protected override Item Copy()
        {
            Prize_AddTurns<TBoard> prize = new Prizes.Prize_AddTurns<TBoard>();
            CopyTo(prize);
            return prize;
        }

        protected void CopyTo(Prize_AddTurns<TBoard> prize)
        {
            prize.ExtraTurns = ExtraTurns;
        }

        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            throw new NotImplementedException();
        }

        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            throw new NotImplementedException();
        }
    }
}
