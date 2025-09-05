using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeComponent : MonoBehaviour
{

    [SerializeField] private Image fruit;

    public void FadeOut() {

        fruit.DOFade(0, 0.5f);
     
    }


    private void Reset()
    {
        fruit = GetComponent<Image>();
    }
}
