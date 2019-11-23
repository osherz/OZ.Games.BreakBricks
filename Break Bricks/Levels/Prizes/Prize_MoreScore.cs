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
    public class Prize_MoreScore<TBoard> : Prize<TBoard>
        where TBoard : IBoard
    {
        #region Variables
        /// <summary>
        /// The score that will extra
        /// </summary>
        private int extraScore;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when ExtraScore changed
        /// </summary>
        public event EventHandler<int> ExtraScoreChanged;
        #endregion Events

        #region Raise events methods
        /// <summary>
        /// Raise the ExtraScoreChaned event
        /// </summary>
        /// <param name="newExtraScore"></param>
        protected virtual void OnExtraScoreChanged(int newExtraScore)
        {

            ExtraScoreChanged?.Invoke(this, newExtraScore);
        }
        #endregion Raise events methods

        #region Properties
        /// <summary>
        /// The score that will extra
        /// </summary>
        public int ExrtaScore
        {
            get
            {
                return extraScore;
            }

            set
            {
                extraScore = value;
                OnExtraScoreChanged(ExrtaScore);
            }
        }
        #endregion Properties

        public override void Active(TBoard board = default(TBoard))
        {
            if (board == null) board = Board;
            board.Score += extraScore;
            base.Active(board);
        }

        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            throw new NotImplementedException();
        }

        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            throw new NotImplementedException();
        }

        protected override Game.Characters.Item Copy()
        {
            Prize_MoreScore<TBoard> prize = new Prizes.Prize_MoreScore<TBoard>();
            CopyTo(prize);
            return prize;
        }

        protected void CopyTo(Prize_MoreScore<TBoard> prize)
        {
            prize.ExrtaScore = ExrtaScore;
        }
    }
}
