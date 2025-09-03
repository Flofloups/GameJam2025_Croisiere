using System;
using System.Collections;
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
        LoadMenuScene();
    }

    public void LoadNextScene()
    {
        if (_sceneReferences.Count >= _currentSceneIndex + 1)
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
        if (_sceneReferences.Count >= sceneIndex || sceneIndex < 0) return;
        
        LoadScene(_sceneReferences[sceneIndex], _currentSceneIndex);
        _currentSceneIndex = sceneIndex;
    }

    public void LoadMenuScene()
    {
        if (TransitionManager.Instance.IsTransitioning || _currentSceneIndex == -1) return;
        
        LoadScene(_uiScene, _currentSceneIndex);
        _currentSceneIndex = -1;
    }

    private void LoadScene(SceneReference sceneReference, int previousSceneIndex)
    {
        TransitionManager.Instance.FadeInTransition(OnTransitionComplete);

        void OnTransitionComplete()
        {
            AsyncOperation asyncOpLoad = sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            AsyncOperation asyncOpUnload = previousSceneIndex switch
            {
                -2 => null,
                -1 => _uiScene.UnloadSceneAsync(),
                _ => _sceneReferences[previousSceneIndex].UnloadSceneAsync(),
            };
            
            if (asyncOpLoad == null && asyncOpUnload == null)
            {
                OnSceneLoaded(null);
                return;
            }
            if (asyncOpLoad != null)
            {
                asyncOpLoad.completed += OnSceneLoaded;
                asyncOpLoad.allowSceneActivation = true;
            }
            if (asyncOpUnload != null)
            {
                asyncOpUnload.completed += OnSceneLoaded;
                asyncOpUnload.allowSceneActivation = true;
            }
            void OnSceneLoaded(AsyncOperation _)
            {
                if ((asyncOpLoad?.isDone ?? true) && (asyncOpUnload?.isDone ?? true))
                {
                    TransitionManager.Instance.FadeOutTransition();
                }
            }
        }
    }
}
