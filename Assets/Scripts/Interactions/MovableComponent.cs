using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableComponent : InteractableComponent, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private int _ingredientNumber;

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetOnTop();
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
                enabled = false;
                return;
            }
        }
    }
}
