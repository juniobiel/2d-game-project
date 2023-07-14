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

        public delegate void TouchReleasedEvent( Vector2 position );
        public event TouchReleasedEvent OnTouchReleased;

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
            _touchPositionAction.canceled -= TouchReleased;
        }

        private void TouchPressed( InputAction.CallbackContext context )
        {
            if ( OnTouchPressed != null)
            {
                OnTouchPressed(context.ReadValue<Vector2>());
            }
        }

        private void TouchReleased(InputAction.CallbackContext context )
        {
            if (OnTouchReleased != null)
            {
                OnTouchReleased(context.ReadValue<Vector2>());
            }
        }
    }
}