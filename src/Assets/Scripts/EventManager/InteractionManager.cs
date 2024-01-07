using Assets.Settings.InputSystem;
using System;
using System.Collections.Generic;
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
    private bool HasPromptInteractionDone;

    [SerializeField]
    private GameObject BrokenFrameObject;

    public static event Action SkipTextAnimation;
    public static event Action<IEnumerable<string>> OnInteractionItemCollected;

    private bool TextCompleted;

    //Panel Interaction
    public GameObject _panelText;
    private GameObject PanelTextInstantiated;
    private TextAnimation TextAnimationObject;
    private IEnumerable<string> TextToWrite;

    private void Awake()
    {
        _canvas = FindAnyObjectByType<Canvas>().gameObject;
        _touchManager = TouchManager.Instance;
        _joyStick = GameObject.FindWithTag(JOYSTICK_TAG);

        ActiveInteraction = false;
        TextCompleted = false;
        HasPromptInteractionDone = false;
    }

    private void OnEnable()
    {
        _touchManager.OnTouchPressed += TouchPressedValidation;
        InteractableObject.OnInteractableItemPressed += InteractableObject_OnInteractableItemPressed; ;
        TextAnimation.OnTextWritterAnimationCompleted += TextAnimation_OnTextWritterAnimationCompleted;
        TextAnimation.OnTextFlickerAnimationCompleted += TextAnimation_OnTextFlickerAnimationCompleted;
    }

    private void TextAnimation_OnTextFlickerAnimationCompleted()
    {
        if(IsFrameInteraction())
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

        if (TextCompleted && InteractionPromptInstantiated == null && !HasPromptInteractionDone)
        {
            DestroyPanelText();
            OpenInteractionPrompt();
        }

        if (HasInteractionTextCompleted())
        {
            DestroyPanelText();
            ActiveInteraction = false;
            TextCompleted = false;

            if(IsFrameInteraction())
            {
                GOSpriteRenderer.sprite = InteractableSO.InteractionSprites[0];

                ActiveBrokenFrameObject();

                OnInteractionItemCollected(VerifyItemCollected());
            }

            if(IsChestInteraction())
            {
                OnInteractionItemCollected(VerifyItemCollected());
            }
        }
    }

    private bool HasInteractionTextCompleted()
    {
        return TextCompleted && HasPromptInteractionDone;
    }

    private bool IsFrameInteraction()
    {
        return InteractionItems.Frame.Equals(InteractableSO.ItemName);
    }

    private bool IsChestInteraction()
    {
        return InteractionItems.Chest.Equals(InteractableSO.ItemName);
    }

    private void TextAnimation_OnTextWritterAnimationCompleted()
    {
        TextCompleted = true;
    }

    private IEnumerable<string> VerifyItemCollected()
    {
        switch (InteractableSO.CollectableItem)
        {
            case CollectableItem.Key:
                return new List<string> { "UMA CHAVE FOI COLETADA!" };
            case CollectableItem.Umbrella:
                return new List<string> { "UM GUARDA-CHUVAS FOI COLETADO!" }; 
            case CollectableItem.Password:
                return new List<string> { "UMA SENHA FOI COLETADA!" };
            default:
                return new List<string> { "UM ITEM FOI COLETADO!" };
        }

    }

    public void StartInteraction()
    {
        _interactableObject.SetInteractionComplete();
        HasPromptInteractionDone = true;
        TextCompleted = false;

        OpenPanelText(InteractableSO.InteractionMessage);
    }

    private void ActiveBrokenFrameObject()
    {
        BrokenFrameObject.SetActive(true);
    }

    private void OpenPanelText(IEnumerable<string> textToWrite)
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

