using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    public interface IRefCount
    {
        #region Properties
        /// <summary>
        /// Max of ref that can to be from this class,
        /// used by user, if you cross this numbet nothing will happen
        /// </summary>
        int MaxOfRefCan { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Get how much ref from this class have
        /// </summary>
        int GetNumberdOfRefernce();

        /// <summary>
        /// Remove 1 ref from dictionery
        /// </summary>
        void DestroyRef();
        #endregion

        #region Events
        event EventHandler<Type> RefAdded;
        /// <summary>
        /// Occurs when reference removed
        /// </summary>
        event EventHandler<Type> RefDestroyed;
        /// <summary>
        /// Occurs when max of refernce can to create changed
        /// </summary>
        event EventHandler<int> MaxOfRefCanChanged;
        #endregion
    }
}
