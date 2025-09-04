using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    [SerializeField] private string _soundName;

    public void PlaySound()
    {
        AudioManager.Instance.PlaySingleSound(_soundName);
    }
}
