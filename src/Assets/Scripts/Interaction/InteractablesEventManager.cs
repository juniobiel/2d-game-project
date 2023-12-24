using Assets.Scripts.Helpers;
using Assets.Settings.InputSystem;
using System;
using UnityEngine;

public class InteractablesEventManager : Singleton<InteractablesEventManager>
{
    private TouchManager _touchManager;
    private bool ActiveInteraction;
    private GameObject InteractableGameObject;

    [SerializeField]
    private GameObject InteractionPrompt;
    public static GameObject InteractableInstantiated;

    private GameObject _canvas;

    public static event Action<GameObject> OnInteractionUITextOpened;
    public static event Action OnTextPanelCompleted;

    private bool TextCompleted;

    private void Awake()
    {
        _canvas = GameObject.FindAnyObjectByType<Canvas>().gameObject;
        ActiveInteraction = false;
        TextCompleted = false;
        _touchManager = TouchManager.Instance;
        _touchManager.OnTouchPressed += TouchPressedValidation;
    }

    private void OnEnable()
    {
        InteractableObject.OnInteractableItemPressed += SetObjectInteraction;
        TextAnimation.OnTextAnimationCompleted += TextAnimationCompleted;
    }

    private void SetObjectInteraction(GameObject interactableObject)
    {        
        if (ActiveInteraction)
            return;

        if(!ActiveInteraction)
            ActiveInteraction = true;

        InteractableGameObject = interactableObject;
        Debug.Log($"Objeto selecionado foi: {InteractableGameObject.name}");
        OnInteractionUITextOpened( interactableObject );
    }


    private void TextAnimationCompleted()
    {
        TextCompleted = true;
    }

    private void TouchPressedValidation()
    {
        if (TextCompleted && InteractableInstantiated == null)
        {
            OnTextPanelCompleted();
            OpenInteractionPrompt();
        }
    }
    private void OpenInteractionPrompt()
    {
        InteractableInstantiated = Instantiate(InteractionPrompt, _canvas.transform);
    }

    public void SetActiveInteraction(bool active)
    {
        ActiveInteraction = active;
        TextCompleted = false;
    }

    public void StartInteraction()
    {
        //Ativar a interface
        //Passar o texto parametrizado (Scriptable Object)


        //Ativar o objeto coletável
        //Chave Coletada.
        ActiveInteraction = false;
        TextCompleted = false;
    }
    
}
