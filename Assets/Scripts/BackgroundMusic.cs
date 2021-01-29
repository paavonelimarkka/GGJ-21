using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip bgMusic;

    void Start() {
        SoundManager.Instance.PlayMusic(bgMusic);
    }
}