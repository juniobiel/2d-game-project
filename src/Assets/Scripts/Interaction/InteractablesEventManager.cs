using Assets.Scripts.Helpers;
using UnityEngine;

public class InteractablesEventManager : Singleton<InteractablesEventManager>
{
    private bool ActiveInteraction;
    private GameObject InteractableGameObject;

    [SerializeField]
    private GameObject InteractionPrompt;
    public static GameObject InteractableInstantiated;

    [SerializeField]
    private GameObject _canvas;

    private void Awake()
    {
        _canvas = GameObject.FindAnyObjectByType<Canvas>().gameObject;
        ActiveInteraction = false;
    }

    private void OnEnable()
    {
        InteractableObject.OnInteractableItemPressed += SetObjectInteraction;
    }

    private void SetObjectInteraction(GameObject interactableObject)
    {        
        if (ActiveInteraction)
            return;

        if(!ActiveInteraction)
            ActiveInteraction = true;

        InteractableGameObject = interactableObject;
        Debug.Log($"Objeto selecionado foi: {InteractableGameObject.name}");
        OpenInteractionPrompt();
    }

    private void OpenInteractionPrompt()
    {
        InteractableInstantiated = Instantiate(InteractionPrompt, _canvas.transform);
    }

    public void SetActiveInteraction(bool active)
    {
        ActiveInteraction = active;
    }

    public void StartInteraction()
    {
        //Ativar a interface
        //Passar o texto parametrizado (Scriptable Object)


        //Ativar o objeto coletável
        //Chave Coletada.
        ActiveInteraction = false;
    }
    
}
