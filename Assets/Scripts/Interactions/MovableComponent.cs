using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MovableComponent : InteractableComponent, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private int _ingredientNumber;

    [SerializeField] private UnityEvent _onDragBeginEvent = new UnityEvent();
    [SerializeField] private UnityEvent _dragInReceptorEvent = new UnityEvent();

    private void Start()
    {
        Debug.Log("adafa");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetOnTop();
        _onDragBeginEvent?.Invoke();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        CheckForReceptor(eventData);
    }
    
    private void SetDraggedPosition(PointerEventData data)
    {
        transform.position = data.position;
    }

    private void CheckForReceptor(PointerEventData data)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        CameraManager.Instance.EventSystem.RaycastAll(data, results);
        foreach (RaycastResult r in results)
        {
            if (r.gameObject.TryGetComponent(out ReceptorComponent receptor))
            {
                receptor.OnObjectReceived(_ingredientNumber);
                _dragInReceptorEvent?.Invoke(); 
                enabled = false;
                return;
            }
        }
    }
}
