using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }
    
    [SerializeField] private AnimationClip _transitionClip;
    [SerializeField] private Transform _canvasTransform;
    [SerializeField, Min(.1f)] private float _transitionDuration;
    
    private float _transitionPercent;
    private int _transitionDirection;

    private Action _onTransitionAction;
    
    public bool IsTransitioning => _transitionDirection != 0;
    
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _transitionPercent = 1;
        _transitionClip.SampleAnimation(_canvasTransform.gameObject, _transitionPercent);
    }

    public void FadeInTransition(Action onTransitionComplete)
    {
        if (_transitionPercent == 1)
        {
            onTransitionComplete?.Invoke();
            _onTransitionAction = null;
            return;
        }
        _onTransitionAction = onTransitionComplete;
        _transitionPercent = 0;
        _transitionDirection = 1;
    }

    public void FadeOutTransition(Action onTransitionComplete = null)
    {
        if (_transitionPercent == 0)
        {
            onTransitionComplete?.Invoke();
            _onTransitionAction = null;
            return;
        }
        _onTransitionAction = onTransitionComplete;
        _transitionPercent = 1;
        _transitionDirection = -1;
    }

    private void Update()
    {
        if (_transitionDirection != 0)
        {
            _transitionPercent += _transitionDirection * Time.deltaTime / _transitionDuration;
            _transitionClip.SampleAnimation(_canvasTransform.gameObject, _transitionPercent);
            if ((_transitionPercent >= 1 && _transitionDirection == 1) || (_transitionPercent <= 0 && _transitionDirection == -1))
            {
                _transitionDirection = 0;
                _transitionPercent = Mathf.Clamp01(_transitionPercent);
                _onTransitionAction?.Invoke();
            }
        }
    }
}
