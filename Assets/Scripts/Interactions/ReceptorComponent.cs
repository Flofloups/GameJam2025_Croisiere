using Assets.Scripts.Interactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ReceptorComponent : MonoBehaviour
{
    [SerializeField] private int _barrelNumber;
    [SerializeField] private CheckAnswers _checkAnswers;
    [SerializeField] private UnityEvent _onObjectReceived;
    
    public void OnObjectReceived(int ingredientNumber)
    {
        _checkAnswers.CheckAnswer(_barrelNumber == ingredientNumber);
        _onObjectReceived?.Invoke();

    }
}
