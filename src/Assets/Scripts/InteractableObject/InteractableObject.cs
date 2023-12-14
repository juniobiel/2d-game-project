using Assets.Settings.InputSystem;
using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private TouchManager _touchManager;
    private Camera _mainCamera;

    public static event Action<GameObject> OnInteractableItemPressed;

    private void Awake()
    {
        _touchManager = TouchManager.Instance;
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _touchManager.OnTouchPressed += TouchPressedInteractableObject;
    }

    private void TouchPressedInteractableObject( Vector2 screenPosition )
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, _mainCamera.nearClipPlane);
        Vector2 worldCoordinates = _mainCamera.ScreenToWorldPoint(screenCoordinates);

        Ray ray = _mainCamera.ScreenPointToRay(screenCoordinates);
        RaycastHit hit;

        //Use this to debug touch tap
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag.Equals("InteractableObject"))
            {
                Debug.Log("Tocou em um objeto interativo");
                OnInteractableItemPressed(hit.transform.gameObject);
            }
        }
    }
}
