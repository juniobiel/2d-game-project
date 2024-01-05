using Assets.Settings.InputSystem;
using UnityEngine;

public class UIManager : EventManagerBase
{
    [SerializeField]
    private GameObject HUDCollectedItem;
    private GameObject HUDInstantiated;

    private TextAnimation _textAnimationScript;
    

    private void Awake()
    {
        _canvas = FindAnyObjectByType<Canvas>().gameObject;
        _touchManager = TouchManager.Instance;
        _joyStick = GameObject.FindWithTag(JOYSTICK_TAG);
    }

    private void OnEnable()
    {
        InteractionManager.OnInteractionItemCollected += InteractionManager_OnInteractionItemCollected;
        TextAnimation.OnTextFlickerAnimationCompleted += TextAnimation_OnTextFlickerAnimationCompleted;
    }

    private void OnDisable()
    {
        InteractionManager.OnInteractionItemCollected -= InteractionManager_OnInteractionItemCollected;
        TextAnimation.OnTextFlickerAnimationCompleted -= TextAnimation_OnTextFlickerAnimationCompleted;
    }

    private void TextAnimation_OnTextFlickerAnimationCompleted()
    {
        Destroy(HUDInstantiated);
        HUDInstantiated = null;
    }

    private void InteractionManager_OnInteractionItemCollected( string text )
    {
        HUDInstantiated = Instantiate( HUDCollectedItem, _canvas.transform);
        _textAnimationScript = HUDInstantiated.GetComponent<TextAnimation>();
        _textAnimationScript.TextToWrite = text;
    }
}
