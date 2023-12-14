using UnityEngine;

public class EventManagerInteractableObjects : MonoBehaviour
{
    private bool ActiveInteraction;
    private GameObject InteractableGameObject;

    private void Awake()
    {
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
    }
}
