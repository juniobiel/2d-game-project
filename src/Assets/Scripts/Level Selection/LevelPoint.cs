using Assets.Settings.InputSystem;
using UnityEngine;

namespace Assets.Scripts.LevelSelection
{
    public class LevelPoint : MonoBehaviour
    {
        private TouchManager _touchManager;
        private Camera _mainCamera;

        private void Awake()
        {
            _touchManager = TouchManager.Instance;
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _touchManager.OnTouchPressed += Move;
        }

        private void OnDisable()
        {
            _touchManager.OnTouchEnded -= Move;
        }

        public void Move( Vector2 screenPosition )
        {
            Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, 0);
            Vector2 worldCoordinates = _mainCamera.ScreenToWorldPoint(screenCoordinates);


            Ray ray = _mainCamera.ScreenPointToRay(screenCoordinates);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Orb")
                {
                    Destroy(hit.transform.gameObject);
                }
            }


            //Debug.Log($"Posicao do objeto no transform Antes:{transform.position}, {transform.localPosition}, {transform.localToWorldMatrix}");
            //transform.position = worldCoordinates;
            //Debug.Log($"Posicao do objeto no transform Depois:{transform.position}, {worldCoordinates}");

        }
    }
}