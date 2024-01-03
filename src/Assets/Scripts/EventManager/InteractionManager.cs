using Assets.Settings.InputSystem;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


public class InteractionManager : EventManagerBase
{
    private bool ActiveInteraction;
    private GameObject InteractableGameObject;

    [SerializeField]
    private GameObject InteractionPrompt;
    public static GameObject InteractionPromptInstantiated;

    public static event Action SkipTextAnimation;

    private bool TextCompleted;

    //Panel Interaction
    public GameObject _panelText;
    private GameObject PanelTextInstantiated;
    private TextAnimation TextAnimationObject;

    private void Awake()
    {
        _canvas = FindAnyObjectByType<Canvas>().gameObject;
        _touchManager = TouchManager.Instance;

        ActiveInteraction = false;
        TextCompleted = false;
    }

    private void OnEnable()
    {
        _touchManager.OnTouchPressed += TouchPressedValidation;
        InteractableObject.OnInteractableItemPressed += InteractableObject_OnInteractableItemPressed; ;
        TextAnimation.OnTextAnimationCompleted += TextAnimationCompleted;
    }

    private void InteractableObject_OnInteractableItemPressed( GameObject interactableObject )
    {
        if (ActiveInteraction)
            return;

        if (!ActiveInteraction)
            ActiveInteraction = true;

        InteractableGameObject = interactableObject;
        OpenPanelText();
    }

    private void OpenPanelText()
    {
        InstantiatePanelText();
    }

    private void InstantiatePanelText()
    {
        PanelTextInstantiated = Instantiate(_panelText, _canvas.transform);
        TextAnimationObject = PanelTextInstantiated.GetComponentInChildren<TextAnimation>(true);
        TextAnimationObject.TextToWrite = InteractableGameObject.GetComponent<InteractableObject>().InteractableObjectSO.InitialPhrase;
    }

    private void TextAnimationCompleted()
    {
        TextCompleted = true;
    }

    private void DestroyPanelText()
    {
        Destroy(PanelTextInstantiated);
        PanelTextInstantiated = null;
    }


    private void TouchPressedValidation( CallbackContext context )
    {
        if (!TextCompleted && PanelTextInstantiated)
            SkipTextAnimation();

        if (TextCompleted && InteractionPromptInstantiated == null)
        {
            DestroyPanelText();
            OpenInteractionPrompt();
        }
    }

    private void OpenInteractionPrompt()
    {
        InteractionPromptInstantiated = Instantiate(InteractionPrompt, _canvas.transform);
    }

    public void SetActiveInteraction( bool active )
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

