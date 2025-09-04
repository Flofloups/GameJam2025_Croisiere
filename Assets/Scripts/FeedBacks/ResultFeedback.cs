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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowResultFeedback(true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ShowResultFeedback(false);
        }
    }


    public void ShowResultFeedback(bool isSuccess)
    {
        _animator.SetTrigger(isSuccess ? SuccessTrigger : FailureTrigger);
    }
}
