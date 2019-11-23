using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Characters
{
    public abstract class GameObject : Figure, IMoveF,IMoveAutoF, IMoveBy<SizeF>, ISpeedF, ICloneable, IDirectionF
    {
        public GameObject()
        {
            moveTimer = new Timer();
            moveTimer.Tick += OnMoveTimer_Tick;
            MoveAutoInterval = 1;
        }

        #region Variables
        /// <summary>
        /// Speed of Charcter
        /// </summary>
        private float speed;
        /// <summary>
        /// The object will move by this point
        /// </summary>
        private PointF moveDirection;
        /// <summary>
        /// Move the plank auto
        /// </summary>
        private Timer moveTimer;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when moved
        /// </summary>
        public event EventHandler<PointF> Moved;
        /// <summary>
        /// Occurs when speed changed
        /// </summary>
        public event EventHandler<float> SpeedChanged;
        /// <summary>
        /// Occurs when direction changed
        /// </summary>
        public event EventHandler<PointF> DirectionChanged;
        /// <summary>
        /// Occurs when MoveAutoInterval changed
        /// </summary>
        public EventHandler<int> MoveAutoIntervalChanged;
        #endregion Events

        #region Raises events method
        /// <summary>
        /// Raises the Moved event
        /// </summary>
        /// <param name="newPoint"></param>
        protected virtual void OnMoved(PointF newPoint)
        {

            Moved?.Invoke(this, newPoint);
        }
        /// <summary>
        /// Raises the SpeedChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSpeedChanged(float speed)
        {

            SpeedChanged?.Invoke(this, speed);
        }
        /// <summary>
        /// Raises the DirectionChanged event
        /// </summary>
        /// <param name="newDirection"></param>
        protected virtual void OnDirectionChanged(PointF newDirection)
        {

            DirectionChanged?.Invoke(this, newDirection);
        }
        /// <summary>
        /// Raises the MoveAutoIntervalChanged event
        /// </summary>
        /// <param name="moveAutoInterval"></param>
        protected virtual void OnMoveAutoIntervalChanged(int moveAutoInterval)
        {

            MoveAutoIntervalChanged?.Invoke(this, moveAutoInterval);
        }
        #endregion Raises events method

        #region Properties
        /// <summary>
        /// Get/Set speed character
        /// </summary>
        public virtual float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
                OnSpeedChanged(Speed);
            }
        }

        /// <summary>
        /// The direction that will move to by X
        /// </summary>
        public float DirectionX
        {
            get
            {
                return Direction.X;
            }

            set
            {
                moveDirection.X = value;
                OnDirectionChanged(Direction);
            }
        }

        /// <summary>
        /// The direction that will move to by Y
        /// </summary>
        public float DirectionY
        {
            get
            {
                return Direction.Y;
            }

            set
            {
                moveDirection.Y = value;
                OnDirectionChanged(Direction);
            }
        }

        /// <summary>
        /// The direction that willl move by him
        /// </summary>
        public PointF Direction
        {
            get
            {
                return moveDirection;
            }

            set
            {
                moveDirection = value;
                OnDirectionChanged(Direction);
            }
        }

        public virtual int MoveAutoInterval
        {
            get
            {
                return moveTimer.Interval;
            }

            set
            {
                moveTimer.Interval = value;
                if (value >= 0)
                {
                    OnMoveAutoIntervalChanged(MoveAutoInterval);
                }
            }
        }
        #endregion Properties

        #region Move methods
        /// <summary>
        /// Move character down
        /// </summary>
        public virtual void GoDown()
        {
            Move(0, 1);
        }

        /// <summary>
        /// Move character left
        /// </summary>
        public virtual void GoLeft()
        {
            Move(-1, 0);
        }

        /// <summary>
        /// Move character right
        /// </summary>
        public virtual void GoRight()
        {
            Move(1, 0);
        }

        /// <summary>
        /// Move character up
        /// </summary>
        public virtual void GoUp()
        {
            Move(0, -1);
        }

        /// <summary>
        /// THE PARAMTER ARE NOT LOCATION!!
        /// x and y using for move's direction
        /// for example:
        /// 1. if you want character move down: x=0 - y=1.
        /// 2. move left: x=-1, y=0.
        /// 3. move up-left: x=-1, y=-1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void Move(float x, float y)
        {
            Location = new PointF(Left + speed * x, Top + speed * y);
            OnMoved(Location);
        }

        /// <summary>
        /// Moving by direction
        /// </summary>
        public virtual void Move()
        {

            Move(DirectionX, DirectionY);
        }

        /// <summary>
        /// Move by specific size
        /// </summary>
        /// <param name="sizeToMove"></param>
        public void MoveBy(SizeF sizeToMove)
        {
            Location = new PointF(Left + sizeToMove.Width, Top + sizeToMove.Height);
            OnMoved(Location);
        }

        protected virtual void OnMoveTimer_Tick(object sender, EventArgs e)
        {
            Move();
        }


        public void StartMove()
        {
            moveTimer.Start();
        }

        public void StartMove(Direction direction)
        {
            switch (direction)
            {
                case Characters.Direction.Bottom:
                    DirectionY = 1;
                    DirectionX = 0;
                    break;
                case Characters.Direction.Top:
                    DirectionY = -1;
                    DirectionX = 0;
                    break;
                case Characters.Direction.Left:
                    DirectionY = 0;
                    DirectionX = -1;
                    break;
                case Characters.Direction.Right:
                    DirectionY = 0;
                    DirectionX = 1;
                    break;
            }
            StartMove();
        }

        /// <summary>
        /// THE PARAMTER ARE NOT LOCATION!!
        /// x and y using for move's direction
        /// for example:
        /// 1. if you want character move down: x=0 - y=1.
        /// 2. move left: x=-1, y=0.
        /// 3. move up-left: x=-1, y=-1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void StartMove(float x, float y)
        {
            DirectionX = x;
            DirectionX = y;
            StartMove();
        }

        public void StopMove()
        {
            moveTimer.Stop();
        }

        #endregion Move methods

        public override object Clone()
        {
            GameObject gameObject = (GameObject)base.Clone();
            gameObject.Speed = Speed;
            gameObject.Direction = Direction;
            gameObject.MoveAutoInterval = MoveAutoInterval;
            return gameObject;
        }
    }
}
