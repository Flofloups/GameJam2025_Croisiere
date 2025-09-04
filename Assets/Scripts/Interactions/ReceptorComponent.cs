using System.Collections.Generic;
using Assets.Scripts.Interactions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ReceptorComponent : MonoBehaviour
{
    [SerializeField] private int _barrelNumber;
    [SerializeField] private CheckAnswers _checkAnswers;
    
    public void OnObjectReceived(int ingredientNumber)
    {
        _checkAnswers.CheckAnswer(_barrelNumber == ingredientNumber);
    }
}
