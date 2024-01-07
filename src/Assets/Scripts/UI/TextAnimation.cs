using System;
using System.Collections.Generic;
using System.Linq;
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

    public IEnumerable<string> TextToWrite;

    public float FlickerTime;
    public int FlickerTimes;

    private int FlickerCounter;

    private float TimingCounter;
    private int i;
    private bool HasAnimationCompleted;
    private bool HasAnimationStarted;
    private int LinesToWrite;
    private int Line;
    private bool CanAnimate;

    public static event Action OnTextWritterAnimationCompleted;

    public static event Action OnTextFlickerAnimationCompleted;


    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        InteractionManager.SkipTextAnimation += InteractionManager_SkipTextAnimation;

        _text.text = string.Empty;
        TimingCounter = 0;
        i = 0;
        LinesToWrite = 0;
        Line = 0;
        CanAnimate = true;
        HasAnimationCompleted = false;
        HasAnimationStarted = false;
        FlickerCounter = FlickerTimes;
    }

    private void OnDisable()
    {
        InteractionManager.SkipTextAnimation -= InteractionManager_SkipTextAnimation;
    }

    private void Update()
    {
        TimingCounter += Time.deltaTime;

        if(AnimationType.Equals(AnimationTextType.Writter) && !HasAnimationCompleted && CanAnimate)
            WriteTextWithTiming(0.1f);


        if (AnimationType.Equals(AnimationTextType.Flicker) && !HasAnimationCompleted)
            FlickerAnimation(FlickerTime);        
    }

    private void FlickerAnimation( float time )
    {

        if (TimingCounter >= time)
        {
            if (_text.text.Equals(TextToWrite.First()))
            {
                _text.text = string.Empty;
            }
            else
            {
                _text.text = TextToWrite.First();
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

    private void InteractionManager_SkipTextAnimation()
    {
        LinesToWrite = TextToWrite.Count();

        if (VerifyTextHasCompletedWrite())
        {
            if (Line == LinesToWrite - 1)
                CompleteAnimation();
            else
            {
                _text.text = string.Empty;
                Line++;
                i = 0;
                CanAnimate = true;
            }
        }
        else
        {
            if (HasAnimationStarted && !HasAnimationCompleted)
            {
                _text.text = TextToWrite.ElementAt(Line);
            }
        }
    }

    

    private void WriteTextWithTiming(float time)
    {
        LinesToWrite = TextToWrite.Count();

        if (TimingCounter >= time)
        {
            if (!VerifyTextHasCompletedWrite())
            {
                HasAnimationStarted = true;
                _text.text += TextToWrite.ElementAt(Line)[i].ToString();
                i++;
                TimingCounter -= TimingCounter;
            }
            else
            {
                CanAnimate = false;
            }
        }
    }

    private void CompleteAnimation()
    {
        HasAnimationStarted = false;
        HasAnimationCompleted = true;
        CanAnimate = false;
        OnTextWritterAnimationCompleted();
    }

    private bool VerifyTextHasCompletedWrite()
    {
        return _text.text.Equals(TextToWrite.ElementAt(Line));
    }
}

