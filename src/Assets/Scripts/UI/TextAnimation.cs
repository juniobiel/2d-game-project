using System;
using TMPro;
using UnityEngine;


public enum AnimationTextType
{
    Writter
}

public class TextAnimation : MonoBehaviour
{
    private TMP_Text _text;

    public AnimationTextType AnimationType;

    public string TextToWrite;

    private float TimingCounter;
    private int i;
    private bool HasAnimationCompleted;
    private bool HasAnimationStarted;
    public static event Action OnTextAnimationCompleted;


    private void Awake()
    {
        InteractionManager.SkipTextAnimation += InteractionManager_SkipTextAnimation;

        _text = GetComponent<TMP_Text>();
        _text.text = string.Empty;
        TimingCounter = 0;
        i = 0;
        HasAnimationCompleted = false;
        HasAnimationStarted = false;
    }

    private void InteractionManager_SkipTextAnimation()
    {
        if (HasAnimationStarted && !HasAnimationCompleted)
        {
            _text.text = TextToWrite;
        }
    }

    private void Update()
    {
        if(AnimationType.Equals(AnimationTextType.Writter) && !HasAnimationCompleted)
        {
            TimingCounter += Time.deltaTime;
            WriteTextWithTiming(0.1f);
        }
    }

    private void WriteTextWithTiming(float time)
    {
        if (TimingCounter >= time)
        {
            if (!VerifyTextHasCompletedWrite())
            {
                HasAnimationStarted = true;
                _text.text += TextToWrite[i].ToString();
                i++;
                TimingCounter -= TimingCounter;
            }
            else
                CompleteAnimation();
            
        }
    }

    private void CompleteAnimation()
    {
        HasAnimationStarted = false;
        HasAnimationCompleted = true;
        OnTextAnimationCompleted();
    }

    private bool VerifyTextHasCompletedWrite()
    {
        return _text.text.Equals(TextToWrite);
    }
}

