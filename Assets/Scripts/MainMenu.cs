using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameLoopManager.Instance.LoadNextScene();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
