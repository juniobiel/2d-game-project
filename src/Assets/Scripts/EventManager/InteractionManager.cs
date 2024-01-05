using Assets.Settings.InputSystem;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


public class InteractionManager : EventManagerBase
{
    private bool ActiveInteraction;
    private GameObject InteractableGameObject;
    private InteractableObject _interactableObject;
    private InteractionObjectSO InteractableSO;
    private SpriteRenderer GOSpriteRenderer;

    [SerializeField]
    private GameObject InteractionPrompt;
    public static GameObject InteractionPromptInstantiated;
    private bool HasInteractionDone;

    [SerializeField]
    private GameObject BrokenFrameObject;

    public static event Action SkipTextAnimation;
    public static event Action<string> OnInteractionItemCollected;

    private bool TextCompleted;

    //Panel Interaction
    public GameObject _panelText;
    private GameObject PanelTextInstantiated;
    private TextAnimation TextAnimationObject;
    private string TextToWrite;

    private void Awake()
    {
        _canvas = FindAnyObjectByType<Canvas>().gameObject;
        _touchManager = TouchManager.Instance;
        _joyStick = GameObject.FindWithTag(JOYSTICK_TAG);

        ActiveInteraction = false;
        TextCompleted = false;
        HasInteractionDone = false;
    }

    private void OnEnable()
    {
        _touchManager.OnTouchPressed += TouchPressedValidation;
        InteractableObject.OnInteractableItemPressed += InteractableObject_OnInteractableItemPressed; ;
        TextAnimation.OnTextWritterAnimationCompleted += TextAnimation_OnTextWritterAnimationCompleted;
        TextAnimation.OnTextFlickerAnimationCompleted += TextAnimation_OnTextFlickerAnimationCompleted;
    }

    private void TextAnimation_OnTextWritterAnimationCompleted()
    {
        TextCompleted = true;
    }

    private void TextAnimation_OnTextFlickerAnimationCompleted()
    {
        Destroy(InteractableGameObject);
        ActiveInteraction = false;
        _joyStick.SetActive(true);
    }

    private void OnDisable()
    {
        _touchManager.OnTouchPressed -= TouchPressedValidation;
        InteractableObject.OnInteractableItemPressed -= InteractableObject_OnInteractableItemPressed; ;
        TextAnimation.OnTextWritterAnimationCompleted -= TextAnimation_OnTextWritterAnimationCompleted;
    }

    private void InteractableObject_OnInteractableItemPressed( GameObject interactableObject )
    {
        if (ActiveInteraction)
            return;

        if (!ActiveInteraction)
            ActiveInteraction = true;

        InteractableGameObject = interactableObject;
        InteractableSO = InteractableGameObject.GetComponent<InteractableObject>().InteractableObjectSO;
        GOSpriteRenderer = interactableObject.GetComponent<SpriteRenderer>();
        _interactableObject = interactableObject.GetComponent<InteractableObject>();

        OpenPanelText(InteractableSO.InitialPhrase);
    }

    private void TouchPressedValidation( CallbackContext context )
    {
        if (!TextCompleted && PanelTextInstantiated)
            SkipTextAnimation();

        if (TextCompleted && InteractionPromptInstantiated == null && !HasInteractionDone)
        {
            DestroyPanelText();
            OpenInteractionPrompt();
        }

        if (TextCompleted && HasInteractionDone)
        {
            DestroyPanelText();
            ActiveInteraction = false;
            TextCompleted = false;

            GOSpriteRenderer.sprite = InteractableSO.InteractionSprites[0];

            ActiveBrokenFrameObject();

            OnInteractionItemCollected(VerifyItemCollected());           
        }
    }

    private string VerifyItemCollected()
    {
        switch (InteractableSO.CollectableItem)
        {
            case CollectableItem.Key:
                return "UMA CHAVE FOI COLETADA!";
            case CollectableItem.Umbrella:
                return "UM GUARDA-CHUVAS FOI COLETADO!";
            case CollectableItem.Password:
                return "UMA SENHA FOI COLETADA!";
            default:
                return "UM ITEM FOI COLETADO!";
        }

    }

    public void StartInteraction()
    {
        _interactableObject.SetInteractionComplete();
        HasInteractionDone = true;

        OpenPanelText(InteractableSO.InteractionMessage);
    }

    private void ActiveBrokenFrameObject()
    {
        BrokenFrameObject.SetActive(true);
    }

    private void OpenPanelText(string textToWrite)
    {
        _joyStick.SetActive( false );
        TextToWrite = textToWrite;
        InstantiatePanelText();
    }

    private void InstantiatePanelText()
    {
        PanelTextInstantiated = Instantiate(_panelText, _canvas.transform);
        TextAnimationObject = PanelTextInstantiated.GetComponentInChildren<TextAnimation>(true);
        TextAnimationObject.TextToWrite = TextToWrite;
    }

    private void DestroyPanelText()
    {
        Destroy(PanelTextInstantiated);
        PanelTextInstantiated = null;
    }
    
    private void OpenInteractionPrompt()
    {
        InteractionPromptInstantiated = Instantiate(InteractionPrompt, _canvas.transform);
    }

    public void BtnResetInteraction()
    {
        ActiveInteraction = false;
        TextCompleted = false;
        _joyStick.SetActive(true );
    }


}

