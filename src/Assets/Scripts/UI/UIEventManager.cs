using Assets.Scripts.Helpers;
using Assets.Settings.InputSystem;
using UnityEngine;

public class UIEventManager : Singleton<UIEventManager>
{
    private Canvas _canvas;
    private TouchManager _touchManager;

    //Panel Interaction
    public GameObject _panelText;
    private GameObject PanelTextInstantiated;
    private GameObject InteractableObject;
    private TextAnimation TextAnimationObject;


    private void Awake()
    {
        _canvas = FindAnyObjectByType<Canvas>();

        InteractablesEventManager.OnInteractionUITextOpened += OpenPanelText;
        InteractablesEventManager.OnTextPanelCompleted += DestroyPanelText;
    }

    private void OpenPanelText( GameObject interactableInfo )
    {
        InteractableObject = interactableInfo;
        InstantiatePanelText();
    }

    private void InstantiatePanelText()
    {
        PanelTextInstantiated = Instantiate(_panelText, _canvas.gameObject.transform);
        TextAnimationObject = PanelTextInstantiated.GetComponentInChildren<TextAnimation>(true);
        TextAnimationObject.TextToWrite = InteractableObject.GetComponent<InteractableObject>().InteractableObjectSO.InitialPhrase;
    }

    private void DestroyPanelText()
    {
        Destroy(PanelTextInstantiated);
        PanelTextInstantiated = null;
    }
}
