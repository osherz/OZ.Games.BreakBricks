using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface IVisible
    {
        #region Properties
        /// <summary>
        /// Get/Set if the item will visible
        /// </summary>
        bool Visible { get; set; }
        #endregion Properties

        #region Events
        /// <summary>
        /// Occurs when Visible changed
        /// </summary>
        event EventHandler<bool> VisibleChanged;
        #endregion Events

        #region Methods
        /// <summary>
        /// Change visible to true
        /// </summary>
        void Show();

        /// <summary>
        /// Change visible to false
        /// </summary>
        void Hide();
        #endregion Methods
    }
}
