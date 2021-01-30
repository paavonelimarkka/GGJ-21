using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip bgMusic;

    void Awake() {
        DontDestroyOnLoad (this.gameObject);
        SoundManager.Instance.PlayMusic(bgMusic);
    }
}