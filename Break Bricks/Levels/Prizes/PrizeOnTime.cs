using Break_Bricks.BoardGame.Interface;
using Break_Bricks.Levels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Break_Bricks.Levels.Prizes
{
    public abstract class PrizeOnTime<TBoard> : Prize<TBoard>, IPrize<TBoard>, IOnTime
                where TBoard : IBoard
    {
        #region Variables
        /// <summary>
        /// Timer to finish prize's active
        /// </summary>
        private Timer finishTimer;
        /// <summary>
        /// The time that will the effect will active
        /// </summary>
        private int timeOfEffect;
        /// <summary>
        /// When effect will start active this var will be the count of time
        /// </summary>
        private int timeLeftOfEffect;
        #endregion

        #region Events
        /// <summary>
        /// Occurs whenn the interval of prize active changed
        /// </summary>
        public event EventHandler<int> TimeOfEffectChanged;
        /// <summary>
        /// Occurs when the TimeLeftOfEffect changed
        /// </summary>
        public event EventHandler<int> TimeLeftOfEffectChanged;
        #endregion Events

        #region Raise events nethods
        /// <summary>
        /// Raise the TimeOfEffectChanged event
        /// </summary>
        /// <param name="newInterval"></param>
        protected virtual void OnTimeOfEffectChanged(int newInterval)
        {

            TimeOfEffectChanged?.Invoke(this, newInterval);
        }
        /// <summary>
        /// Raise the TimeLeftOfEffectChanged event
        /// </summary>
        /// <param name="timeLeftOfEffect"></param>
        protected virtual void OnTimeLeftOfEffectChanged(int timeLeftOfEffect)
        {

            TimeLeftOfEffectChanged?.Invoke(this, timeLeftOfEffect);
        }

        #endregion Raise events nethods

        #region Properties
        public int TimeOfEffect
        {
            get
            {
                return timeOfEffect;
            }

            set
            {
                if (value >= 0)
                {
                    timeOfEffect = value;
                    OnTimeOfEffectChanged(TimeOfEffect);
                }
            }
        }

        public int TimeLeftOfEffect
        {
            get
            {
                return timeLeftOfEffect;
            }

            set
            {
                if (value >= 0)
                {
                    timeLeftOfEffect = value;
                    OnTimeLeftOfEffectChanged(TimeLeftOfEffect);
                }
            }
        }
        #endregion Properties

        #region Methods
        public override void Active(TBoard board)
        {

            finishTimer.Start();
            base.Active(board);
        }

        /// <summary>
        /// Finisg the effect of prize
        /// </summary>
        public void FinishPrizeEffect(object sender, EventArgs e)
        {
            if (IsActive)
            {
                if (TimeLeftOfEffect < TimeOfEffect) TimeLeftOfEffect++;
                else
                {
                    FinishEffect();
                }
            }
        }

        protected virtual void FinishEffect()
        {
            finishTimer.Stop();
            TimeLeftOfEffect = 0;
            OnFinishedActive(new EventArgs());
        }

        public override void FinishActive()
        {
            if (IsActive)
            {
                FinishEffect();
            }
        }

        protected override void PauseEffect()
        {
            finishTimer.Stop();
            base.PauseEffect();
        }

        protected override void ContinueEffect()
        {
            finishTimer.Start();
            base.ContinueEffect();
        }

        #endregion Methods

        public override object Clone()
        {
            PrizeOnTime<TBoard> prize = (PrizeOnTime<TBoard>)base.Clone();
            prize.TimeOfEffect = TimeOfEffect;
            return prize;
        }

        public PrizeOnTime()
        {
            finishTimer = new Timer();
            finishTimer.Tick += FinishPrizeEffect;
            finishTimer.Interval = 1;
        }
    }
}
