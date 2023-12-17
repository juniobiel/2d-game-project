using UnityEngine;

public class EventManagerInteractableObjects : MonoBehaviour
{
    private bool ActiveInteraction;
    private GameObject InteractableGameObject;

    [SerializeField]
    private GameObject InteractionPrompt;

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
        Instantiate(InteractionPrompt, _canvas.transform);
    }
}
