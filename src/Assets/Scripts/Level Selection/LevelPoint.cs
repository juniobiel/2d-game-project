using Assets.Settings.InputSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.LevelSelection
{
    public class LevelPoint : MonoBehaviour
    {
        public static event Action<GameObject> OnLevelPointSelected;
        
        private TouchManager _touchManager;
        private Camera _mainCamera;

        public ScriptableObject LevelInformation;

        private void Awake()
        {
            _touchManager = TouchManager.Instance;
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _touchManager.OnTouchPressed += TouchPressedLevelPoint;
        }

        private void OnDisable()
        {
            _touchManager.OnTouchReleased -= TouchReleasedLevelPoint;
        }

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
                if (hit.transform.tag == "LevelPoint")
                {
                    OnLevelPointSelected(hit.transform.gameObject);
                }
            }
        }

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