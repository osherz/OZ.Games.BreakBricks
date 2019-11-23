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
    public class Prize_MoreSpeed<TBoard> : PrizeOnTime<TBoard>, IPrize<TBoard>
        where TBoard : IBoard
    {
        #region Variables
        /// <summary>
        /// The orginal speed will muliiply by this number
        /// </summary>
        private float multiplySpeed;
        /// <summary>
        /// Save the ld speed
        /// </summary>
        private float oldSpeed;
        /// <summary>
        /// This object will speed more
        /// </summary>
        private ISpeedF objectWillSpeed;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when the multiplySpeed changed
        /// </summary>
        public event EventHandler<float> MultiplySpeedChanged;
        /// <summary>
        /// Occurs when OldSpeedChanged
        /// </summary>
        protected static event EventHandler<float> OldSpeedChanged;
        /// <summary>
        /// Occurs when the object changed
        /// </summary>
        public event EventHandler<ISpeedF> ObjectWillSpeedChanged;
        #endregion Events

        #region Raise events methods
        /// <summary>
        /// Raise the MultiplySpeedChanged event
        /// </summary>
        /// <param name="newMultiplyChanged"></param>
        protected virtual void OnMultiplySpeedChanged(float newMultiplyChanged)
        {

            MultiplySpeedChanged?.Invoke(this, newMultiplyChanged);
        }
        /// <summary>
        /// Raise the OldSpeedChanged event
        /// </summary>
        /// <param name="oldSpeed"></param>
        protected virtual void OnOldSpeedChanged(float oldSpeed)
        {

            OldSpeedChanged?.Invoke(this, oldSpeed);
        }
        /// <summary>
        /// Raise the ObjectWillSpeedChanged event
        /// </summary>
        /// <param name="objectWillSpeed"></param>
        protected virtual void OnObjectWillSpeedChanged(ISpeedF objectWillSpeed)
        {

            ObjectWillSpeedChanged?.Invoke(this, objectWillSpeed);
        }
        #endregion Raise events methods

        #region Properties
        public float MultiplySpeed
        {
            get
            {
                return multiplySpeed;
            }

            set
            {
                multiplySpeed = value;
                OnMultiplySpeedChanged(MultiplySpeed);
            }
        }

        public float OldSpeed
        {
            get
            {
                return oldSpeed;
            }

            private set
            {
                oldSpeed = value;
                OnOldSpeedChanged(OldSpeed);
            }
        }

        /// <summary>
        /// The speed of this object will multiply
        /// </summary>
        public ISpeedF ObjectWillSpeed
        {
            get
            {
                return objectWillSpeed;
            }

            set
            {
                objectWillSpeed = value;
                OnObjectWillSpeedChanged(ObjectWillSpeed);
            }
        }
        #endregion Properties

        #region Methods
        public override void Active(TBoard board = default(TBoard))
        {
            if (board == null) board = Board; 
            OldSpeed = Board.BallSpeed;
            Board.BallSpeed *= MultiplySpeed;
            base.Active(board);
        }

        protected override void FinishEffect()
        {
            Board.BallSpeed /= MultiplySpeed;
            base.FinishEffect();
        }

        #endregion Methods
        protected override void DrawFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            //throw new NotImplementedException();
        }

        protected override void DrawNotFill(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            //throw new NotImplementedException();
        }

        protected override Game.Characters.Item Copy()
        {
            Prize_MoreSpeed<TBoard> prize = new Prize_MoreSpeed<TBoard>(ObjectWillSpeed);
            CopyTo(prize);
            return prize;
        }

        protected void CopyTo(Prize_MoreSpeed<TBoard> prize)
        {
            prize.MultiplySpeed = MultiplySpeed;
            prize.ObjectWillSpeed = ObjectWillSpeed;
            prize.OldSpeed = OldSpeed;
        }

        public Prize_MoreSpeed(ISpeedF objectWillSpeed)
        {
            ObjectWillSpeed = objectWillSpeed;
        }
    }
}
