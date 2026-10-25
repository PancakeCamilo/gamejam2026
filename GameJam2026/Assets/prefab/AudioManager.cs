using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio References")]
    public AudioSource MusicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps playing across scene changes
        }
        else
        {
            Destroy(gameObject); // Prevents audio restart/overlap when reloading scenes
        }
    }
}