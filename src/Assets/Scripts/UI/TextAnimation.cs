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

    public static event Action OnTextAnimationCompleted;


    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = string.Empty;
        TimingCounter = 0;
        i = 0;
        HasAnimationCompleted = false;
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
                _text.text += TextToWrite[i].ToString();
                i++;
                TimingCounter -= TimingCounter;
            }
            else
            {
                HasAnimationCompleted = true;
                OnTextAnimationCompleted();
            }
        }
    }

    private bool VerifyTextHasCompletedWrite()
    {
        return _text.text.Equals(TextToWrite);
    }
}

