using TMPro;
using UnityEngine;

public class NextSceneButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _duration;
    [SerializeField] private TMP_Text _textMeshPro;

    private int _animDirection;

    private bool _isVictory;
    
    private void Start()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _animDirection = 0;
    }

    public void SetVictory(bool isSuccess)
    {
        _isVictory = isSuccess;
    }
    
    public void Display()
    {
        _animDirection = 1;
        _textMeshPro.text = _isVictory ? "Continue" : "Main Menu";
    }

    public void GoToNextScene()
    {
        if (_isVictory)
        {
            GameLoopManager.Instance.LoadNextScene();
        }
        else
        {
            GameLoopManager.Instance.LoadMenuScene();
        }
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (_animDirection > 0)
        {
            _canvasGroup.alpha += Time.deltaTime / _duration;
            if (_canvasGroup.alpha >= 1f)
            {
                _canvasGroup.alpha = 1;
                _animDirection = 0;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
