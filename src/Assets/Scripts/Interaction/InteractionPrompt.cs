using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    private InteractablesEventManager _eventManager;
    

    private void Awake()
    {
        _eventManager = InteractablesEventManager.Instance;
    }
    public void OnClickYesButton()
    {
        Destroy(InteractablesEventManager.InteractableInstantiated);
        _eventManager.StartInteraction();
    }

    public void OnClickNoButton()
    {
        Destroy(InteractablesEventManager.InteractableInstantiated);
        _eventManager.SetActiveInteraction(false);
    }
}
