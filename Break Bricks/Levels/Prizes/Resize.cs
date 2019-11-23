using Break_Bricks.BoardGame.Interface;
using Break_Bricks.Levels.Interface;
using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Break_Bricks.Levels.Prizes
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBoard"></typeparam>
    /// <typeparam name="T">int/float</typeparam>
    /// <typeparam name="TPoint">Point/PointF</typeparam>
    /// <typeparam name="TSize">Size/SizeF</typeparam>
    public abstract class Resize<TBoard, T, TSize, TPoint, TObjectToResize> : PrizeOnTime<TBoard>, IPrize<TBoard>
        where TBoard : IBoard
        where TObjectToResize: ILocation<T, TPoint>, ISize<T, TSize>
    {
        public Resize(TObjectToResize objectToResize)
        {
            ObjectToResize = objectToResize;
        }

        #region Variables
        /// <summary>
        /// The object will resize by this
        /// </summary>
        private T multiplySize;

        private TObjectToResize objectToResize;
        #endregion Variables

        #region Events
        /// <summary>
        /// Occurs when MultiplySize changed
        /// </summary>
        public event EventHandler<T> MultiplySizeChanged;
        /// <summary>
        /// Occurs when ObjectToResize changed
        /// </summary>
        public event EventHandler<TObjectToResize> ObjectToResizeChanged;
        /// <summary>
        /// Occurs when ObjectToResize before changed
        /// </summary>
        public event EventHandler<TObjectToResize> ObjectToResizeBeforeChanged;
        #endregion Events

        #region Raise events methods
        /// <summary>
        /// Raise the MultiplySizeChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMultiplyChanged(T e)
        {

            MultiplySizeChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raise the ObjectToResizeChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnObjectToResizeChanged(TObjectToResize e)
        {

            ObjectToResizeChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raise the ObjectToResizeBeforeChanged event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnObjectToResizeBeforeChanged(TObjectToResize e)
        {

            ObjectToResizeBeforeChanged?.Invoke(this, e);
        }
        #endregion Raise events methods

        #region Reset/Restart events
        protected virtual void ResetObjectToResizeEvents()
        {
            if (objectToResize != null)
            {

            }
        }

        protected virtual void RestartObjectToResizeEvents()
        {
            if (objectToResize != null)
            {

            }
        }
        #endregion Reset/Restart events

        #region Properties
        public T MultiplySize
        {
            get
            {
                return multiplySize;
            }

            set
            {
                multiplySize = value;
                OnMultiplyChanged(MultiplySize);
            }
        }

        public TObjectToResize ObjectToResize
        {
            get
            {
                return objectToResize;
            }

            set
            {
                OnObjectToResizeBeforeChanged(ObjectToResize);
                objectToResize = value;
                OnObjectToResizeChanged(ObjectToResize);
            }
        }
        #endregion Properties

        #region Methods
        public override void Active(TBoard board = default(TBoard))
        {
            if (board == null) board = Board;
            ResizeObject(ObjectToResize, MultiplySize);
            base.Active(board);
        }

        protected override void FinishEffect()
        {
            BackToFirstSizeObject(ObjectToResize, MultiplySize);
            base.FinishEffect();
        }

        /// <summary>
        /// Resize the size of object(the start method)
        /// </summary>
        /// <param name="objectToResize"></param>
        abstract protected void ResizeObject(TObjectToResize objectToResize, T multiplySize);
        /// <summary>
        /// Resize the object to his first size(When prize finish his effect)
        /// </summary>
        /// <param name="objectToResize"></param>
        abstract protected void BackToFirstSizeObject(TObjectToResize objectToResize, T multiplySize);
        #endregion Methods

        public override object Clone()
        {
            Resize<TBoard, T, TSize, TPoint, TObjectToResize> prize = (Resize<TBoard, T, TSize, TPoint, TObjectToResize>)base.Clone();
            prize.MultiplySize = MultiplySize;
            prize.ObjectToResize = ObjectToResize;
            return prize;
        }
    }
}
