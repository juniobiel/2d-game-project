using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    private InteractionManager _interactionManager;

    private void Awake()
    {
        _interactionManager = FindAnyObjectByType<InteractionManager>();
    }
    public void OnClickYesButton()
    {
        Destroy(InteractionManager.InteractionPromptInstantiated);
        _interactionManager.StartInteraction();
    }

    public void OnClickNoButton()
    {
        Destroy(InteractionManager.InteractionPromptInstantiated);
        _interactionManager.SetActiveInteraction(false);
    }
}
