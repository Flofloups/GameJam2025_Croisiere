using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultFeedback : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private readonly int SuccessTrigger = Animator.StringToHash("Success");
    private readonly int FailureTrigger = Animator.StringToHash("Failure");


    public void ShowResultFeedback(bool isSuccess)
    {
        _animator.SetTrigger(isSuccess ? SuccessTrigger : FailureTrigger);
    }
}
