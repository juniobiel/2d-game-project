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

        public delegate void TouchPressedPositionEvent( Vector2 position );
        public event TouchPressedPositionEvent OnTouchPositionPressed;

        public delegate void TouchPressedEvent();
        public event TouchPressedEvent OnTouchPressed;


        public delegate void TouchPositionReleasedEvent( Vector2 position );
        public event TouchPositionReleasedEvent OnTouchReleased;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _touchPressAction = _playerInput.actions["TouchPress"];
            _touchPositionAction = _playerInput.actions["TouchPosition"];
        }

        private void OnEnable()
        {
            _touchPositionAction.performed += TouchPressedPosition;
            _touchPressAction.performed += TouchPressed;
        }

        private void OnDisable()
        {
            _touchPositionAction.canceled -= TouchPositionReleased;
        }

        private void TouchPressed( InputAction.CallbackContext context )
        {
            OnTouchPressed?.Invoke();
        }

        private void TouchPressedPosition( InputAction.CallbackContext context )
        {
            OnTouchPositionPressed?.Invoke(context.ReadValue<Vector2>());
        }

        private void TouchPositionReleased(InputAction.CallbackContext context )
        {
            OnTouchReleased?.Invoke(context.ReadValue<Vector2>());
        }
    }
}