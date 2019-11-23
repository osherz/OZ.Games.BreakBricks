using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface ITimer
    {
        #region Properties
        /// <summary>
        /// Gets or sets the time, in milliseconds, before the Tick event is raised relative 
        /// to the last occurrence of the Tick event.
        /// </summary>
        int TimerInterval { get; set; }
        #endregion Properties

        #region Events
        /// <summary>
        /// Occurs when tick
        /// </summary>
        event EventHandler Tick;
        /// <summary>
        /// Occurs when interval changed
        /// </summary>
        event EventHandler<int> TimerIntervalChanged;
        #endregion Events

        #region Raises events methods
        /// <summary>
        /// Raises the Tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_OnTick(object sender, EventArgs e);
        #endregion Raises events methods

        #region Methods
        /// <summary>
        /// Starts the timer.
        /// </summary>
        void StartTimer();
        /// <summary>
        /// Stops the timer.
        /// </summary>
        void StopTimer();
        #endregion Methods
    }
}
