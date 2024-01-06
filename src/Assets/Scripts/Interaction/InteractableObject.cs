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

    
    public InteractionObjectSO InteractableObjectSO;

    private bool IsActive = false;
    private bool HasInteractionCompleted;

    [SerializeField]
    private float InteractionDistanceActivation;

    public static event Action<GameObject> OnInteractableItemPressed;


    private void Awake()
    {
        _touchManager = TouchManager.Instance;
        _mainCamera = Camera.main;
        _playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        HasInteractionCompleted = false;
    }

    private void OnEnable()
    {
        _touchManager.OnTouchPositionPressed += TouchPressedInteractableObject;
    }

    private void OnDisable()
    {
        _touchManager.OnTouchPositionPressed -= TouchPressedInteractableObject;
    }

    private void FixedUpdate()
    {
        if(!HasInteractionCompleted)
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
        InteractionIconInstance.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
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
            if (VerifyTouchObject(ref hit))
            {
                OnInteractableItemPressed(hit.transform.gameObject);
                DestroyInteractionIcon();
            }
        }
    }

    private bool VerifyTouchObject( ref RaycastHit hit )
    {
        return hit.transform.CompareTag(INTERACTABLE_TAG) 
            && IsActive 
            && !HasInteractionCompleted
            && hit.transform.gameObject.name.Equals(gameObject.name);
    }

    public void SetInteractionComplete()
    {
        HasInteractionCompleted = true;
    }
}
