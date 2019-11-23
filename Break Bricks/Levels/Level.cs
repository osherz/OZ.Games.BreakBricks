using Break_Bricks.BoardGame.Interface;
using Game.Other;
using Break_Bricks.Levels;
using Break_Bricks.Levels.Interface;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Break_Bricks.Levels
{
    public class Level<TBoard> : Break_Bricks.BoardGame.Items, ILevel<TBoard>
        where TBoard : IBoard
    {
        #region Variables
        /// <summary>
        /// List of bricks
        /// </summary>
        private List<List<IBrick>> brickList;
        /// <summary>
        /// If true, the size of level will be by the size of all bricks
        /// </summary>
        private SizeF sizeOfBrick;
        /// <summary>
        /// Margin of brick from other brick
        /// </summary>
        private Padding marginOfAllBrick;
        /// <summary>
        /// List of prize
        /// </summary>
        private List<IPrize<TBoard>> prizeList;
        /// <summary>
        /// List of prize that fall
        /// </summary>
        private List<IPrize<TBoard>> prizeFallList;
        /// <summary>
        /// Lit of prize that active
        /// </summary>
        private List<IPrize<TBoard>> prizeActiveList;
        /// <summary>
        /// הסיכוי שייצא פרס בפגיעה בלבנה
        /// </summary>
        private double prizePrecent;
        /// <summary>
        /// The board of level
        /// </summary>
        private TBoard parentBoard;
        /// <summary>
        /// Sum of briks that need to break
        /// </summary>
        private int bricksLeft;
        /// <summary>
        /// Is level started
        /// </summary>
        private bool isLevelStart;
        /// <summary>
        /// Is level paused
        /// </summary>
        private bool isLevelPaused;
        /// <summary>
        /// Is level finished
        /// </summary>
        private bool isLevelFinished;
        /// <summary>
        /// The size of bricks will be by level
        /// </summary>
        private bool isSizeOfBricksByLevel;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when row added
        /// </summary>
        public event EventHandler<int> RowAdded;
        /// <summary>
        /// Occurs when brick added
        /// </summary>
        public event EventHandler<IBrick[]> BrickAdded;
        /// <summary>
        /// Occurs when brick removed
        /// </summary>
        public event EventHandler<IBrick[]> BrickRemoved;
        /// <summary>
        /// Occurs when prize added
        /// </summary>
        public event EventHandler<IPrize<TBoard>[]> PrizeAdded;
        /// <summary>
        /// Occurs when prize removed
        /// </summary>
        public event EventHandler<IPrize<TBoard>[]> PrizeRemoved;
        /// <summary>
        /// Occurs when prize fall
        /// </summary>
        public event EventHandler<IPrize<TBoard>> PrizeFall;
        /// <summary>
        /// Occurs when margin of all bricks one from other changed
        /// </summary>
        public event EventHandler<Padding> MarginOfAllBrickChanged;
        /// <summary>
        /// Occurs when SizeOfBrick changed
        /// </summary>
        public event EventHandler<SizeF> SizeOfBrickChanged;
        /// <summary>
        /// Occurs when prize precent changed
        /// </summary>
        public event EventHandler<double> PrizePrecentChanged;
        /// <summary>
        /// Occurs before parent change
        /// </summary>
        public event EventHandler<TBoard> ParentBeforeChange;
        /// <summary>
        /// Occurs when parent changed
        /// </summary>
        public event EventHandler<TBoard> ParentChanged;
        /// <summary>
        /// Occurs when level started
        /// </summary>
        public event EventHandler LevelStart;
        /// <summary>
        /// Occurs when level paused
        /// </summary>
        public event EventHandler LevelPaused;
        /// <summary>
        /// Occurs when level finished
        /// </summary>
        public event EventHandler LevelFinished;
        /// <summary>
        /// Occurs when isSizeOfBricksByLevel changed
        /// </summary>
        public event EventHandler<bool> IsSizeOfBricksByLevelChanged;
        /// <summary>
        /// Occurs when brick broke
        /// </summary>
        public event EventHandler<IBrick> BrickBroke;
        /// <summary>
        /// Occurs when prize generated
        /// </summary>
        public event EventHandler<IPrize<TBoard>> PrizeGenerated;
        #endregion Events

        #region Raises events methods
        /// <summary>
        /// Raise the RowAdded event
        /// </summary>
        /// <param name="newRow"></param>
        protected virtual void OnRowAdded(int newRow)
        {

            RowAdded?.Invoke(this, newRow);
        }

        /// <summary>
        /// Raise the BrickAdded event
        /// </summary>
        /// <param name="brick"></param>
        protected virtual void OnBrickAdded(params IBrick[] brick)
        {
            if(IsSizeOfBricksByLevel)
            {
                UpdateBricksByLevel();
            }
            else
            {
                UpdateLevelByBricks();
            }
            BrickAdded?.Invoke(this, brick);
        }

        /// <summary>
        /// Raise the BrickRemoved event
        /// </summary>
        /// <param name="brick"></param>
        protected virtual void OnBrickRemoved(params IBrick[] brick)
        {

            BrickRemoved?.Invoke(this, brick);
        }

        /// <summary>
        /// Raise the PrizeAdded event
        /// </summary>
        /// <param name="prize"></param>
        protected virtual void OnPrizeAdded(params IPrize<TBoard>[] prize)
        {

            PrizeAdded?.Invoke(this, prize);
        }

        /// <summary>
        /// Raise the PrizeRemoved event
        /// </summary>
        /// <param name="prize"></param>
        protected virtual void OnPrizeRemoved(params IPrize<TBoard>[] prize)
        {

            PrizeRemoved?.Invoke(this, prize);
        }

        /// <summary>
        /// Raise the PrizeFall event
        /// </summary>
        /// <param name="prize"></param>
        protected virtual void OnPrizeFall(IPrize<TBoard> prize)
        {

            PrizeFall?.Invoke(this, prize);
        }

        /// <summary>
        /// Raise the MarginOfAllBrickChanged event
        /// </summary>
        /// <param name="newMarginOfAllBrick"></param>
        protected virtual void OnMarginOfAllBrickChanged(Padding newMarginOfAllBrick)
        {

            MarginOfAllBrickChanged?.Invoke(this, newMarginOfAllBrick);
        }

        /// <summary>
        /// Raise the SizeOfBrickChanged event
        /// </summary>
        /// <param name="newSize"></param>
        protected virtual void OnSizeOfBrickChanged(SizeF newSize)
        {

            SizeOfBrickChanged?.Invoke(this, newSize);
        }

        /// <summary>
        /// Raise the PrizePrecentChanged event
        /// </summary>
        /// <param name="newPrecent"></param>
        protected virtual void OnPrizePrecentChanged(double newPrecent)
        {

            PrizePrecentChanged?.Invoke(this, newPrecent);
        }

        /// <summary>
        /// Raise the ParentBeforeChange event
        /// </summary>
        /// <param name="oldParentBoard"></param>
        protected virtual void OnParentBeforeChange(TBoard oldParentBoard)
        {

            ParentBeforeChange?.Invoke(this, oldParentBoard);
        }

        /// <summary>
        /// Raise the ParentChanged event
        /// </summary>
        /// <param name="newParentBoard"></param>
        protected virtual void OnParentChanged(TBoard newParentBoard)
        {
            Plank = ParentBoard.Plank;
            ParentChanged?.Invoke(this, newParentBoard);
        }

        /// <summary>
        /// Raise the LevelStart event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLevelStarted(EventArgs e)
        {

            LevelStart?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the LevelPaused event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLevelPaused(EventArgs e)
        {

            LevelPaused?.Invoke(this, e);
        }

        /// <summary>
        /// Raise the LevelFinished event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLevelFinished(EventArgs e)
        {

            LevelFinished?.Invoke(this, e);
        }

        /// <summary>
        /// Raise rhe IsSizeOfBricksByLevelChanged event
        /// </summary>
        /// <param name="isSizeOfBricksByLevel"></param>
        protected virtual void OnIsSizeOfBricksByLevelChanged(bool isSizeOfBricksByLevel)
        {

            IsSizeOfBricksByLevelChanged?.Invoke(this, isSizeOfBricksByLevel);
        }

        /// <summary>
        /// Raise the BrickBroke Event
        /// </summary>
        /// <param name="brick"></param>
        protected virtual void OnBrickBroke(IBrick brick)
        {

            BrickBroke?.Invoke(this, brick);
        }

        /// <summary>
        /// Raise the PrizeGenerated event
        /// </summary>
        /// <param name="prize"></param>
        protected virtual void OnPrizeGenerated(IPrize<TBoard> prize)
        {

            PrizeGenerated?.Invoke(this, prize);
        }
        #endregion Raises events methods

        #region Reset/Restart methods
        private void ResetPrizeEvents(IPrize<TBoard> prize)
        {
            if (prize != null)
            {
                prize.LocationChanged -= Prize_OnLocationChanged;
                prize.FinishedActive -= Prize_FinishedActive;
            }
        }

        private void RestartPrizeEvents(IPrize<TBoard> prize)
        {
            if(prize != null)
            {
                prize.LocationChanged += Prize_OnLocationChanged;
                prize.FinishedActive += Prize_FinishedActive;
            }
        }

        private void ResetParentEvent(TBoard parentBoard)
        {
            if(parentBoard!=null)
            {
                parentBoard.BallFailed -= ParentBoard_BallFailed;
            }
        }

        private void RestartParentEvent(TBoard parentBoard)
        {
            if (parentBoard != null)
            {
                parentBoard.BallFailed += ParentBoard_BallFailed;
            }
        }

        private void ResetBrickEvent(params IBrick[] bricks)
        {
            foreach (IBrick brick in bricks)
            {
                if (brick != null)
                {
                    brick.BrokedCompletly -= Brick_OnBrokedCompletly;
                }
            }
        }

        private void RestartBrickEvent(params IBrick[] bricks)
        {
            foreach (IBrick brick in bricks)
            {
                if (brick != null)
                {
                    brick.BrokedCompletly += Brick_OnBrokedCompletly;
                }
            }
        }

        #endregion Reset/Restart methods

        #region events methods
        protected virtual void ParentBoard_BallFailed(object sender, EventArgs e)
        {
            if(CheckIfLevelFinished())
            {
                FinishLevel();
            }
        }

        protected override void OnAutoSizeChanged(bool autoSize)
        {
            if(autoSize)
            {
                IsSizeOfBricksByLevel = false;
                UpdateLevelByBricks();
            }
            base.OnAutoSizeChanged(autoSize);
        }

        protected virtual void Brick_OnBrokedCompletly(object sender, EventArgs e)
        {
            if (sender is IBrick)
            {
                bricksLeft--;
                IBrick brick = (IBrick)sender;
                OnBrickBroke(brick);
                GeneratePrize(new RectangleF(brick.Left, brick.Top, brick.Width, brick.Height));
                if (CheckIfLevelFinished())
                {
                    FinishLevel();
                }
            }
        }

        protected void Prize_OnLocationChanged(object sender, PointF e)
        {
            if(IsLevelStarted && !IsLevelPaused && sender is IPrize<TBoard>)
            {
                IPrize<TBoard> prize = (IPrize<TBoard>)sender;
                if (prizeFallList.IndexOf(prize) >= 0)
                {
                    RectangleF prizeRect = new RectangleF(prize.Left + Left, prize.Top + Top, prize.Width, prize.Height);
                    if (prizeRect.IntersectsWith(Plank.GetRectangleF()))
                    {
                        prizeActiveList.Add(prize);
                        prize.Active(ParentBoard);
                        prizeFallList.Remove(prize);
                        if (bricksLeft <= 0 && prizeFallList.Count == 0)
                        {
                            FinishLevel();
                        }                 
                    }
                    else if (prize.Top > Plank.Bottom)
                    {
                        prizeFallList.Remove(prize);
                        ResetPrizeEvents(prize);
                        ChangeSumOfPrizeFellEachPrize(prize.Tag, -1);
                        if (bricksLeft <= 0 && prizeFallList.Count == 0) FinishLevel();
                    }
                } 
            }
        }

        protected void Prize_FinishedActive(object sender, EventArgs e)
        {
            if (IsLevelStarted && !IsLevelPaused && sender is IPrize<TBoard>)
            {
                IPrize<TBoard> prize = (IPrize<TBoard>)sender;
                ResetPrizeEvents(prize);
                prizeActiveList.Remove(prize);
                ChangeSumOfPrizeFellEachPrize(prize.Tag, -1);
            }
        }
        #endregion events methods
        
        #region Properties
            /// <summary>
            /// Sum of row
            /// </summary>
        public int SumOfRow
        {
            get
            {
                return brickList.Count;
            }
        }

        /// <summary>
        /// Margin of all bricks one from other
        /// </summary>
        public Padding MarginOfAllBrick
        {
            get
            {
                return marginOfAllBrick;
            }

            set
            {
                marginOfAllBrick = value;
                OnMarginOfAllBrickChanged(MarginOfAllBrick);
            }
        }

        /// <summary>
        /// If order auto is true you can't change it
        /// </summary>
        public SizeF SizeOfBrick
        {
            get
            {
                return sizeOfBrick;
            }

            set
            {
                sizeOfBrick = value;
                UpdateSizeOfBricks();
                UpdateLocationOfBricks();
                OnSizeOfBrickChanged(SizeOfBrick);
            }
        }

        /// <summary>
        /// The board of level
        /// </summary>
        public TBoard ParentBoard
        {
            get
            {
                return parentBoard;
            }

            set
            {
                ResetParentEvent(ParentBoard);
                OnParentBeforeChange(ParentBoard);
                parentBoard = value;
                RestartParentEvent(ParentBoard);
                OnParentChanged(ParentBoard);
            }
        }

        public bool IsLevelStarted
        {
            get
            {
                return isLevelStart;
            }

            private set
            {
                isLevelStart = value;
                if (IsLevelStarted) OnLevelStarted(new EventArgs());
            }
        }

        public bool IsLevelPaused
        {
            get
            {
                return isLevelPaused;
            }

            private set
            {
                isLevelPaused = value;
                if (IsLevelPaused) OnLevelPaused(new EventArgs());
            }
        }

        public bool IsLevelFinished
        {
            get
            {
                return isLevelFinished;
            }

            private set
            {
                isLevelFinished = value;
                if (IsLevelFinished) OnLevelFinished(new EventArgs());
            }
        }

        public int BricksLeft
        {
            get
            {
                return bricksLeft;
            }

            private set
            {
                bricksLeft = value;
            }
        }

        /// <summary>
        /// Get/Set. minimum value 0, maximum 1.
        /// </summary>
        public double PrizePrecent
        {
            get
            {
                return prizePrecent;
            }

            set
            {
                prizePrecent = value;
                OnPrizePrecentChanged(PrizePrecent);
            }
        }

        public bool IsSizeOfBricksByLevel
        {
            get
            {
                return isSizeOfBricksByLevel;
            }

            set
            {
                isSizeOfBricksByLevel = value;
                if(IsSizeOfBricksByLevel)
                {
                    AutoSize = false;
                    UpdateBricksByLevel();
                }
                OnIsSizeOfBricksByLevelChanged(IsSizeOfBricksByLevel);
            }
        }
        #endregion Properties

        #region Constructors
        public Level()
        {
            brickList = new List<List<BoardGame.Interface.IBrick>>();
            prizeList = new List<BoardGame.Interface.IPrize<TBoard>>();
            prizeFallList = new List<IPrize<TBoard>>();
            prizeActiveList = new List<BoardGame.Interface.IPrize<TBoard>>();
        }

        #endregion Constructors

        #region Help methods
        private bool CheckIfLevelFinished()
        {
            return BricksLeft <= 0 && prizeFallList.Count == 0;
        }


        private int IndexOfListWithMaxOfBricks()
        {
            int maxSumOfBrick = 0;
            int index = -1;
            for(int i = 0; i<SumOfRow; i++)
            {
                if (brickList[i].Count > maxSumOfBrick)
                {
                    maxSumOfBrick = brickList[i].Count;
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// מעדכן את גודלם ומיקומם של הלבנים בהתאם לגודל של השלב
        /// </summary>
        private void UpdateBricksByLevel()
        {
            int sumOfMaxBricksInRow = brickList[IndexOfListWithMaxOfBricks()].Count;
            SizeF size = new SizeF();
            size.Width = Width / sumOfMaxBricksInRow - MarginOfAllBrick.Right - MarginOfAllBrick.Left;
            size.Height = Height / SumOfRow - MarginOfAllBrick.Top - MarginOfAllBrick.Bottom;
            SizeOfBrick = size;
        }

        private void UpdateLevelByBricks()
        {
            int i = 0;
            while (brickList.Count > i && brickList[i].Count == 0) i++;
            if (i < brickList.Count)
            {
                Height = (MaxBottom(SumOfRow - 1) + MarginOfAllBrick.Bottom) - (brickList[i][0].Top + marginOfAllBrick.Top);
                Width = (MaxRight(SumOfRow - 1) + MarginOfAllBrick.Right) - (brickList[i][0].Left + marginOfAllBrick.Left);
            }
            UpdateLocationOfBricks();
        }

        private float MaxRight(int index)
        {
            float maxRight = 0;
            foreach (IBrick brick in brickList[index])
            {
                if (maxRight < brick.Right) maxRight = brick.Right;
            }
            return maxRight;
        }

        private float MaxBottom(int index)
        {
            float maxBottom = 0;
            foreach(IBrick brick in brickList[index])
            {
                if (maxBottom < brick.Bottom) maxBottom = brick.Bottom;
            }
            return maxBottom;
        }

        private void UpdateSizeOfBricks()
        {
            int sumBrickInRow = 0;
            for (int i = 0; i < brickList.Count; i++)
            {
                sumBrickInRow = brickList[i].Count;
                for (int j = 0; j < sumBrickInRow; j++)
                {
                    brickList[i][j].Size = SizeOfBrick;
                }
            }
        }

        private void UpdateLocationOfBricks()
        {
            int sumBrickInRow = 0;
            float top = 0;
            float left = 0;
            float widthOfRow = 0;
            float maxBottom;
            for (int i = 0; i < brickList.Count; i++)
            {
                maxBottom = 0;
                sumBrickInRow = brickList[i].Count;
                top += MarginOfAllBrick.Top;
                widthOfRow = WidthOfRow(i);
                left = (Width - widthOfRow) / 2 + MarginOfAllBrick.Left;
                for (int j = 0; j < sumBrickInRow; j++)
                {
                    brickList[i][j].Location = new PointF(left, top);
                    left += brickList[i][j].Width + MarginOfAllBrick.Left + MarginOfAllBrick.Right;
                    if (brickList[i][j].Bottom > maxBottom) maxBottom = brickList[i][j].Bottom;
                }
                top = maxBottom + marginOfAllBrick.Bottom;
            }
        }

        private float WidthOfRow(int index)
        {
            float width = 0;
            foreach(IBrick brick in brickList[index])
            {
                width += marginOfAllBrick.Left + brick.Width + marginOfAllBrick.Right;
            }
            return width;
        }

        public RectangleF GetRectangleF()
        {
            return new RectangleF(Location, Size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prizeTag"></param>
        /// <param name="num">By this num the value will be change</param>
        private void ChangeSumOfPrizeFellEachPrize(object prizeTag, int num)
        {
            List<IPrize<TBoard>> prizeToChangeList = prizeList.FindAll(
                new Predicate<BoardGame.Interface.IPrize<TBoard>>
                ((IPrize<TBoard> item) => item.Tag.Equals(prizeTag)));
            while(prizeToChangeList.Count > 0)
            {
                prizeToChangeList[0].SumOfPrizeFell += num;
                prizeToChangeList.RemoveAll(prizeToChangeList[0].Equals);

            }
        }
        #endregion Help methods

        #region Add bricks methods
        /// <summary>
        /// Add new row to end
        /// </summary>
        /// <param name="sumOfRow"></param>
        public void AddRow(int sumOfRow)
        {
            while (sumOfRow > 0)
            {
                brickList.Add(new List<IBrick>());
                sumOfRow--;
            }
            OnRowAdded(sumOfRow);
        }

        /// <summary>
        /// Add new row to end with this bricks
        /// </summary>
        /// <param name="bricks"></param>
        public void AddRow(params IBrick[] bricks)
        {
            brickList.Add(new List<IBrick>(bricks));
            RestartBrickEvent(bricks);
            OnRowAdded(1);
            OnBrickAdded(bricks);
        }

        /// <summary>
        /// Add new rows to end with this bricks
        /// </summary>
        /// <param name="bricks"></param>
        public void AddRow(int sumOfRow, params IBrick[] bricks)
        {
            while (sumOfRow > 0)
            {
                for(int i =0; i<bricks.Length; i++)
                {
                    bricks[i] = (IBrick)bricks.Clone();
                }
                AddRow(bricks);
                sumOfRow--;
            }

        }

        /// <summary>
        /// Add nnew row before index and enter the bricks to him
        /// </summary>
        /// <param name="indexRow">Brfore this index the bricks will enter</param>
        /// <param name="bricks"></param>
        public void InsertToNewRow(int indexRow, params IBrick[] bricks)
        {
            brickList.Insert(indexRow, new List<BoardGame.Interface.IBrick>(bricks));
            RestartBrickEvent(bricks);
            OnRowAdded(1);
            OnBrickAdded(bricks);
        }

        /// <summary>
        /// Insert bricks to exciting row
        /// </summary>
        /// <param name="indexRow"></param>
        /// <param name=""></param>
        /// <param name="bricks"></param>
        public void InsertToRow(int indexRow, params IBrick[] bricks)
        {
            brickList[indexRow].AddRange(bricks);
            RestartBrickEvent(bricks);
            OnBrickAdded(bricks);
        }

        /// <summary>
        /// Remove row
        /// </summary>
        /// <param name="indexRow"></param>
        public void RemoveRow(int indexRow)
        {
            List<IBrick> bricks = brickList[indexRow];
            brickList.RemoveAt(indexRow);
            ResetBrickEvent(bricks.ToArray());
            OnBrickRemoved(bricks.ToArray());
        }

        /// <summary>
        /// Remove brick by index
        /// </summary>
        /// <param name="indexRow"></param>
        /// <param name="indexOfBrick"></param>
        public void RemoveBrickAt(int indexRow, int indexOfBrick)
        {
            IBrick brick = brickList[indexRow][indexOfBrick];
            brickList[indexRow].RemoveAt(indexOfBrick);
            ResetBrickEvent(brick);
            OnBrickRemoved(brick);
        }

        /// <summary>
        /// Remove brick by item
        /// </summary>
        /// <param name="bricks"></param>
        public void RemoveBrick(params IBrick[] bricks)
        {
            foreach(IBrick brick in bricks)
            {
                foreach (List<IBrick> brickList in this.brickList)
                {
                    if (brickList.Remove(brick))
                    {
                        ResetBrickEvent(brick);
                        OnBrickRemoved(brick);
                    }
                }
            }
        }
        #endregion Add bricks methods

        #region Add prizes methods
        /// <summary>
        /// Add prize to list
        /// </summary>
        /// <param name="prizes"></param>
        public void AddPrize(params IPrize<TBoard>[] prizes)
        {
            prizeList.AddRange(prizes);
            OnPrizeAdded(prizes);
        }

        /// <summary>
        /// Remove prize by index
        /// </summary>
        /// <param name="index"></param>
        public void RemovePrize(int index)
        {
            IPrize<TBoard> prize = prizeList[index];
            prizeList.RemoveAt(index);
            OnPrizeRemoved(prize);
        }

        /// <summary>
        /// Remove prize by item
        /// </summary>
        /// <param name="prizes"></param>
        public void RemovePrize(params IPrize<TBoard>[] prizes)
        {
            foreach(IPrize<TBoard> prize in prizes)
            {
                if(prizeList.Remove(prize))
                {
                    OnPrizeRemoved(prize);
                }
            }
        }
        #endregion Add prizes methods

        #region Methods
        public override void Draw(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            for(int i =0;i<brickList.Count; i++)
            {
                for(int j=0; j<brickList[i].Count; j++)
                {
                    if (!brickList[i][j].IsBrokeCompletly)
                    {
                        if(!brickList[i][j].IsBrokeCompletly) brickList[i][j].Draw(g, Left + extraX, Top + extraY, extraWidth, extraHeight);
                    }
                }
            }

            for (int i = 0; i < prizeFallList.Count; i++)
            {
                prizeFallList[i].Draw(g, Left +extraX, Top + extraY, extraWidth, extraHeight);
            }
            base.Draw(g, extraX, extraY, extraWidth, extraHeight);
        }

        /// <summary>
        /// מעדכן את מיקום הלבנים וגודלם
        /// </summary>
        public void ToMiddleFromLeft()
        {
            Left = (ParentBoard.Width - Width) / 2;
        }

        public void ToMiddleFromTop()
        {
            Top = (ParentBoard.Height - Height) / 2;
        }

        public void ToMiddle()
        {
            ToMiddleFromLeft();
            ToMiddleFromTop();
        }

        /// <summary>
        /// Check if something hit the brick and return where hit on brick
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private Direction Old_CheckIfBallHitBricks_Old(IBall ball)
        {
            if (IsLevelStarted && !IsLevelPaused)
            {
                IBrick brick;
                Region regHit = ball.GetRegion();
                RectangleF brickRect;
                for (int i = 0; i < brickList.Count; i++)
                {
                    for (int j = 0; j < brickList[i].Count; j++)
                    {
                        brick = brickList[i][j];
                        if (!brick.IsBrokeCompletly)
                        {
                            brickRect = brick.GetRectangleF();
                            brickRect.X += Left;
                            brickRect.Y += Top;
                            if (regHit.IsVisible(brickRect))
                            {
                                brick.BreakLvl();
                                if (ball.DirectionY > 0)
                                {
                                    float distanceBottomFromTop = ball.Bottom - brickRect.Top; //מרחק תחתית הכדור מראש הלבנה
                                    float distanceRightFromLeft = ball.Right - brickRect.Left; //מרחק ימין הכדור משמאל הלבנה
                                    float distanceLeftFromRight = ball.Left - brickRect.Right; //מרחק שמאל הכדור מימין הלבנה
                                    if (distanceBottomFromTop < distanceRightFromLeft)
                                    {
                                        if (distanceBottomFromTop < Math.Abs(distanceLeftFromRight))
                                            return Direction.Top;
                                        else
                                            return Direction.Right;
                                    }
                                    else
                                    {
                                        return Direction.Left;
                                    }

                                }
                                else
                                {
                                    float distanceBottomFromTop = brickRect.Bottom - ball.Top; //מרחק תחתית הלבנה מראש הכדור
                                    float distanceRightFromLeft = brickRect.Right - ball.Left; // מרחק ימין הלבנה משמאל הכדור
                                    float distanceLeftFromRight = brickRect.Left - ball.Right;  // מרחק שמאל הלבנה מימין הכדור
                                    if (distanceBottomFromTop < distanceRightFromLeft)
                                    {
                                        if (distanceBottomFromTop < Math.Abs(distanceLeftFromRight))
                                            return Direction.Bottom;
                                        else
                                            return Direction.Left;
                                    }
                                    else
                                    {
                                        return Direction.Right;
                                    }

                                }
                            }
                        }
                    }

                }
            }
            return Direction.None;
        }

        public Direction CheckIfBallHitBricks(IBall ball)
        {
            if (IsLevelStarted && !IsLevelPaused)
            {
                IBrick brick;
                IBrick brickNear;
                RectangleF brickRect;
                RectangleF brickNearRect;
                for (int i = 0; i < brickList.Count; i++)
                {
                    for (int j = 0; j < brickList[i].Count; j++)
                    {
                        brick = brickList[i][j];
                        if (!brick.IsBrokeCompletly)
                        {
                            brickRect = brick.GetRectangleF();
                            brickRect.X += Left;
                            brickRect.Y += Top;
                            float areaToCheckBy = 2;
                            if(ball.Top + ball.Height / areaToCheckBy >= brickRect.Top && ball.Bottom - ball.Height / areaToCheckBy <= brickRect.Bottom)
                            {
                                if(ball.Right >= brickRect.Left && ball.Left < brickRect.Left)
                                {
                                    brick.BreakLvl();
                                    return Direction.Left;
                                }
                                else if(ball.Left <= brickRect.Right && ball.Right > brickRect.Right)
                                {
                                    brick.BreakLvl();
                                    return Direction.Right;
                                }
                            }
                            if(ball.Left + ball.Width/ areaToCheckBy >= brickRect.Left && ball.Right - ball.Width/areaToCheckBy <=brickRect.Right)
                            {
                                if(ball.Bottom >= brickRect.Top && ball.Top<brickRect.Top)
                                {
                                    brick.BreakLvl();
                                    return Direction.Top;
                                }
                                else if(ball.Top<=brickRect.Bottom && ball.Bottom>brickRect.Bottom)
                                {
                                    brick.BreakLvl();
                                    return Direction.Bottom;
                                }
                            }
                            if(ball.IsHit(brickRect.Left, brickRect.Top))
                            {
                                brick.BreakLvl();
                                if (j > 0)
                                {
                                    brickNear = brickList[i][j - 1];
                                    if (!brickNear.IsBrokeCompletly)
                                    {
                                        brickNearRect = brickNear.GetRectangleF();
                                        brickNearRect.X += Left;
                                        brickNearRect.Y += Top;
                                        if (ball.IsHit(brickNearRect.Right, brickNearRect.Top))
                                        {
                                            brickNear.BreakLvl();
                                            return Direction.Top;
                                        }
                                    }
                                }
                                if(i - 1 >= 0 && brickList[i - 1].Count > j)
                                {
                                    brickNear = brickList[i - 1][j];
                                    if (!brickNear.IsBrokeCompletly)
                                    {
                                        brickNearRect = brickNear.GetRectangleF();
                                        brickNearRect.X += Left;
                                        brickNearRect.Y += Top;
                                        if (ball.IsHit(brickNearRect.Left, brickNearRect.Bottom))
                                        {
                                            brickNear.BreakLvl();
                                            return Direction.Left;
                                        }
                                    }
                                }
                                return Direction.Left | Direction.Top;
                            }
                            if (ball.IsHit(brickRect.Right, brickRect.Top))
                            {
                                brick.BreakLvl();
                                if (j + 1 < brickList[i].Count)
                                {
                                    brickNear = brickList[i][j + 1];
                                    if (!brickNear.IsBrokeCompletly)
                                    {
                                        brickNearRect = brickNear.GetRectangleF();
                                        brickNearRect.X += Left;
                                        brickNearRect.Y += Top;
                                        if (ball.IsHit(brickNearRect.Left, brickNearRect.Top))
                                        {
                                            brickNear.BreakLvl();
                                            return Direction.Top;
                                        }
                                    }
                                }
                                if (i - 1 >=0 && brickList[i-1].Count>j)
                                {
                                    brickNear = brickList[i - 1][j];
                                    if (!brickNear.IsBrokeCompletly)
                                    {
                                        brickNearRect = brickNear.GetRectangleF();
                                        brickNearRect.X += Left;
                                        brickNearRect.Y += Top;
                                        if (ball.IsHit(brickNearRect.Right, brickNearRect.Bottom))
                                        {
                                            brickNear.BreakLvl();
                                            return Direction.Right;
                                        }
                                    }
                                }
                                return Direction.Right | Direction.Top;
                            }
                            if (ball.IsHit(brickRect.Left, brickRect.Bottom))
                            {
                                brick.BreakLvl();
                                if (j > 0)
                                {
                                    brickNear = brickList[i][j - 1];
                                    if (!brickNear.IsBrokeCompletly)
                                    {
                                        brickNearRect = brickNear.GetRectangleF();
                                        brickNearRect.X += Left;
                                        brickNearRect.Y += Top;
                                        if (ball.IsHit(brickNearRect.Right, brickNearRect.Bottom))
                                        {
                                            brickNear.BreakLvl();
                                            return Direction.Bottom;
                                        }
                                    }
                                }
                                if (i + 1 < brickList.Count && brickList[i+1].Count > j)
                                {
                                    brickNear = brickList[i + 1][j];
                                    if (!brickNear.IsBrokeCompletly)
                                    {
                                        brickNearRect = brickNear.GetRectangleF();
                                        brickNearRect.X += Left;
                                        brickNearRect.Y += Top;
                                        if (ball.IsHit(brickNearRect.Left, brickNearRect.Top))
                                        {
                                            brickNear.BreakLvl();
                                            return Direction.Left;
                                        }
                                    }
                                }

                                return Direction.Left | Direction.Bottom;
                            }
                            if (ball.IsHit(brickRect.Right, brickRect.Bottom))
                            {
                                brick.BreakLvl();
                                if (j + 1 < brickList[i].Count)
                                {
                                    brickNear = brickList[i][j + 1];
                                    if (!brickNear.IsBrokeCompletly)
                                    {
                                        brickNearRect = brickNear.GetRectangleF();
                                        brickNearRect.X += Left;
                                        brickNearRect.Y += Top;
                                        if (ball.IsHit(brickNearRect.Left, brickNearRect.Bottom))
                                        {
                                            brickNear.BreakLvl();
                                            return Direction.Bottom;
                                        }
                                    }
                                }

                                if (i + 1 < brickList.Count && brickList[i + 1].Count > j)
                                {
                                    brickNear = brickList[i + 1][j];
                                    if (!brickNear.IsBrokeCompletly)
                                    {
                                        brickNearRect = brickNear.GetRectangleF();
                                        brickNearRect.X += Left;
                                        brickNearRect.Y += Top;
                                        if (ball.IsHit(brickNearRect.Right, brickNearRect.Top))
                                        {
                                            brickNear.BreakLvl();
                                            return Direction.Right;
                                        }
                                    }
                                }
                                return Direction.Right | Direction.Bottom;
                            }
                        }
                    }
                }
            }
            return Direction.None;
        }

        public void GeneratePrize(RectangleF rect)
        {
            Random rnd = new Random();

            if (PrizePrecent > 0)
            {
                double number = rnd.NextDouble();
                if (number <= PrizePrecent)
                {
                    List<IPrize<TBoard>> prizeCanFallList = CheckIfHavePrizeThatCanFell();
                    if (prizeCanFallList.Count > 0)
                    {
                        int indexOfPrize = rnd.Next(0, prizeCanFallList.Count);
                        IPrize<TBoard> prize = (IPrize<TBoard>)prizeCanFallList[indexOfPrize].Clone();
                        prizeFallList.Add(prize);
                        prize.Location = new PointF(rect.X + (rect.Width - prize.Width) / 2, rect.Bottom);
                        prize.StartMove(Direction.Bottom);
                        ChangeSumOfPrizeFellEachPrize(prize.Tag, 1);
                        RestartPrizeEvents(prize);
                        OnPrizeGenerated(prize);
                    }
                }
            }
        }

        private List<IPrize<TBoard>> CheckIfHavePrizeThatCanFell()
        {
            List<IPrize<TBoard>> prizeCanFallList = new List<BoardGame.Interface.IPrize<TBoard>>();
            foreach (IPrize<TBoard> prize in prizeList)
            {
                if (prize.MaxOfPrizeCanFall < 0 || prize.MaxOfPrizeCanFall > prize.SumOfPrizeFell)
                    prizeCanFallList.Add(prize);
            }
            return prizeCanFallList;
        }

        #endregion Methods

        #region Level game methods
        public void StartLevel()
        {
            ClearFallPrize();
            BricksLeft = 0;
            foreach (List<IBrick> bricks in brickList)
            {
                foreach (IBrick brick in bricks)
                {
                    brick.Reset();
                    if(brick.IsCanToBreak)
                    {
                        brick.Reset();
                        BricksLeft++;
                    }
                }
            }
            IsLevelPaused = false;
            IsLevelFinished = false;
            IsLevelStarted = true;
        }

        public void PausePrizes()
        {
            foreach(IPrize<TBoard> prize in prizeFallList)
            {
                prize.Pause();
            }

            foreach (IPrize<TBoard> prize in prizeActiveList)
            {
                prize.Pause();
            }
        }

        public void ContinuePrizes()
        {
            foreach (IPrize<TBoard> prize in prizeFallList)
            {
                prize.Continue();
            }

            foreach (IPrize<TBoard> prize in prizeActiveList)
            {
                prize.Continue();
            }
        }

        public void Pause()
        {
            if (!IsLevelPaused)
            {
                PausePrizes();

                IsLevelPaused = true;
            }
        }

        public void Continue()
        {
            if (IsLevelPaused)
            {
                ContinuePrizes();

                IsLevelPaused = false;
            }
        }

        public void FinishLevel()
        {
            ClearFallPrize();
            OnLevelFinished(new EventArgs());
        }

        /// <summary>
        /// Finish active the active prizes
        /// </summary>
        public void FinishActivePrize()
        {
            IPrize<TBoard>[] prizeActiveArry = prizeActiveList.ToArray();
            foreach (IPrize<TBoard> prize in prizeActiveArry)
            {
                prize.FinishActive();
            }
        }

        public void ClearFallPrize()
        {
            //for(int i = prizeFallList.Count-1; i>=0 ; i--)
            //{
            //    prizeFallList[i].SumOfPrizeFell = 0;
            //    ResetPrizeEvents(prizeFallList[i]);
            //    prizeFallList.RemoveAt(i);
            //}
            FinishActivePrize();

            foreach (IPrize<TBoard> prize in prizeList)
            {
                List<IPrize<TBoard>> prizesToDelete =
                    prizeFallList.FindAll(
                        new Predicate<IPrize<TBoard>>((IPrize<TBoard> item) => item.Tag == prize.Tag));
                foreach(IPrize<TBoard> prizeToDelete in prizesToDelete)
                {
                    prizeFallList.Remove(prizeToDelete);
                    ResetPrizeEvents(prizeToDelete);
                }
                prize.SumOfPrizeFell = 0;
            }
        }

        #endregion Level game methods

        #region Copy methods
        protected override Game.Characters.Item Copy()
        {
            Level<TBoard> level = new Levels.Level<TBoard>();
            CopyTo(level);
            return level;
        }

        protected void CopyTo(Level<TBoard> item)
        {
            item.AutoSize = AutoSize;
            item.IsLevelFinished = IsLevelFinished;
            item.IsLevelPaused = IsLevelPaused;
            item.IsLevelStarted = IsLevelStarted;
            item.MarginOfAllBrick = MarginOfAllBrick;
            item.ParentBoard = ParentBoard;
            item.prizeList = prizeList.CopyList();
            item.prizeFallList = prizeFallList.CopyList();
            item.prizeActiveList = prizeActiveList.CopyList();
            item.brickList = CopyBrick();
            item.bricksLeft = BricksLeft;
            item.prizePrecent = PrizePrecent;
            item.SizeOfBrick = SizeOfBrick;
        }

        public List<List<IBrick>> CopyBrick()
        {
            List<List<IBrick>> brickListToReturn = new List<List<BoardGame.Interface.IBrick>>();
            foreach(List<IBrick> brickList in this.brickList)
            {
                brickListToReturn.Add(new List<BoardGame.Interface.IBrick>(brickList.CopyList()));
            }
            return brickListToReturn;
        }

        #endregion Copy methods
    }
}
