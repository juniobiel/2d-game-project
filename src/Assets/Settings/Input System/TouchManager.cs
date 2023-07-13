using Assets.Scripts.Helpers;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Settings.InputSystem
{
    [DefaultExecutionOrder(-1)]
    public class TouchManager : Singleton<TouchManager>
    {
        private PlayerInput _playerInput;

        private InputAction _touchPositionAction;
        private InputAction _touchPressAction;

        public delegate void TouchPressedEvent( Vector2 position );
        public event TouchPressedEvent OnTouchPressed;

        public delegate void TouchEndedEvent( Vector2 position );
        public event TouchEndedEvent OnTouchEnded;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _touchPressAction = _playerInput.actions["TouchPress"];
            _touchPositionAction = _playerInput.actions["TouchPosition"];
        }

        private void OnEnable()
        {
            _touchPositionAction.performed += TouchPressed;
        }

        private void OnDisable()
        {
            _touchPositionAction.canceled -= TouchPressed;
        }

        private void TouchPressed( InputAction.CallbackContext context )
        {
            if ( OnTouchPressed != null)
            {
                OnTouchPressed(context.ReadValue<Vector2>());
            }
        }

        private void TouchEnded( InputAction.CallbackContext context )
        {
            if (OnTouchEnded != null)
            {
                OnTouchEnded(context.ReadValue<Vector2>());
            }
        }
    }
}