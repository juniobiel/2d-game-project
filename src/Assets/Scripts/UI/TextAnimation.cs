using System;
using TMPro;
using UnityEngine;


public enum AnimationTextType
{
    Writter,
    Flicker
}

public class TextAnimation : MonoBehaviour
{
    private TMP_Text _text;

    public AnimationTextType AnimationType;

    public string TextToWrite;

    public float FlickerTime;
    public int FlickerTimes;

    private int FlickerCounter;

    private float TimingCounter;
    private int i;
    private bool HasAnimationCompleted;
    private bool HasAnimationStarted;

    public static event Action OnTextWritterAnimationCompleted;

    public static event Action OnTextFlickerAnimationCompleted;


    private void Awake()
    {
        InteractionManager.SkipTextAnimation += InteractionManager_SkipTextAnimation;

        _text = GetComponent<TMP_Text>();
        _text.text = string.Empty;
        TimingCounter = 0;
        i = 0;
        HasAnimationCompleted = false;
        HasAnimationStarted = false;
        FlickerCounter = FlickerTimes;
    }

    private void InteractionManager_SkipTextAnimation()
    {
        if (HasAnimationStarted && !HasAnimationCompleted && AnimationTextType.Writter.Equals(AnimationType))
            _text.text = TextToWrite;
        
    }

    private void Update()
    {
        TimingCounter += Time.deltaTime;

        if(AnimationType.Equals(AnimationTextType.Writter) && !HasAnimationCompleted)
            WriteTextWithTiming(0.1f);


        if (AnimationType.Equals(AnimationTextType.Flicker) && !HasAnimationCompleted)
            FlickerAnimation(FlickerTime, FlickerTimes);        
    }

    private void FlickerAnimation(float time, int flickerTimes)
    {
        
        if( TimingCounter >= time)
        {
            if(_text.text.Equals(TextToWrite))
            {
                _text.text = string.Empty;
            }
            else
            {
                _text.text = TextToWrite;
                FlickerCounter--;
            }

            if (FlickerCounter == -1)
            {
                HasAnimationCompleted = true;
                _text.text = string.Empty;
                OnTextFlickerAnimationCompleted();
            }

            TimingCounter -= TimingCounter;
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
        OnTextWritterAnimationCompleted();
    }

    private bool VerifyTextHasCompletedWrite()
    {
        return _text.text.Equals(TextToWrite);
    }
}

