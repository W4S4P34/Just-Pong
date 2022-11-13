using UnityEngine;
using UnityEngine.Events;

namespace Ultilities
{
    public class GameEventListener : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField]
        protected GameEvent _event;
        [SerializeField]
        protected UnityEvent responses;

        #endregion


        #region MonoBehaviour Callbacks

        protected void OnEnable() => _event.RegisterListener(this);

        protected void OnDisable() => _event.UnregisterListener(this);

        #endregion


        #region Public Callbacks

        public virtual void Raise() => responses?.Invoke();

        #endregion
    }
}