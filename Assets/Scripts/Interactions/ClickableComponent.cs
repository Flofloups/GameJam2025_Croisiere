using Assets.Scripts.Interactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableComponent : InteractableComponent, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent _onPointerUp = new UnityEvent();
    [SerializeField] private UnityEvent _onPointerDown = new UnityEvent();
    [SerializeField] private bool isGood = false;
    [SerializeField] private CheckAnswers checkAnswer;
    
    
    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        SetOnTop();
        _onPointerDown.Invoke();

        //GameObject obj = pointerEventData.pointerPress;
        checkAnswer.CheckAnswer(isGood);
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _onPointerUp.Invoke();
    }
}