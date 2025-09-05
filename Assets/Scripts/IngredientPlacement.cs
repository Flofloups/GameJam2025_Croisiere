using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPlacement : MonoBehaviour
{
    [SerializeField] private List<FruitMovement> _fruitMovements;

    private int _currentFruitMovement = -1;
    
    private void Start()
    {
        BringNextFruit();
    }

    public void BringNextFruit()
    {
        _currentFruitMovement++;
        if (_fruitMovements.Count <= _currentFruitMovement) return;
        
        _fruitMovements[_currentFruitMovement].MoveToPosition(transform.position);
    }
}
