﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // UI Stuff
    [SerializeField]
    private Text statusText;

    // Draggable gameobjects
    [SerializeField]
    private List<GameObject> draggables;

    // Queues
    [SerializeField]
    private List<GameObject> queues;

    // Container for draggable objects
    [SerializeField]
    private GameObject container;

    // Item prefab
    [SerializeField]
    private GameObject itemPrefab;

    // Animal prefab
    [SerializeField]
    private GameObject animalPrefab;

    [SerializeField]
    private List<Sprite> animalSprites;

    private bool canSpawn;

    // PLAYER *HEALTH*
    private int strikes = 0;

    // Level info used to populate the queue & chest/box/whatever
    private List<KeyValuePair<string, List<string>>> firstLvlAnimals = new List<KeyValuePair<string, List<string>>>();
    // Make an instance of LevelInfo parameters: int level, int timelimit,
    // Dictionary<string, List<string>> animalDictionary with animal names as keys and list of possible lost items as value
    private LevelInfo firstLevel;
    
    void Awake() 
    {
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Lion", new List<string>{ "Tooth", "Mane", "Severed leg" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Lion", new List<string>{ "Tooth", "Mane", "Severed leg" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Lion", new List<string>{ "Tooth", "Mane", "Severed leg" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Lion", new List<string>{ "Tooth", "Mane", "Severed leg" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Reindeer", new List<string>{ "Claw", "Honey", "Berries" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Reindeer", new List<string>{ "Claw", "Honey", "Berries" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Reindeer", new List<string>{ "Claw", "Honey", "Berries" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Reindeer", new List<string>{ "Claw", "Honey", "Berries" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Turtle", new List<string>{ "Ball of yarn", "Catnip", "Mouse toy" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Turtle", new List<string>{ "Ball of yarn", "Catnip", "Mouse toy" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Turtle", new List<string>{ "Ball of yarn", "Catnip", "Mouse toy" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Turtle", new List<string>{ "Ball of yarn", "Catnip", "Mouse toy" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Shark", new List<string>{ "Fin", "Snorkle", "Harpoon" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Shark", new List<string>{ "Fin", "Snorkle", "Harpoon" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Shark", new List<string>{ "Fin", "Snorkle", "Harpoon" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Shark", new List<string>{ "Fin", "Snorkle", "Harpoon" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Dog", new List<string>{ "Bone", "Chewed football", "AutoPet4000" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Dog", new List<string>{ "Bone", "Chewed football", "AutoPet4000" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Dog", new List<string>{ "Bone", "Chewed football", "AutoPet4000" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Dog", new List<string>{ "Bone", "Chewed football", "AutoPet4000" }));
        canSpawn = true;
        // Generate animals and animal items, populate a container with items
        foreach (KeyValuePair<string, List<string>> animal in firstLvlAnimals) {
            // Set up the new item with random color
            GameObject newItem = itemPrefab;
            RandomizeColor(newItem);

            // Get container bounding rect and select a random position inside it (with a margin for graphics)
            float containerMargin = 1;
            Vector3 randomPosition = container.transform.position + GetRandomSpawn(container, containerMargin);
            
            //Instantiate(newItem, randomPosition, container.transform.rotation);
        }
    }


    void Start()
    {
        SetStatusText("Status");
        StartCoroutine(AnimalRandTimer(1f, 0.5f));
    }
    public void SetStatusText(string text) {
        statusText.GetComponent<Text>().text = text;
    }

    public void AddStrike() {
        strikes ++;
        if (strikes == 3) {
            GameOver();
        }
    }
    private void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
    // Randomize GameObjects color
    public void RandomizeColor(GameObject obj) {
        Color randomColor = new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );
        SpriteRenderer targetRenderer = obj.GetComponent<SpriteRenderer>();
        targetRenderer.color = randomColor;
    }

    // Get randomized Vector3 to add to containers position to get a spawnpoint inside its sprite.
    public Vector3 GetRandomSpawn(GameObject obj, float margin) {
        SpriteRenderer targetSprite = obj.GetComponent<SpriteRenderer>();
        
        float xMin = -targetSprite.bounds.size.x/2 + margin;
        float xMax = targetSprite.bounds.size.x/2 - margin;
        float yMin = -targetSprite.bounds.size.y/2 + margin;
        float yMax = targetSprite.bounds.size.y/2 - margin;
        return new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
    }

    IEnumerator AnimalRandTimer(float wait, float randomizer)
    {
        while(canSpawn && firstLvlAnimals.Count > 0) {
            yield return new WaitForSeconds(1f);
            SpawnAnimal();
            yield return new WaitForSeconds(wait + Random.Range(-randomizer, randomizer));
        }
    }

    public void SpawnAnimal() {
        int randomAnimalIndex = Random.Range(0, firstLvlAnimals.Count);
        Sprite randomSprite = animalSprites[Random.Range(0, animalSprites.Count)];
        KeyValuePair<string, List<string>> randomAnimalInfo = firstLvlAnimals[randomAnimalIndex];
        GameObject chosenQueue = queues[Random.Range(0, queues.Count)];
        chosenQueue.GetComponent<Queue>().SpawnToQueue(randomAnimalInfo, animalPrefab, randomSprite);
        firstLvlAnimals.Remove(randomAnimalInfo);
    }
}
