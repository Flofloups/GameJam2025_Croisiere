using System.Collections.Generic;
using SceneReferenceUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager Instance { get; private set; }

    [SerializeField] private SceneReference _uiScene;
    
    [SerializeField] private List<SceneReference> _sceneReferences = new List<SceneReference>();
    
    private int _currentSceneIndex = -2;
    
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
        if (SceneManager.GetActiveScene().name == "_Boot")
        {
            LoadMenuScene();
        }
        else if (SceneManager.GetActiveScene().name == _uiScene.Name)
        {
            _currentSceneIndex = -1;
        }
        else
        {
            for (int i = 0; i < _sceneReferences.Count; i++)
            {
                if (_sceneReferences[i].Name == SceneManager.GetActiveScene().name)
                {
                    _currentSceneIndex = i;
                    return;
                }
            }
        }
    }

    public void LoadNextScene()
    {
        if (_sceneReferences.Count < _currentSceneIndex + 1)
        {
            LoadMenuScene();
        }
        else
        {
            LoadGameScene(_currentSceneIndex + 1);
        }
    }

    public void LoadGameScene(int sceneIndex)
    {
        if (TransitionManager.Instance.IsTransitioning || _currentSceneIndex == sceneIndex) return;
        if (_sceneReferences.Count <= sceneIndex || sceneIndex < 0) return;
        
        LoadScene(_sceneReferences[sceneIndex]);
        _currentSceneIndex = sceneIndex;
    }

    public void LoadMenuScene()
    {
        if (TransitionManager.Instance.IsTransitioning || _currentSceneIndex == -1) return;
        
        LoadScene(_uiScene);
        _currentSceneIndex = -1;
    }

    private void LoadScene(SceneReference sceneReference)
    {
        TransitionManager.Instance.FadeInTransition(OnTransitionComplete);

        void OnTransitionComplete()
        {
            AsyncOperation asyncOpLoad = sceneReference.LoadSceneAsync(LoadSceneMode.Single);
            
            if (asyncOpLoad == null)
            {
                OnSceneLoaded(null);
            }
            else
            {
                asyncOpLoad.completed += OnSceneLoaded;
                asyncOpLoad.allowSceneActivation = true;
            }
            void OnSceneLoaded(AsyncOperation _)
            {
                if (asyncOpLoad?.isDone ?? true)
                {
                    TransitionManager.Instance.FadeOutTransition();
                }
            }
        }
    }
}
