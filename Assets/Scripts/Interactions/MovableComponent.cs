using UnityEngine.EventSystems;

public class MovableComponent : InteractableComponent, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetOnTop();
        SetDraggedPosition(eventData);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }
    
    private void SetDraggedPosition(PointerEventData data)
    {
        transform.position = data.position;
    }
}
