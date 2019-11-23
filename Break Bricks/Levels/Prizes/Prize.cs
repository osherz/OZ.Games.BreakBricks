using Break_Bricks.BoardGame.Interface;
using Break_Bricks.Levels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Break_Bricks.Levels.Prizes
{
    public abstract class Prize<TBoard> : Game.Characters.GameObject, IPrize<TBoard>
        where TBoard : IBoard
    {
        #region Variables
        /// <summary>
        /// The board that will effect
        /// </summary>
        private TBoard board;
        /// <summary>
        /// Number of how much reference heve from prizes
        /// </summary>
        private static Dictionary<Type, int> prizesRefDictionary;
        /// <summary>
        /// Max of ref that can to be from this class,
        /// used by user, if you cross this numbet nothing will happen
        /// </summary>
        private int maxOfRefCan;
        /// <summary>
        /// Max number of prizes that can fall from each prize
        /// </summary>
        private int maxOfPrizeCanFall;
        /// <summary>
        /// Sum of prize that fell
        /// </summary>
        private int sumOfPrizeFell;

        private bool isActive;

        private bool isPause;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occues when prize activated
        /// </summary>
        public event EventHandler Activated;
        /// <summary>
        /// Occurs when prize end to active
        /// </summary>
        public event EventHandler FinishedActive;
        /// <summary>
        /// Occurs when board changed
        /// </summary>
        public event EventHandler<TBoard> BoardChanged;
        /// <summary>
        /// Occurs when reference added
        /// </summary>
        public event EventHandler<Type> RefAdded;
        /// <summary>
        /// Occurs when reference removed
        /// </summary>
        public event EventHandler<Type> RefDestroyed;
        /// <summary>
        /// Occurs when max of refernce can to create changed
        /// </summary>
        public event EventHandler<int> MaxOfRefCanChanged;
        /// <summary>
        /// Occurs when MaxOfPrizeCanFall changed
        /// </summary>
        public event EventHandler<int> MaxOfPrizeCanFallChanged;
        /// <summary>
        /// Occurs when PrizeFell
        /// </summary>
        public event EventHandler<int> SumOfPrizeFellChanged;
        public event EventHandler Paused;
        public event EventHandler Continued;
        #endregion Events

        #region Raise event methods
        /// <summary>
        /// Raise the Activated
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnActivated(EventArgs e)
        {
            IsActive = true;
            Activated?.Invoke(this, e);
        }

        /// <summary>
        /// Raise the FinishedActive event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFinishedActive(EventArgs e)
        {
            IsActive = false;
            FinishedActive?.Invoke(this, e);
        }

        /// <summary>
        /// Raise the BoardChanged event
        /// </summary>
        /// <param name="board"></param>
        protected virtual void OnBoardChanged(TBoard board)
        {

            BoardChanged?.Invoke(this, board);
        }

        /// <summary>
        /// Raise the RefAdded event
        /// </summary>
        /// <param name="prizeType"></param>
        protected virtual void OnRefAdded(Type prizeType)
        {

            RefAdded?.Invoke(this, prizeType);
        }

        /// <summary>
        /// Raise the RefDestroyed event
        /// </summary>
        /// <param name="prizeType"></param>
        protected virtual void OnRefDestroyed(Type prizeType)
        {

            RefDestroyed?.Invoke(this, prizeType);
        }

        /// <summary>
        /// Raise the MaxOfRefCanChanged event
        /// </summary>
        /// <param name="newmaxOfRefCan"></param>
        protected virtual void OnRefMaxOfRefCanChanged(int newmaxOfRefCan)
        {

            MaxOfRefCanChanged?.Invoke(this, newmaxOfRefCan);
        }

        /// <summary>
        /// Raise the MaxOfPrizeCanFallChanged ecent
        /// </summary>
        /// <param name="maxOfPrizeCanFall"></param>
        protected virtual void OnMaxOfPrizeCanFall(int maxOfPrizeCanFall)
        {

            MaxOfPrizeCanFallChanged?.Invoke(this, maxOfPrizeCanFall);
        }

        /// <summary>
        /// Raise the PrizeFellChanged event
        /// </summary>
        /// <param name="prizeFell"></param>
        protected virtual void OnSumOfPrizeFellChanged(int prizeFell)
        {

            SumOfPrizeFellChanged?.Invoke(this, prizeFell);
        }

        /// <summary>
        /// Raise the Paused event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPaused(EventArgs e)
        {
            Paused?.Invoke(this, e);
        }

        /// <summary>
        /// Raise the Continued event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnContinued(EventArgs e)
        {
            Continued?.Invoke(this, e);
        }
        #endregion Raise event methods

        #region Properties
        public override int MoveAutoInterval
        {
            get
            {
                return base.MoveAutoInterval;
            }

            set
            {
                base.MoveAutoInterval = value;
            }
        }
        public TBoard Board
        {
            get
            {
                return board;
            }

            set
            {
                board = value;
                OnBoardChanged(Board);
            }
        }

        public int MaxOfRefCan
        {
            get
            {
                return maxOfRefCan;
            }

            set
            {
                if (value >= 0)
                {
                    maxOfRefCan = value;
                    OnRefMaxOfRefCanChanged(MaxOfRefCan);
                }
            }
        }

        public int MaxOfPrizeCanFall
        {
            get
            {
                return maxOfPrizeCanFall;
            }

            set
            {
                maxOfPrizeCanFall = value;
                OnMaxOfPrizeCanFall(MaxOfPrizeCanFall);
            }
        }

        public int SumOfPrizeFell
        {
            get
            {
                return sumOfPrizeFell;
            }

            set
            {
                if(value>=0)
                {
                    sumOfPrizeFell = value;
                    OnSumOfPrizeFellChanged(SumOfPrizeFell);
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            private set
            {
                isActive = value;
            }
        }

        public bool IsPause
        {
            get
            {
                return isPause;
            }

            private set
            {
                isPause = value;
                if(isPause)
                {
                    OnPaused(new EventArgs());
                }
                else
                {
                    OnContinued(new EventArgs());
                }
            }
        }

        #endregion Properties

        #region Methods
        public virtual void Active(TBoard board)
        {

            OnActivated(new EventArgs());
        }

        protected virtual int GetNumberdOfRefernce(Type prizeType)
        {
            return prizesRefDictionary[prizeType];
        }

        /// <summary>
        /// If this ref is excited so the number of him will grow by 1
        /// </summary>
        /// <param name="prizeType"></param>
        protected virtual void AddRef(Type prizeType)
        {
            if(prizesRefDictionary.ContainsKey(prizeType))
            {
                prizesRefDictionary[prizeType]++;
            }
            else
            {
                prizesRefDictionary.Add(prizeType, 1);
            }
            OnRefAdded(prizeType);
        }

        /// <summary>
        /// Remove 1 ref from this type
        /// </summary>
        /// <param name="prizeType"></param>
        protected virtual void RemoveRef(Type prizeType)
        {
            //if(prizesRefDictionary.ContainsKey(prizeType))
            {
                prizesRefDictionary[prizeType]--;
                OnRefDestroyed(prizeType);
            }
        }

        /// <summary>
        /// Remove 1 ref from dictionery
        /// </summary>
        public void DestroyRef()
        {
            RemoveRef(GetType());
        }

        public int GetNumberdOfRefernce()
        {
            return GetNumberdOfRefernce(GetType());
        }

        /// <summary>
        /// Move the prize down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Timer_OnTick(object sender, EventArgs e)
        {
            GoDown();
            base.Timer_OnTick(sender, e);
        }

        public virtual void FinishActive()
        {
            if(IsActive) OnFinishedActive(new EventArgs());
        }
        #endregion Methods

        public override object Clone()
        {
            Prize<TBoard> prize =(Prize<TBoard>)base.Clone();
            prize.Board = Board;
            return prize;
        }

        public void Pause()
        {
            if (!IsPause)
            {
                if (!IsActive)
                {
                    StopMove();
                }
                else
                {
                    PauseEffect();
                }
                IsPause = true;
            }
        }

        public void Continue()
        {
            if (IsPause)
            {
                if (!IsActive)
                {
                    StartMove();
                }
                else
                {
                    ContinueEffect();
                }
                IsPause = false;
            }
        }

        protected virtual void PauseEffect()
        { }

        protected virtual void ContinueEffect()
        { }

        public Prize()
        {
            if(prizesRefDictionary == null) prizesRefDictionary = new Dictionary<Type, int>();
            AddRef(GetType());                    
        }
    }
}
