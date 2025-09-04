using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableComponent : InteractableComponent, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent _onPointerUp = new UnityEvent();
    [SerializeField] private UnityEvent _onPointerDown = new UnityEvent();
    
    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _onPointerDown.Invoke();
        SetOnTop();
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _onPointerUp.Invoke();
    }
}