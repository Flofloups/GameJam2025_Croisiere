using UnityEngine;

public class VictoryButton : MonoBehaviour
{
    [SerializeField] private NextSceneButton _nextSceneButton;
    [SerializeField, Min(0.1f)] private float _duration;
    private float _timer;
    
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _duration)
        {
            _nextSceneButton?.Display();
            enabled = false;
        }
    }
}
