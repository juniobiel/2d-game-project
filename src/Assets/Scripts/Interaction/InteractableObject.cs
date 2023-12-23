using Assets.Settings.InputSystem;
using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private const string INTERACTABLE_TAG = "InteractableObject";

    private TouchManager _touchManager;
    private Camera _mainCamera;
    
    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private GameObject InteractionIcon;

    private GameObject InteractionIconInstance;

    private bool IsActive = false;

    [SerializeField]
    private float InteractionDistanceActivation;

    public static event Action<GameObject> OnInteractableItemPressed;

    private void Awake()
    {
        _touchManager = TouchManager.Instance;
        _mainCamera = Camera.main;
        _playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().gameObject.transform;
    }

    private void OnEnable()
    {
        _touchManager.OnTouchPressed += TouchPressedInteractableObject;
    }

    private void FixedUpdate()
    {
        if (DistanceVerify(InteractionDistanceActivation) && !IsActive)
        {
            IsActive = true;
            InstantiateInteractionIcon();
        }
        else if(!DistanceVerify(InteractionDistanceActivation))
        {
            IsActive = false;
            DestroyInteractionIcon();
        }

    }

    private void DestroyInteractionIcon()
    {
        Destroy(InteractionIconInstance);
        InteractionIconInstance = null;
    }

    private void InstantiateInteractionIcon()
    {
        InteractionIconInstance = Instantiate(InteractionIcon, gameObject.transform);
        InteractionIconInstance.transform.localScale = Vector3.one;
        InteractionIconInstance.transform.position = Vector3.zero;
        InteractionIconInstance.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private bool DistanceVerify(float distance) => Vector2.Distance(_playerTransform.position, gameObject.transform.position) <= distance;
    

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
            if (hit.transform.CompareTag(INTERACTABLE_TAG) && IsActive)
            {
                Debug.Log("Tocou em um objeto interativo");
                OnInteractableItemPressed(hit.transform.gameObject);
            }
        }
    }
}
