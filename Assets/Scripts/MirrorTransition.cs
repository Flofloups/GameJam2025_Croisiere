using UnityEngine;
using UnityEngine.Events;

public class MirrorTransition : MonoBehaviour
{
    [SerializeField] private AnimationClip _transitionClip;
    [SerializeField] private Transform _canvasTransitionTransform, _canvasMirrorTransform;
    [SerializeField, Min(.1f)] private float _transitionDuration;
    [SerializeField] private UnityEvent _onTransitionComplete;
    
    private float _transitionPercent;
    private int _transitionDirection;
    
    
    private void Start()
    {
        _transitionPercent = 0;
        _transitionClip.SampleAnimation(_canvasTransitionTransform.gameObject, _transitionPercent);
    }

    public void StartTransition()
    {
        _transitionDirection = 1;
        _transitionPercent = 0;
    }
    
    private void Update()
    {
        if (_transitionDirection != 0)
        {
            _transitionPercent += _transitionDirection * Time.deltaTime * .5f / _transitionDuration;
            _transitionClip.SampleAnimation(_canvasTransitionTransform.gameObject, _transitionPercent);
            if (_transitionPercent >= 1 && _transitionDirection == 1)
            {
                _transitionDirection = -1;
                _transitionPercent = 1f;
                _canvasMirrorTransform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (_transitionPercent <= 0 && _transitionDirection == -1)
            {
                _transitionDirection = 0;
                _transitionPercent = 0;
            }
        } }
}
