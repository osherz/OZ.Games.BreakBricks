using Break_Bricks.BoardGame.Interface;
using Break_Bricks.Levels.Interface;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Break_Bricks.Levels.Prizes
{
    public class Prize_MoreBalls<TBoard> : Prize<TBoard>, IPrize<TBoard>
        where TBoard: IBoard
    {
        #region Variables
        /// <summary>
        /// Sum of ball that will extra
        /// </summary>
        private int sumOfExtraBalls = 1;
        /// <summary>
        /// Font for draw sum of balls
        /// </summary>
        private Font font;

        public Prize_MoreBalls() : base()
        {

        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when sum of extra balls changed
        /// </summary>
        public event EventHandler<int> SumOfExtraBallsChanged;
        /// <summary>
        /// Occurs when font changed
        /// </summary>
        public event EventHandler<Font> FontChnaged;
        #endregion Events

        #region Raise events methods
        /// <summary>
        /// Raise the SumOfExtraBallsChanged event
        /// </summary>
        /// <param name="sumOfExtraBalls"></param>
        protected virtual void OnSumOfExtraBallChanged(int sumOfExtraBalls)
        {

            SumOfExtraBallsChanged?.Invoke(this, sumOfExtraBalls);
        }

        /// <summary>
        /// Raise the FontChanged event
        /// </summary>
        /// <param name="newFont"></param>
        protected virtual void OnFontChanged(Font newFont)
        {

            FontChnaged?.Invoke(this, newFont);
        }
        #endregion Raise events methods

        #region Properties
        public int SumOfExtraBalls
        {
            get
            {
                return sumOfExtraBalls;
            }

            set
            {
                if (SumOfExtraBalls > 0)
                {
                    sumOfExtraBalls = value;
                    OnSumOfExtraBallChanged(SumOfExtraBalls);
                }
            }
        }

        /// <summary>
        /// Font for draw sum of balls
        /// </summary>
        public Font Font
        {
            get
            {
                return font;
            }

            set
            {
                font = value;
                OnFontChanged(Font);
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Active the prize
        /// </summary>
        /// <param name="board"></param>
        public override void Active(TBoard board = default(TBoard))
        {
            if (board == null) board = Board;
            board.AddBall(SumOfExtraBalls);
            base.Active(board);
            OnFinishedActive(new EventArgs());
        }

        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            g.FillEllipse(Brush, Left + extraX, Top + extraY, Board.Ball.Width + extraWidth, Board.Ball.Height + extraHeight);
            DrawText(g, Width + extraX, Height + extraY, extraWidth, extraHeight);
        }

        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            g.DrawEllipse(Pen, Left + extraX, Top + extraY, Width + extraWidth, Height + extraHeight);
            DrawText(g, Width + extraX, Height + extraY, extraWidth, extraHeight);
        }

        private void DrawText(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            g.DrawString("+" + SumOfExtraBalls.ToString(), Font, Brush,
                new PointF(Left + extraX, Top + extraY));
        }

        protected override Game.Characters.Item Copy()
        {
            Prize_MoreBalls<TBoard> prize = new Prize_MoreBalls<TBoard>();
            CopyTo(prize);
            return prize;
        }

        protected virtual void CopyTo(Prize_MoreBalls<TBoard> prize)
        {
            prize.SumOfExtraBalls = SumOfExtraBalls;
            if(Font!=null) prize.Font = (Font)Font.Clone();
        }
        #endregion Methods


    }
}
