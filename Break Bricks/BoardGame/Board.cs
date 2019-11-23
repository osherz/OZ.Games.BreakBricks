//#define CheckCircleOfPlank

using Break_Bricks.BoardGame.Interface;
using Break_Bricks.Levels.Interface;
using Game.Characters;
using GeometryMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Break_Bricks.BoardGame
{
    /// <summary>
    /// 
    /// </summary>
    public class Board : Items, IItemF, IBoard
    {
        #region Variables
        /// <summary>
        /// The ball that will run on the board
        /// </summary>
        private List<IBall> ballList;
        /// <summary>
        /// This ball will remember the first ball.
        /// using when the is gone.
        /// </summary>
        private IBall ballPrototype;
        /// <summary>
        /// The border that will be in top right and left
        /// </summary>
        private IBorder borderPrototype;
        /// <summary>
        /// The border will draw
        /// </summary>
        private IBorder borderToDraw;
        /// <summary>
        /// Borders arry - top, left and right
        /// </summary>
        private Dictionary<string, RectangleF> bordersDictionary;
        /// <summary>
        /// Width of border
        /// </summary>
        private float widthOfBorders;
        /// <summary>
        /// The distance of plank from bottom of board
        /// </summary>
        private float distancePlankFromBoardBottom;
        /// <summary>
        /// This list will draw
        /// </summary>
        private List<IItemF> drawList;
        /// <summary>
        /// Sum of turns
        /// </summary>
        private int turns;
        /// <summary>
        /// If the game start
        /// </summary>
        private bool isPause;
        /// <summary>
        /// If ball is on the plank
        /// </summary>
        private bool isBallOnPlank;
        /// <summary>
        /// Is the game end
        /// </summary>
        private bool isEndGame;
        /// <summary>
        /// Is the game started
        /// </summary>
        private bool isGameStart;
        /// <summary>
        /// Responsible to move ball.
        /// </summary>
        private Timer timer_BallMove;
        /// <summary>
        /// Levels list
        /// </summary>
        private List<ILevel<Board>> levelsList;
        /// <summary>
        /// this level
        /// </summary>
        private int levelNum;
        /// <summary>
        /// Score
        /// </summary>
        private int score;
        /// <summary>
        /// The score that will add when brick broke
        /// </summary>
        private int scoreAddByBrokeBrick = 1;
        /// <summary>
        /// The image that will show near numbers of turns
        /// </summary>
        private IItemF turnsImage;
        /// <summary>
        /// The font that will show the numbers of turns
        /// </summary>
        private Font fontTurns;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs before ball change
        /// </summary>
        public event EventHandler<IBall> BallBeforeChange;
        /// <summary>
        /// Occurs when ball changed
        /// </summary>
        public event EventHandler<IBall> BallChanged;
        /// <summary>
        /// Occurs before border change
        /// </summary>
        public event EventHandler<IBorder> BorderPrototypeBeforeChange;
        /// <summary>
        /// Occurs when border changed
        /// </summary>
        public event EventHandler<IBorder> BorderPrototypeChanged;
        /// <summary>
        /// Occurs when width of border changed
        /// </summary>
        public event EventHandler<float> WidthOfBordersChanged;
        /// <summary>
        /// Occurs when DistancePlankFromBoardBottom changed
        /// </summary>
        public event EventHandler<float> DistancePlankFromBoardBottomChanged;
        /// <summary>
        /// Occurs when sum of turns changed
        /// </summary>
        public event EventHandler<int> TurnsChanged;
        /// <summary>
        /// Occurs when IsStartGame changed
        /// </summary>
        public event EventHandler<bool> IsPauseChanged;
        /// <summary>
        /// Occurs when IsBallOnPlank changed
        /// </summary>
        public event EventHandler<bool> IsBallOnPlankChanged;
        /// <summary>
        /// Occurs when the game start
        /// </summary>
        public event EventHandler GameStarted;
        /// <summary>
        /// Occurs when the game end
        /// </summary>
        public event EventHandler<bool> GameEnded;
        /// <summary>
        /// Occurs when BallMoveSpeed time changed
        /// </summary>
        public event EventHandler<int> TimeBallMoveSpeedChanged;
        /// <summary>
        /// Occurs when balls added
        /// </summary>
        public event EventHandler<IBall[]> BallsAdded;
        /// <summary>
        /// Occurs when level added
        /// </summary>
        public event EventHandler<ILevel<Board>[]> LevelAdded;
        /// <summary>
        /// Occurs when level removed
        /// </summary>
        public event EventHandler<ILevel<Board>[]> LevelRemoved;
        /// <summary>
        /// Occurs before level change
        /// </summary>
        public event EventHandler<ILevel<Board>> LevelBeforeChanged;
        /// <summary>
        /// Occurs when level change
        /// </summary>
        public event EventHandler<ILevel<Board>> LevelChanged;
        /// <summary>
        /// Occurs when level num changed
        /// </summary>
        public event EventHandler<int> LevelNumChanged;
        /// <summary>
        /// Occurs when score changed
        /// </summary>
        public event EventHandler<int> ScoreChanged;
        /// <summary>
        /// Occurs when ScoreAddedByBrokeBrick changed
        /// </summary>
        public event EventHandler<int> ScoreAddByBrokeBrickChanged;
        /// <summary>
        /// Occurs when TurnsImage changed
        /// </summary>
        public event EventHandler<IItemF> TurnsImageChanged;
        /// <summary>
        /// Occurs when FontTurns changed
        /// </summary>
        public event EventHandler<Font> FontTurnsChanged;
        /// <summary>
        /// Occurs when ball fall and player fail
        /// </summary>
        public event EventHandler BallFailed;
        #endregion Events

        #region Raises events methods
        /// <summary>
        /// Raises the BallBeforeChanged event
        /// </summary>
        /// <param name="ball"></param>
        protected virtual void OnBallBeforeChanged(IBall ball)
        {

            BallBeforeChange?.Invoke(this, ball);
        }
        /// <summary>
        /// Raises the BallChanged event
        /// </summary>
        /// <param name="ball"></param>
        protected virtual void OnBallChanged(IBall ball)
        {

            BallChanged?.Invoke(this, ball);
        }

        /// <summary>
        /// Raises the BorderPrototypeBeforeChanged event
        /// </summary>
        /// <param name="border"></param>
        protected virtual void OnBorderPrototypeBeforeChanged(IBorder border)
        {

            BorderPrototypeBeforeChange?.Invoke(this, border);
        }
        /// <summary>
        /// Raises the BorderPrototypeChanged event
        /// </summary>
        /// <param name="border"></param>
        protected virtual void OnBorderPrototypeChanged(IBorder border)
        {

            BorderPrototypeChanged?.Invoke(this, border);
        }

        /// <summary>
        /// Raises the WidthOfBordersChanged event
        /// </summary>
        /// <param name="widthOfBorder"></param>
        protected virtual void OnWidthOfBordersChanged(float widthOfBorders)
        {
            WidthOfBordersChanged?.Invoke(this, widthOfBorders);
        }

        /// <summary>
        /// Raise the DistancePlankFromBoardBottomChanged event
        /// </summary>
        /// <param name="newDistancePlankFromBoardBottom"></param>
        protected virtual void OnDistancePlankFromBoardBottomChanged(float newDistancePlankFromBoardBottom)
        {

            DistancePlankFromBoardBottomChanged?.Invoke(this, newDistancePlankFromBoardBottom);
        }

        /// <summary>
        /// Raises the TurnsChanged event
        /// </summary>
        /// <param name="newTurns"></param>
        protected virtual void OnTurnsChanged(int newTurns)
        {

            TurnsChanged?.Invoke(this, newTurns);
        }

        /// <summary>
        /// Raises the IsStartGameChanged event
        /// </summary>
        /// <param name="isStartGame"></param>
        protected virtual void OnPauseChanged(bool isStartGame)
        {

            IsPauseChanged?.Invoke(this, isStartGame);
        }

        /// <summary>
        /// Raises the IsBallOnPlankChanged event
        /// </summary>
        /// <param name="isStartGame"></param>
        protected virtual void OnIsBallOnPlankChanged(bool isStartGame)
        {

            IsBallOnPlankChanged?.Invoke(this, isStartGame);
        }

        /// <summary>
        /// Raises the GameStarted event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnGameStarted(EventArgs e)
        {
            GameStarted?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the GameEnded event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnGameEnded(bool isWinAllLevels)
        {

            GameEnded?.Invoke(this, isWinAllLevels);
        }

        /// <summary>
        /// Raises the TimeBallMoveSpeedChanged event
        /// </summary>
        /// <param name="newInterval"></param>
        protected virtual void OnTimeBallMoveSpeedChanged(int newInterval)
        {

            TimeBallMoveSpeedChanged?.Invoke(this, newInterval);
        }

        /// <summary>
        /// Raises the BallsAdded event
        /// </summary>
        /// <param name="balls"></param>
        protected virtual void OnBallsAdded(IBall[] balls)
        {

            BallsAdded?.Invoke(this, balls);
        }

        /// <summary>
        /// Raises the LevelAdded event
        /// </summary>
        /// <param name="levels"></param>
        protected virtual void OnLevelAdded(params ILevel<Board>[] levels)
        {

            LevelAdded?.Invoke(this, levels);
        }

        /// <summary>
        /// Raises the LevelRemoved event
        /// </summary>
        /// <param name="levels"></param>
        protected virtual void OnLevelRemoved(params ILevel<Board>[] levels)
        {

            LevelRemoved?.Invoke(this, levels);
        }

        /// <summary>
        /// Raises the LevelBeforeChanged event
        /// </summary>
        /// <param name="levelNum"></param>
        /// <param name="level"></param>
        protected virtual void OnLevelBeforeChanged(ILevel<Board> level)
        {
            LevelBeforeChanged?.Invoke(this, level);
        }
        
        /// <summary>
        /// Raises the LevelChanged event
        /// </summary>
        /// <param name="levelNum"></param>
        /// <param name="level"></param>
        protected virtual void OnLevelChanged(int levelNum, ILevel<Board> level)
        {
            LevelNumChanged?.Invoke(this, levelNum);
            LevelChanged?.Invoke(this, level);
        }

        /// <summary>
        /// Raise the ScoreChanged event
        /// </summary>
        /// <param name="score"></param>
        protected virtual void OnScoreChanged(int score)
        {

            ScoreChanged?.Invoke(this, score);
        }

        /// <summary>
        /// Raise the ScoreAddByBrokeBrickChanged event
        /// </summary>
        /// <param name="scoreAddByBrokeBrick"></param>
        protected virtual void OnScoreAddByBrokeBrickChanged(int scoreAddByBrokeBrick)
        {

            ScoreAddByBrokeBrickChanged?.Invoke(this, scoreAddByBrokeBrick);
        }

        /// <summary>
        /// Raise the TurnsImageChanged event
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnTurnsImageChanged(IItemF item)
        {

            TurnsImageChanged?.Invoke(this, item);
        }

        /// <summary>
        /// Raise the FontTurnsChanged event
        /// </summary>
        /// <param name="font"></param>
        protected virtual void OnFontTurnsChanged(Font font)
        {

            FontTurnsChanged?.Invoke(this, font);
        }

        /// <summary>
        /// Raise the BallFailed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBallFailed(EventArgs e)
        {

            BallFailed?.Invoke(this, e);
        }
        #endregion Raises events methods

        #region Properties
        /// <summary>
        /// The ball that will run on the board
        /// </summary>
        public IBall Ball
        {
            get
            {
                return ballList[0];
            }

            set
            {
                if (Ball != null)
                {
                    drawList.Remove(Ball);
                    ResetBallEvents(Ball);
                    ResetBallPrototypeEvents();
                }
                OnBallBeforeChanged(Ball);
                ballList[0] = value;
                if (Ball != null)
                {
                    RestartBallEvents(Ball);
                    drawList.Add(Ball);
                }
                ballPrototype = Ball;
                if(ballPrototype!=null) RestartBallPrototypeEvents();
                OnBallChanged(Ball);
            }
        }

        /// <summary>
        /// Sum of balls in game
        /// </summary>
        public int SumOfBalls
        {
            get
            {
                return ballList.Count;
            }
        }

        /// <summary>
        /// The border that will be in top right and left
        /// </summary>
        public IBorder BorderPrototype
        {
            get
            {
                return borderPrototype;
            }

            set
            {
                OnBorderPrototypeBeforeChanged(BorderPrototype);
                borderPrototype = value;
                if(BorderPrototype!=null)
                {
                    RestartBorders();
                }
                OnBorderPrototypeChanged(BorderPrototype);
            }
        }

        /// <summary>
        /// Width of border
        /// </summary>
        public float WidthOfBorders
        {
            get
            {
                return widthOfBorders;
            }

            set
            {
                widthOfBorders = value;
                UpdateBordersToNewSizeAndLocation();
                OnWidthOfBordersChanged(WidthOfBorders);
            }
        }

        /// <summary>
        /// The distance of plank from bottom of board
        /// </summary>
        public float DistancePlankFromBoardBottom
        {
            get
            {
                return distancePlankFromBoardBottom;
            }

            set
            {
                distancePlankFromBoardBottom = value;
                OnDistancePlankFromBoardBottomChanged(DistancePlankFromBoardBottom);
            }
        }

        /// <summary>
        /// Sum of turns.
        /// must be bigger than 0.
        /// </summary>
        public int Turns
        {
            get
            {
                return turns;
            }

            set
            {
                if(value>=0)
                {
                    turns = value;
                    OnTurnsChanged(Turns);
                }
            }
        }

        /// <summary>
        /// If the game start
        /// </summary>
        public bool IsPause
        {
            get
            {
                return isPause;
            }

            protected set
            { 
                isPause = value;
                OnPauseChanged(IsPause);
            }
        }

        /// <summary>
        /// If ball is on the plank
        /// </summary>
        public bool IsBallOnPlank
        {
            get
            {
                return isBallOnPlank;
            }

            protected set
            {
                isBallOnPlank = value;
                OnIsBallOnPlankChanged(IsBallOnPlank);
            }
        }

        /// <summary>
        /// If game is end
        /// </summary>
        public bool IsEndGame
        {
            get
            {
                return isEndGame;
            }

            private set
            {
                isEndGame = value;
                OnGameEnded(false);
            }
        }

        /// <summary>
        /// If game started
        /// </summary>
        public bool IsGameStart
        {
            get
            {
                return isGameStart;
            }

            private set
            {
                isGameStart = value;
                if (IsGameStart)
                {
                    OnGameStarted(new EventArgs());
                    IsPause = false;
                }
            }
        }

        /// <summary>
        /// The time of ball move speed
        /// </summary>
        public int IntervalBallSpeed
        {
            get
            {
                return timer_BallMove.Interval;
            }

            set
            {
                if(value > 0)
                {
                    timer_BallMove.Interval = value;
                    OnTimeBallMoveSpeedChanged(IntervalBallSpeed);
                }
            }
        }

        /// <summary>
        /// Get/Set the level that the user play int. the level musr be big than 0.
        /// </summary>
        public int LevelNum
        {
            get
            {
                return levelNum;
            }

            set
            {
                if(value>0)
                {
                    if (LevelNum > 0)
                    {
                        ResetLevelEvents(Level);
                        OnLevelBeforeChanged(Level);
                    }
                    levelNum = value;
                    RestartLevelEvents(Level);
                    OnLevelChanged(LevelNum, Level);
                }
            }
        }

        public ILevel<Board> Level
        {
            get
            {
                return levelsList[LevelNum - 1];
            }
        }

        /// <summary>
        /// Sum of levels in board
        /// </summary>
        public int SumOfLevels
        {
            get
            {
                return levelsList.Count;
            }
        }

        public float BallSpeed
        {
            get
            {
                return Ball.Speed;
            }

            set
            {
                foreach(IBall ball in ballList)
                {
                    ball.Speed = value;
                }
                ballPrototype.Speed = value;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                if(value >= 0)
                {
                    score = value;
                    OnScoreChanged(Score);
                }
            }
        }

        /// <summary>
        /// The score that will add when brick broke
        /// </summary>
        public int ScoreAddByBorokeBrick
        {
            get
            {
                return scoreAddByBrokeBrick;
            }

            set
            {
                if (value > 0)
                {
                    scoreAddByBrokeBrick = value;
                    OnScoreChanged(ScoreAddByBorokeBrick);
                }
            }
        }

        /// <summary>
        /// The image that will show near numbers of turns
        /// </summary>
        public IItemF TurnsImage
        {
            get
            {
                return turnsImage;
            }

            set
            {
                turnsImage = value;
                if(TurnsImage != null)
                {
                    TurnsImage.Location = new PointF(2, 0);
                    TurnsImage.Size = new SizeF(20, 20);
                }
                OnTurnsImageChanged(TurnsImage);
            }
        }

        public Font FontTurns
        {
            get
            {
                return fontTurns;
            }

            set
            {
                fontTurns = value;
                OnFontTurnsChanged(FontTurns);
            }
        }
        #endregion Properties

        #region Contructors
        public Board()
        {
            levelsList = new List<Interface.ILevel<BoardGame.Board>>();
            ballList = new List<IBall>();
            ballList.Add(default(IBall));
            string[] keys = GetBordersKeys();
            bordersDictionary = new Dictionary<string, RectangleF>();
            foreach (string key in keys)
            {
                bordersDictionary.Add(key, new RectangleF());
            }
            drawList = new List<IItemF>();
            timer_BallMove = new Timer();
            timer_BallMove.Interval = 1;
            timer_BallMove.Tick += new EventHandler(Ball_TimerMove);

            FontTurns = new Font("David", 15, FontStyle.Bold);
        }
        #endregion Contructors

        #region Reset/Restart events
        protected virtual void ResetBallEvents(IBall ball)
        {
            ball.Moved -= Ball_OnMoved;

        }

        protected virtual void RestartBallEvents(IBall ball)
        {
            ball.Moved += Ball_OnMoved;
        }

        protected virtual void ResetBallPrototypeEvents()
        {
            ballPrototype.SizeChanged += Ball_SizeChanged;

        }

        protected virtual void RestartBallPrototypeEvents()
        {
            ballPrototype.SizeChanged += Ball_SizeChanged;
        }

        /// <summary>
        /// Occurs before plank changed
        /// </summary>
        protected override void ResetPlankEvents()
        {
            if(base.Plank != null)
            {
                base.Plank.Moved -= Plank_OnMoved;
                base.Plank.SizeChanged -= Plank_SizeChanged;
            }
        }

        /// <summary>
        /// Occurs after plank changed
        /// </summary>
        protected override void RestartPlankEvents()
        {
            if (base.Plank != null)
            {
                base.Plank.Moved += Plank_OnMoved;
                base.Plank.SizeChanged += Plank_SizeChanged;
            }
        }

        protected virtual void ResetBorderPrototypeEvents()
        {
            if(BorderPrototype!=null)
            {
                BorderPrototype.LookChanged -= BorderPrototype_OnLookChanged;
            }
        }

        protected virtual void RestartPrototypeEvents()
        {
            if (BorderPrototype != null)
            {
                BorderPrototype.LookChanged += BorderPrototype_OnLookChanged;
            }
        }

        protected virtual void ResetLevelEvents(ILevel<Board> level)
        {
            if(level!=null)
            {
                level.LevelFinished -= Level_OnEnded;
                level.BrickBroke -= Level_OnBrickBroke;
            }
        }

        protected virtual void RestartLevelEvents(ILevel<Board> level)
        {
            if (level != null)
            {
                level.LevelFinished += Level_OnEnded;
                level.BrickBroke += Level_OnBrickBroke;
            }
        }

        #endregion Reset/Restart events

        #region Methods private
        /// <summary>
        /// Return the keys of borders dictionary
        /// </summary>
        /// <returns></returns>
        private string[] GetBordersKeys()
        {
            return new string[] {"Top", "Right", "Left" };
        }
        
        /// <summary>
        /// Clone the BorderPrototype to bordersArry
        /// </summary>
        private void RestartBorders()
        {
            borderToDraw = (IBorder)BorderPrototype.Clone();
            UpdateBordersToNewSizeAndLocation();
        }

        /// <summary>
        /// Update borders to new location and/or size
        /// </summary>
        private void UpdateBordersToNewSizeAndLocation()
        {
            ///TOP 
            bordersDictionary["Top"] = new RectangleF(new PointF(0, 0), new SizeF(Width, WidthOfBorders));
            ///Left
            bordersDictionary["Left"] = new RectangleF(new PointF(0, 0), new SizeF(WidthOfBorders, Height));
            ///Right
            bordersDictionary["Right"] = new RectangleF(new PointF(Width - WidthOfBorders, 0), new SizeF(WidthOfBorders, Height));
        }

        /// <summary>
        /// Draw borders
        /// </summary>
        /// <param name="g"></param>
        private void DrawBorders(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            string[] bordersPositions = GetBordersKeys();
            for (int i = 0; i < bordersPositions.Length; i++)
            {
                RectangleF rect = bordersDictionary[bordersPositions[i]];
                borderToDraw.Width = rect.Size.Width + extraWidth;
                borderToDraw.Height = rect.Size.Height + extraHeight;
                borderToDraw.Left = rect.Left +Left + extraX;
                borderToDraw.Top = rect.Top + Top + extraY;
                borderToDraw.Draw(g);
            }
        }

        /// <summary>
        /// Check if ball hitted any borders.
        /// if true' the method change ball direction
        /// </summary>
        /// <returns></returns>
        private bool CheckIfBallHitBorders(IBall ball = default(IBall))
        {
            if (ball == null) ball = Ball;
            bool isHit = false;
            if (ball.DirectionY <= 0 && ball.Top <= bordersDictionary["Top"].Bottom)
            {
                ball.DirectionY *= -1;
                isHit = true;
            }
            if (ball.DirectionX <= 0 && ball.Left <= bordersDictionary["Left"].Right)
            { 
                ball.DirectionX *= -1;
                isHit = true;
            }
            if(ball.DirectionX >= 0 && ball.Right >= bordersDictionary["Right"].Left)
            {
                ball.DirectionX *= -1;
                isHit = true;
            }
            return isHit;
        }

        /// <summary>
        /// Check if player fail by checking if the ball fall
        /// </summary>
        /// <returns></returns>
        private bool CheckIfBallFallDownThePlank(IBall ball = default(IBall))
        {
            if (ball == null) ball = Ball;
            return ball.Top >= base.Plank.Bottom;
        }
        
        /// <summary>
        /// Shoot ball from plank
        /// </summary>
        private void BallHitPlank(IBall ball = default(IBall))
        {
            /*
             * הפונקציה שולחת את הכדור לכיוון מסויים בהתאם למיקומו על הקרש
             * הפונקציב מתייחסת אל מרכז הקרש כאל מרכז של עיגול
             * הפונקציה מתייחסת אל הכדור כאילו היה על המעגל ואז מחפשת את המיקום שלו
             * היחס בין הX לY הוא כיוון הכדור
             */
            if (ball == null) ball = Ball;
            ball.DirectionY = -1;
            float rBall = ball.Width / 2; // Radius of ball
            float rPlank = Plank.Width / 2 + ball.Width + ball.Speed/2; //Radius of circle of plank
            PointF centerPlank = new PointF(Plank.Left + Plank.Width / 2, Plank.Top);
            double[] y = Geometry.FindPointOnCircleY(rPlank, centerPlank.X, centerPlank.Y, ball.Left + rBall);
            PointF pointOnCircle = new PointF(ball.Left + rBall, (float)Math.Min(y[0], y[1]));
            float relationPoints = Math.Abs((pointOnCircle.X - centerPlank.X) / (pointOnCircle.Y - centerPlank.Y));
            ball.DirectionX = relationPoints;
            if (ball.Left + rBall < centerPlank.X) ball.DirectionX *= -1;
        }

        #endregion Methods private

        #region Events methods

        protected virtual void BorderPrototype_OnLookChanged(object sender, EventArgs e)
        {
            borderToDraw = (IBorder)BorderPrototype.Clone();
        }

        protected override void OnSizeChanged(SizeF newSize)
        {
            UpdateBordersToNewSizeAndLocation();
            base.OnSizeChanged(newSize);
        }

        /// <summary>
        /// Move the ball on board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Ball_TimerMove(object sender, EventArgs e)
        {
            int sumOfBalls = ballList.Count;
            IBall[] balls = ballList.ToArray();
            for (int i = 0; i < sumOfBalls; i++) balls[i].Move();
        }

        private void Ball_SizeChanged(object sender, SizeF e)
        {
            foreach(IBall ball in ballList)
            {
                if(ball!=sender) ball.Size = e;
            }
        }

        protected virtual void Level_OnEnded(object sender, EventArgs e)
        {

            LevelUp();
        }

        private void Level_OnBrickBroke(object sender, IBrick e)
        {
            Score += scoreAddByBrokeBrick * e.SumOfLvls * levelNum;
        }
        #endregion Events methods

        #region Methods public
        /// <summary>
        /// Draw all board
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {
            DrawBorders(g, extraX, extraY, extraWidth, extraHeight);
            foreach(IBall ball in ballList)
            {
                    ball.Draw(g, Left + extraX, Top + extraY, extraWidth, extraHeight);
            }
            Plank.Draw(g, Left + extraX, Top + extraY, extraWidth, extraHeight);
            DrawTruns(g, Left + extraX, Top + extraY, extraWidth, extraHeight);
            Level.Draw(g, Left + extraX, Top + extraY, extraWidth, extraHeight);

#if CheckCircleOfPlank
            if (IsBallOnPlank)
            {
                g.DrawEllipse(Pens.Black,
                    plank.Left + Left - ball.Width / 2 - ball.Speed/2,
                    Top + plank.Top - plank.Width / 2 - ball.Width / 2 - ball.Speed / 2,
                    plank.Width + ball.Width + ball.Speed ,
                    plank.Width + ball.Width + ball.Speed);
                float newPointY = (float)Geometry.FindPointOnCircleY(plank.Width / 2 + ball.Width/2 + ball.Speed/2 , Left + plank.Left + plank.Width / 2, Top + plank.Top, Left + ball.Left + ball.Width / 2)[0];
                g.DrawEllipse(Pens.Red, Left + ball.Left + ball.Width / 2,newPointY , 5, 5);
                g.DrawLine(Pens.Black, Left + plank.Left + plank.Width / 2, Top + plank.Top, Left + ball.Left + ball.Width / 2, newPointY);

                BallHitPlank();
                SizeF sizeOfBallMoving = new SizeF(ball.Speed + 10, ball.Speed + 10);
                sizeOfBallMoving.Width *= ballDirection.Width;
                sizeOfBallMoving.Height *= ballDirection.Height;
                g.DrawLine(Pens.Black, Left + ball.Left, Top + ball.Top,Left+ball.Left+ sizeOfBallMoving.Width, Top+ball.Top+sizeOfBallMoving.Height);
            }
#endif
            base.Draw(g);
        }

        protected virtual void DrawTruns(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight=0)
        {
            TurnsImage.Draw(g, extraX, extraY, extraWidth, extraHeight);

            SizeF turnsStringSize = g.MeasureString("X " + Turns.ToString(), FontTurns);
            g.DrawString("X " + Turns.ToString(), new Font("David", 15, FontStyle.Bold), Brushes.White,
                extraX + TurnsImage.Right, extraY + TurnsImage.Top + (TurnsImage.Height - turnsStringSize.Height) / 2 + 2);
        }
        #endregion Methods public

        #region Move plank and ball method
        /// <summary>
        /// Move plank to left
        /// </summary>
        public virtual void Plank_GoLeft()
        {
            if (!IsPause && IsGameStart)
            {
                Plank.StartMove(Direction.Left);
            }
        }

        /// <summary>
        /// Move plank to right
        /// </summary>
        public virtual void Plank_GoRight()
        {
            if (!IsPause && IsGameStart)
            {
                Plank.StartMove(Direction.Right);
            }

        }

        public virtual void Plank_StopMove()
        {
            Plank.StopMove();
        }

        /// <summary>
        /// Occurs when the plank moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newLocation"></param>
        protected virtual void Plank_OnMoved(object sender, PointF newLocation)
        {
            Plank_UpdateLocationInBoard();
            if (IsBallOnPlank) Ball.Left = base.Plank.Left + (base.Plank.Width - Ball.Width) / 2;
        }

        private void Plank_SizeChanged(object sender, SizeF e)
        {
            Plank_UpdateLocationInBoard();
        }

        /// <summary>
        /// Update the location of plank with the borders
        /// </summary>
        protected void Plank_UpdateLocationInBoard()
        {
            if (base.Plank.Left <= bordersDictionary["Left"].Right)
                base.Plank.Left = bordersDictionary["Left"].Right + 0.1f;
            else if (base.Plank.Right >= bordersDictionary["Right"].Left)
                base.Plank.Right = bordersDictionary["Right"].Left - 0.1f;
        }

        /// <summary>
        /// Move ball when on plank to left
        /// </summary>
        public virtual void Ball_GoLeft()
        {
            if (!IsPause && IsBallOnPlank && Ball.Left > base.Plank.Left) Ball.GoLeft();
        }

        /// <summary>
        /// Move ball when on plank to right
        /// </summary>
        public virtual void Ball_GoRight()
        {
            if (!IsPause && IsBallOnPlank && Ball.Right < Plank.Right) Ball.GoRight();
        }

        /// <summary>
        /// Shoot the ball from plank
        /// </summary>
        public void ShootBall()
        {
            if (IsBallOnPlank && IsGameStart && !IsPause && !IsEndGame)
            {
                BallHitPlank();
                timer_BallMove.Start();
                IsBallOnPlank = false;
            }
        }

        /// <summary>
        /// Occurs when the ball moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newLocation"></param>
        protected virtual void Ball_OnMoved(object sender, PointF newLocation)
        {
            if (IsGameStart && !IsPause && !IsBallOnPlank)
            {
                IBall ball = (IBall)sender;
                if (CheckIfBallHitBorders(ball))
                {

                }
                else if (ball.Bottom >= Plank.Top &&
                    (ball.Right >= Plank.Left && ball.Right <= Plank.Right ||
                    ball.Left <= Plank.Right && ball.Left >= Plank.Left)) //If hit plank
                {
                    BallHitPlank(ball);
                }
                else if (CheckIfBallFallDownThePlank(ball))
                {
                    if (turns != 0 || ballList.Count > 0)
                    {
                        if (ballList.Count == 1)
                        {
                            timer_BallMove.Stop();
                            Level.ClearFallPrize();
                            UpdateBallAndPlankToMiddle();
                            DestroyTurn();
                        }
                        else
                        {
                            ballList.Remove(ball);
                            ResetBallEvents(ball);
                            if (ballList.Count == 1)
                            {
                                ballPrototype.Location = Ball.Location;
                                ballPrototype.Direction = Ball.Direction;
                                Ball = ballPrototype;
                            }
                        }
                    }
                }
                else //if (SumOfLevels > 0 && levelsList[levelNum - 1].GetRectangleF().IntersectsWith(ball.GetRectangleF()))
                {
                    Direction hitBrickDirection = levelsList[levelNum - 1].CheckIfBallHitBricks(ball);
                    if (hitBrickDirection == (Direction.Top | Direction.Left))
                    {
                        if (ball.DirectionX > 0) ball.DirectionX *= -1;
                        if (ball.DirectionY > 0) ball.DirectionY *= -1;
                        ball.Left += 0.1f;
                    }
                    else if (hitBrickDirection == (Direction.Top | Direction.Right))
                    {
                        if (ball.DirectionX < 0) ball.DirectionX *= -1;
                        if (ball.DirectionY > 0) ball.DirectionY *= -1;
                        ball.Left += 0.1f;
                    }
                    else if (hitBrickDirection == (Direction.Bottom | Direction.Right))
                    {
                        if (ball.DirectionX < 0) ball.DirectionX *= -1;
                        if (ball.DirectionY < 0) ball.DirectionY *= -1;
                        ball.Left += 0.1f;
                    }
                    else if (hitBrickDirection == (Direction.Bottom | Direction.Left))
                    {
                        if (ball.DirectionX > 0) ball.DirectionX *= -1;
                        if (ball.DirectionY < 0) ball.DirectionY *= -1;
                        ball.Left += 0.1f;
                    }
                    else
                    {
                        switch (hitBrickDirection)
                        {
                            case Direction.Bottom:
                                if (ball.DirectionY < 0) ball.DirectionY *= -1;
                                break;
                            case Direction.Top:
                                if (ball.DirectionY > 0) ball.DirectionY *= -1;
                                break;
                            case Direction.Right:
                                if (ball.DirectionX < 0) ball.DirectionX *= -1;
                                break;
                            case Direction.Left:
                                if (ball.DirectionX > 0) ball.DirectionX *= -1;
                                break;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update the location of ball and plank to middle
        /// </summary>
        public void UpdateBallAndPlankToMiddle()
        {
            Plank.Left = (Width - base.Plank.Width) / 2;
            Plank.Bottom = Height - DistancePlankFromBoardBottom;
            Ball.Left = Plank.Left + (Plank.Width - Ball.Width) / 2;
            Ball.Top = Plank.Top - Ball.Height;
            IsBallOnPlank = true;
        }

        /// <summary>
        /// Add balls the the exict ball
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="powBySumOfBallsExict">If true nubmers of balls will kefel by numbers of ball that exict</param>
        public void AddBall(int sum, bool kefelBySumOfBallsExict = false)
        {
            if (kefelBySumOfBallsExict) sum *= this.ballList.Count;
            List<IBall> ballList = new List<IBall>();
            float moveByX = 0.2f;
            while(sum>0)
            {
                this.ballList.Add((IBall)Ball.Clone());
                this.ballList[this.ballList.Count - 1].DirectionX += moveByX;
                RestartBallEvents(this.ballList[this.ballList.Count - 1]);
                moveByX *= 1.5f;
                sum--;
                ballList.Add(this.ballList[this.ballList.Count - 1]);
            }
            OnBallsAdded(ballList.ToArray());
        }
        #endregion Move plank and ball method

        #region Game method
        /// <summary>
        /// Start the game
        /// </summary>
        public void Start(int turns, int levelNum = 1)
        {
            Pause();
            if(LevelNum >= 1 && levelNum == 1)
            {
                Level.ClearFallPrize();
                Score = 0;
            }
            Turns = turns;
            ResetBalls();
            if (SumOfLevels > 0) LevelNum = levelNum;
            Level.StartLevel();
            IsEndGame = false;
            IsGameStart = true;
        }

        private void ResetBalls()
        {
            timer_BallMove.Stop();
            if (ballList.Count >= 1) ballList.RemoveRange(1, ballList.Count - 1);
            Ball = ballPrototype;
            UpdateBallAndPlankToMiddle();
        }

        public void PauseOrContinue()
        {
            if (IsPause) Continue();
            else Pause();
        }

        /// <summary>
        /// Start the game.
        /// </summary>
        public void Pause()
        {
            if (IsGameStart)
            {
                IsPause = true;
                timer_BallMove.Stop();
                Plank_StopMove();
                Level.Pause();
            }
        }

        /// <summary>
        /// Continue the game.
        /// </summary>
        public void Continue()
        {
            if (IsGameStart)
            {
                IsPause = false;
                if(!IsBallOnPlank)
                    timer_BallMove.Start();
                Level.Continue();
            }
        }

        public void DestroyBalls()
        {
            if (isGameStart)
            {
                DestroyTurn();
                Level.ClearFallPrize();
                ResetBalls();
            }
        }

        private void DestroyTurn()
        {
            if (Turns > 0)
                Turns--;
            else EndGame();
            OnBallFailed(new EventArgs());
        }

        /// <summary>
        /// End the game.
        /// </summary>
        public void EndGame(bool isWinAllLevels = false)
        {
            Pause();
            isEndGame = true;
            OnGameEnded(isWinAllLevels);
        }
        #endregion Game method

        #region Level methods
        /// <summary>
        /// Raise to next level
        /// </summary>
        public void LevelUp()
        {
            if (LevelNum < SumOfLevels)
            {
                Start(Turns, LevelNum+1);
            }
            else
            {
                EndGame(true);
            }
        }

        public void AddLevel(params ILevel<Board>[] levels)
        {
            levelsList.AddRange(levels);
            OnLevelAdded(levels);
        }

        public void RemoveLevel(params ILevel<Board>[] levels)
        {
            List<ILevel<Board>> removedLevel = new List<Interface.ILevel<BoardGame.Board>>();
            foreach(ILevel<Board> level in levels)
            {
                if (levelsList.Remove(level)) removedLevel.Add(level);
            }
            if(removedLevel.Count>0) OnLevelRemoved(removedLevel.ToArray());
        }
        #endregion Level methods

        protected override Game.Characters.Item Copy()
        {
            Board board = new BoardGame.Board();
            board.Ball = (IBall)Ball.Clone();
            board.Plank = (IPlank)Plank.Clone();
            board.BorderPrototype = (IBorder)BorderPrototype.Clone();
            board.borderToDraw = (IBorder)borderToDraw.Clone();
            board.WidthOfBorders = WidthOfBorders;
            board.Turns = Turns;
            board.IsPause = IsPause;
            board.IsEndGame = IsEndGame;
            board.IsGameStart = IsGameStart;
            board.IntervalBallSpeed = IntervalBallSpeed;
            return board;
        }
    }
}
