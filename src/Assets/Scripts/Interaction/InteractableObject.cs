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
    private Sprite NormalSprite;
    [SerializeField]
    private Sprite OutlineSprite;
    private SpriteRenderer _spriteRenderer;

    private bool IsActive = false;
    [SerializeField]
    private float InteractionDistanceActivation;

    public static event Action<GameObject> OnInteractableItemPressed;

    private void Awake()
    {
        _touchManager = TouchManager.Instance;
        _mainCamera = Camera.main;
        _playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

            _spriteRenderer.sprite = OutlineSprite;
        }
        else if(!DistanceVerify(InteractionDistanceActivation))
        {
            IsActive = false;

            _spriteRenderer.sprite = NormalSprite;
        }
        
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
