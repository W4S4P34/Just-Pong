using System.Collections.Generic;
using UnityEngine;

namespace Ultilities
{
    [CreateAssetMenu(
        fileName = "GameEvent", menuName = "Scriptable Objects/Event", order = 1
    )]
    public class GameEvent : ScriptableObject
    {
        #region Private Fields

        private HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

        #endregion


        #region Public Callbacks

        public virtual void RegisterListener(GameEventListener listener) => listeners.Add(listener);

        public virtual void UnregisterListener(GameEventListener listener) => listeners.Remove(listener);

        public void RaiseEvent()
        {
            foreach (var listener in listeners)
            {
                listener.Raise();
            }
        }

        #endregion
    }
}