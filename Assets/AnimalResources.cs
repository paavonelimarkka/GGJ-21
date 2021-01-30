using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalResources : MonoBehaviour
{
    [Serializable]
    public struct NamedSprite {
        public string name;
        public Sprite sprite;
    }

    [SerializeField]
    public NamedSprite[] animals;
    [SerializeField]
    public NamedSprite[] moods;

    public Dictionary<string, Sprite> animalSprites;
    public Dictionary<string, Sprite> moodSprites;

    void Awake() {
        animalSprites = new Dictionary<string, Sprite>();
        foreach(NamedSprite animal in animals){
            animalSprites.Add(animal.name, animal.sprite);
        }
        moodSprites = new Dictionary<string, Sprite>();
        foreach(NamedSprite mood in moods){
            moodSprites.Add(mood.name, mood.sprite);
        }
    }
}
