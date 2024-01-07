using Assets.Scripts.Level_Selection;
using Assets.Settings.InputSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.LevelSelection
{
    public class LevelPoint : MonoBehaviour
    {
        /// <summary>
        /// this creating the Event when the point is selected
        /// </summary>
        public static event Action<GameObject> OnLevelPointSelected;
        public static event Action<bool> ActiveButtonPlay;
        
        private TouchManager _touchManager;
        private Camera _mainCamera;

        public LevelScriptableObject LevelInformation;

        private void Awake()
        {
            _touchManager = TouchManager.Instance;
            _mainCamera = Camera.main;
        }
        /// <summary>
        /// This is subscribing the touch pressed event
        /// </summary>
        private void OnEnable()
        {
            _touchManager.OnTouchPositionPressed += TouchPressedLevelPoint;
        }

        /// <summary>
        /// This is unsubscribing the touch released event
        /// </summary>
        private void OnDisable()
        {
            _touchManager.OnTouchReleased -= TouchReleasedLevelPoint;
        }

        /// <summary>
        /// Used to raise action when the touch was pressed
        /// </summary>
        /// <param name="screenPosition">Position of touch in the screen</param>
        private void TouchPressedLevelPoint( Vector2 screenPosition )
        {
            Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, _mainCamera.nearClipPlane);
            Vector2 worldCoordinates = _mainCamera.ScreenToWorldPoint(screenCoordinates);

            Ray ray = _mainCamera.ScreenPointToRay(screenCoordinates);
            RaycastHit hit;
            
            //Use this to debug touch tap
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag.Equals("LevelPoint"))
                {
                    OnLevelPointSelected(hit.transform.gameObject);

                    ActiveButtonPlay(true);
                }
            }
        }

        /// <summary>
        /// Used to raise action when the touch was realeased;
        /// </summary>
        /// <param name="screenPosition">Position of touch in the screen</param>
        private void TouchReleasedLevelPoint(Vector2 screenPosition)
        {
            Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, _mainCamera.nearClipPlane);
            Vector2 worldCoordinates = _mainCamera.ScreenToWorldPoint(screenCoordinates);

            Ray ray = _mainCamera.ScreenPointToRay(screenCoordinates);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "LevelPoint")
                {
                    Debug.Log("Can create a new interaction when the player release the touch point");
                }
            }
        }
    }
}