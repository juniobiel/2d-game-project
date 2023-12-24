using UnityEngine;
using UnityEngine.UI;

public class PanelFadeAnimation : MonoBehaviour
{
    private bool HasPanelFaded;
    private float TimingCounter;

    private Image _panelSrcImage;

    private void Awake()
    {
        HasPanelFaded = false;
        _panelSrcImage = GetComponent<Image>();
        _panelSrcImage.fillAmount = 0;
        TimingCounter = 0;
    }

    void Update()
    {
        if (!HasPanelFaded)
            AnimatePanelFadeIn();
        
    }

    private void AnimatePanelFadeIn()
    {
        TimingCounter += Time.deltaTime;

        if(TimingCounter >= 0.025f && !HasPanelFaded)
        {
            _panelSrcImage.fillAmount += 0.05f;

            if (_panelSrcImage.fillAmount == 1)
                HasPanelFaded = true;

            TimingCounter -= TimingCounter;
        }
    }
}
