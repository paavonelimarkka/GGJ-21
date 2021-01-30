using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalResources : MonoBehaviour
{
    // Create custom structs because Unity cant show Dictionaries in the editor :|
    [Serializable]
    public struct NamedSprite {
        public string name;
        public Sprite sprite;
    }
    [Serializable]
    public struct NamedSound {
        public string name;
        public AudioClip sound;
    }

    [SerializeField]
    public NamedSprite[] animals;
    [SerializeField]
    public NamedSprite[] moods;
    [SerializeField]
    public NamedSound[] sounds;

    public Dictionary<string, Sprite> animalSprites;
    public Dictionary<string, Sprite> moodSprites;
    public Dictionary<string, AudioClip> animalSounds;

    void Awake() {
        // Initialize the custom structs to Dictionaries for easier access
        animalSprites = new Dictionary<string, Sprite>();
        foreach(NamedSprite animal in animals){
            animalSprites.Add(animal.name, animal.sprite);
        }
        moodSprites = new Dictionary<string, Sprite>();
        foreach(NamedSprite mood in moods){
            moodSprites.Add(mood.name, mood.sprite);
        }
        animalSounds = new Dictionary<string, AudioClip>();
        foreach(NamedSound sound in sounds){
            animalSounds.Add(sound.name, sound.sound);
        }
    }
}
