using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Characters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">int/float (for location)</typeparam>
    /// <typeparam name="TPoint">Point/PointF (for location)</typeparam>
    /// <typeparam name="TSize">Size/SizeF</typeparam>
    public abstract class Item : IItemF, ITimer, ICloneable
    {
        #region Variables
        /// <summary>
        /// Loction of character
        /// </summary>
        private PointF location;
        /// <summary>
        /// Size of character
        /// </summary>
        private SizeF size;
        /// <summary>
        /// Timet for ITimer
        /// </summary>
        private Timer timer;
        /// <summary>
        /// If change size relation the containing
        /// </summary>
        private bool autoSize = false;

        private object tag;
        #endregion Variable

        #region Events
        /// <summary>
        /// Occurs when drew
        /// </summary>
        public event EventHandler Drew;
        /// <summary>
        /// Occurs when location changed
        /// </summary>
        public event EventHandler<PointF> LocationChanged;
        /// <summary>
        /// Occurs when size changed
        /// </summary>
        public event EventHandler<SizeF> SizeChanged;
        /// <summary>
        /// Occurs when tick
        /// </summary>
        public event EventHandler Tick;
        /// <summary>
        /// Occurs when interval changed
        /// </summary>
        public event EventHandler<int> TimerIntervalChanged;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        public event EventHandler LookChanged;
        /// <summary>
        /// Occurs when AutoSize changed
        /// </summary>
        public event EventHandler<bool> AutoSizeChanged;
        public event EventHandler<object> TagChanged;
        #endregion Events

        #region Constructors
        public Item()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(Timer_OnTick);
        }
        #endregion Constructors

        #region Raises events method
        /// <summary>
        /// Raises the Drew event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDrew(EventArgs e)
        {

            Drew?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the LookChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLookChanged(EventArgs e)
        {

            LookChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the LocationChaanged event
        /// </summary>
        /// <param name="newLocation"></param>
        protected virtual void OnLocationChanged(PointF newLocation)
        {

            LocationChanged?.Invoke(this, newLocation);
        }
        /// <summary>
        /// Raises the SizeChaanged event
        /// </summary>
        /// <param name="newLocation"></param>
        protected virtual void OnSizeChanged(SizeF newSize)
        {

            SizeChanged?.Invoke(this, newSize);
            OnLookChanged(new EventArgs());
        }
        /// <summary>
        /// Raises the Tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Timer_OnTick(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Raises the TimerIntervalChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTimerIntervalChanged(int newInterval)
        {

            TimerIntervalChanged?.Invoke(this, newInterval);
        }

        /// <summary>
        /// Raises the TimerIntervalChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnAutoSizeChanged(bool autoSize)
        {

            AutoSizeChanged?.Invoke(this, autoSize);
        }

        protected virtual void OnTagChanged(object tag)
        {

            TagChanged?.Invoke(this, tag);
        }
        #endregion

        #region Properties
        public virtual PointF Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                OnLocationChanged(Location);
            }
        }

        public virtual float Top
        {
            get
            {
                return Location.Y;
            }

            set
            {
                location.Y = value;
                OnLocationChanged(Location);
            }
        }

        public virtual float Left
        {
            get
            {
                return Location.X;
            }

            set
            {
                location.X = value;
                OnLocationChanged(Location);
            }
        }

        public virtual float Right
        {
            get
            {
                return Left + Width;
            }

            set
            {
                location.X = value - Width;
                OnLocationChanged(Location);
            }
        }

        public virtual float Bottom
        {
            get
            {
                return Top + Height;
            }

            set
            {
                location.Y = value - Height;
                OnLocationChanged(Location);
            }
        }

        public virtual SizeF Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
                OnSizeChanged(Size);
            }
        }

        public virtual float Width
        {
            get
            {
                return Size.Width;
            }

            set
            {
                Size = new SizeF(value, Height);
            }
        }

        public virtual float Height
        {
            get
            {
                return Size.Height;
            }

            set
            {
                Size = new SizeF(Width, value);
            }
        }

        public virtual int TimerInterval
        {
            get
            {
                return timer.Interval;
            }

            set
            {
                if(value>0)
                {
                    timer.Interval = value;
                    OnTimerIntervalChanged(timer.Interval);
                }
            }
        }

        public virtual bool AutoSize
        {
            get
            {
                return autoSize;
            }

            set
            {
                autoSize = value;
                OnAutoSizeChanged(AutoSize);
            }
        }

        public object Tag
        {
            get
            {
                return tag;
            }

            set
            {
                tag = value;
                OnTagChanged(Tag);
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Draw shape
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraX"></param>
        /// <param name="extraY"></param>
        /// <param name="extraWidth"></param>
        /// <param name="extraHeight"></param>
        public virtual void Draw(Graphics g, float extraX = 0, float extraY = 0, float extraWidth = 0, float extraHeight = 0)
        {

            OnDrew(new EventArgs());
        }


        /// <summary>
        /// Add location to original location, without change him, just for drawing
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraX"></param>
        /// <param name="extraY"></param>
        public virtual void DrawWithExtraLocation(Graphics g, float extraX, float extraY)
        {
            Draw(g, extraX, extraY);
        }

        /// <summary>
        /// Add size to original size, without change him, just for drawing
        /// </summary>
        /// <param name="g"></param>
        /// <param name="extraWidth"></param>
        /// <param name="extraHeight"></param>
        public virtual void DrawWithExtraSize(Graphics g, float extraWidth, float extraHeight)
        {
            Draw(g, 0, 0, extraWidth, extraHeight);
        }
        /// <summary>
        /// Starts the timer.
        /// </summary>
        public virtual void StartTimer()
        {
            timer.Start();
        }
        /// <summary>
        /// Stops the timer.
        /// </summary>
        public virtual void StopTimer()
        {
            timer.Stop();
        }

        public virtual object Clone()
        {
            Item item = Copy();
            item.Location = Location;
            item.Size = Size;
            item.AutoSize = AutoSize;
            item.Tag = Tag;
            return item;
        }

        /// <summary>
        /// return the specific object with the new values
        /// </summary>
        /// <returns></returns>
        protected abstract Item Copy();
        #endregion Methods

    }
}
