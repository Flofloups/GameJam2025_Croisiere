using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    
    [field: SerializeField] public Camera MainCamera { get; private set; }
    [field: SerializeField] public EventSystem EventSystem { get; private set; }
    
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

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}
