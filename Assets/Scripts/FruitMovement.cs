using DG.Tweening;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    private Tween _movementTween;
    
    public void MoveToPosition(Vector3 position)
    {
        _movementTween = transform.DOMove(position, 1f).SetEase(Ease.InOutExpo);
    }

    public void KillMovement()
    {
        _movementTween?.Kill();
    }
}
