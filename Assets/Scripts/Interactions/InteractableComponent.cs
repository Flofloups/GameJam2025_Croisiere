using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InteractableComponent : MonoBehaviour
{
    protected void SetOnTop()
    {
        transform.SetSiblingIndex(transform.parent.childCount - 1);
    }
}
