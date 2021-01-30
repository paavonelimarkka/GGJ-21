using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // UI Stuff
    [SerializeField]
    private Text statusText;

    // Draggable gameobjects
    [SerializeField]
    private List<GameObject> draggables;

    // Container for draggable objects
    [SerializeField]
    private GameObject container;

    // Item prefab to create
    [SerializeField]
    private GameObject itemPrefab;

    // Level info used to populate the queue & chest/box/whatever
    [SerializeField]
    private Dictionary<string, List<string>> levelInfo = 
        new Dictionary<string, List<string>>() {
            {"Lion", new List<string>{ "Tooth", "Mane", "Severed leg" }},
            {"Bear", new List<string>{ "Claw", "Honey", "Berries" }},
            {"Cat", new List<string>{ "Ball of yarn", "Catnip", "Mouse toy" }},
            {"Shark", new List<string>{ "Fin", "Snorkle", "Harpoon" }},
            {"Dog", new List<string>{ "Bone", "Chewed football", "AutoPet4000" }},
        };

    // The actual 'Animals'
    private List<Animal> animals = new List<Animal>();
    
    void Awake() 
    {
        // Generate animals and animal items, populate a container with items
        foreach (KeyValuePair<string, List<string>> animal in levelInfo) {
            // New Animal, with name, hurryAmount(seconds), wanted item randomly from the list of potential items
            Animal newAnimal = new Animal(animal.Key, Random.Range(6, 60), animal.Value[Random.Range(0, animal.Value.Count)]);
            animals.Add(newAnimal);
            Debug.Log(newAnimal.ToString());

            // Set up the new item with random color
            GameObject newItem = itemPrefab;
            randomizeColor(newItem);

            // Get container bounding rect and select a random position inside it (with a margin for graphics)
            float containerMargin = 1;
            Vector3 randomPosition = container.transform.position + getRandomSpawn(container, containerMargin);
            
            Instantiate(newItem, randomPosition, container.transform.rotation);
        }
    }


    void Start()
    {
        setStatusText("Status");
    }
    public void setStatusText(string text) {
        statusText.GetComponent<Text>().text = text;
    }

    // Randomize GameObjects color
    public void randomizeColor(GameObject obj) {
        Color randomColor = new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );
        SpriteRenderer targetRenderer = obj.GetComponent<SpriteRenderer>();
        targetRenderer.color = randomColor;
    }

    // Get randomized Vector3 to add to containers position to get a spawnpoint inside its sprite.
    public Vector3 getRandomSpawn(GameObject obj, float margin) {
        SpriteRenderer targetSprite = obj.GetComponent<SpriteRenderer>();
        
        float xMin = -targetSprite.bounds.size.x/2 + margin;
        float xMax = targetSprite.bounds.size.x/2 - margin;
        float yMin = -targetSprite.bounds.size.y/2 + margin;
        float yMax = targetSprite.bounds.size.y/2 - margin;
        return new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
    }
}
