using Assets.Scripts.LevelSelection;
using UnityEngine;

public class ButtonPlay : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        LevelPoint.ActiveButtonPlay += MakeButtonPlayActivated;
    }

    private void MakeButtonPlayActivated(bool isActive)
    {
        if (isActive)
        {
            _animator.enabled = true;
        }
            
    }
}
