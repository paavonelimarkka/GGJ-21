using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Item sprites struct for editor
    [System.Serializable]
    public struct NamedSprite {
        public string name;
        public Sprite sprite;
    }

    [SerializeField]
    public NamedSprite[] items;
    public Dictionary<string, Sprite> itemSprites;
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

    private bool canSpawn;

    // PLAYER *HEALTH*
    public GameObject ratPlayer;
    private int strikes = 0;
    public bool gameOver = false;
    public GameObject strikeCounter;
    // Level info used to populate the queue & chest/box/whatever
    private List<KeyValuePair<string, List<string>>> firstLvlAnimals = new List<KeyValuePair<string, List<string>>>();
    // Make an instance of LevelInfo parameters: int level, int timelimit,
    // Dictionary<string, List<string>> animalDictionary with animal names as keys and list of possible lost items as value
    private LevelInfo firstLevel;
    private List<string> levelItems;

    void Awake() 
    {
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Bat", new List<string>{ "bloodpack", "teeth", "garlic" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Bulldog", new List<string>{ "cigar", "bone", "ball" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Elephant", new List<string>{ "vase", "tukki", "allas" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Lion", new List<string>{ "mane", "crown", "conditioner" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Reindeer", new List<string>{ "bell", "nose", "present" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Shark", new List<string>{ "surfboard", "snorkle", "harpoon" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Turtle", new List<string>{ "nunchaku", "pizza", "turtlewax" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Rabbit", new List<string>{ "carrot", "clock", "ears" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Bear", new List<string>{ "honey", "salmon", "marjapoimuri" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Cock", new List<string>{ "aapinen", "flakes", "alarmclock" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Octopus", new List<string>{ "beachball", "muste", "treasure" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Bat", new List<string>{ "bloodpack", "teeth", "garlic" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Bulldog", new List<string>{ "cigar", "bone", "ball" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Elephant", new List<string>{ "vase", "tukki", "allas" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Lion", new List<string>{ "mane", "crown", "conditioner" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Reindeer", new List<string>{ "bell", "nose", "present" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Shark", new List<string>{ "surfboard", "snorkle", "harpoon" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Turtle", new List<string>{ "nunchaku", "pizza", "turtlewax" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Rabbit", new List<string>{ "carrot", "clock", "ears" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Bear", new List<string>{ "honey", "salmon", "marjapoimuri" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Cock", new List<string>{ "aapinen", "flakes", "alarmclock" }));
        firstLvlAnimals.Add(new KeyValuePair<string, List<string>>("Octopus", new List<string>{ "beachball", "muste", "treasure" }));
        
        canSpawn = true;
        // Generate animals and animal items, populate a container with items
        itemSprites = new Dictionary<string, Sprite>();
        foreach(NamedSprite item in items){
            itemSprites.Add(item.name, item.sprite);
        }
        levelItems = GenerateItemList(10);
        PopulateContainer();
    }


    void Start()
    {
        SetStatusText("Status");
        StartCoroutine(AnimalSpawner(10f, 3f));
    }
    public void SetStatusText(string text) {
        statusText.GetComponent<Text>().text = text;
    }

    public void AddStrike() {
        strikes ++;
        strikeCounter.GetComponent<StrikeCounter>().strikeCount = strikes;
        if (strikes == 3) {
            float waitUntilLoad = 0.5f;
            StartCoroutine(GameOver(waitUntilLoad));
        }
    }
    IEnumerator GameOver(float waitUntilLoad) {
        gameOver = true;
        yield return new WaitForSeconds(waitUntilLoad);
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

    IEnumerator AnimalSpawner(float wait, float randomizer)
    {
        while(canSpawn && firstLvlAnimals.Count > 0) {
            yield return new WaitForSeconds(1f);
            SpawnAnimal();
            yield return new WaitForSeconds(wait + Random.Range(-randomizer, randomizer));
        }
    }

    public void SpawnAnimal() {
        int randomAnimalIndex = Random.Range(0, firstLvlAnimals.Count);
        KeyValuePair<string, List<string>> randomAnimalInfo = firstLvlAnimals[randomAnimalIndex];
        GameObject chosenQueue = queues[Random.Range(0, queues.Count)];
        chosenQueue.GetComponent<Queue>().SpawnToQueue(randomAnimalInfo, animalPrefab);
        firstLvlAnimals.Remove(randomAnimalInfo);
    }

    private List<string> GenerateItemList(int additionalItems) {
        List<string> itemList = new List<string>();
        foreach(KeyValuePair<string, List<string>> animal in firstLvlAnimals) {
            itemList.Add(animal.Value[Random.Range(0, animal.Value.Count)]);
        }
        for(int i = 0; i < additionalItems; i++) {
            List<string> randomItemsList = firstLvlAnimals[Random.Range(0, firstLvlAnimals.Count)].Value;
            itemList.Add(randomItemsList[Random.Range(0, randomItemsList.Count)]);
        }
        return itemList;
    }

    private void PopulateContainer() {
        foreach (string item in levelItems) {
            // Get container bounding rect and select a random position inside it (with a margin for graphics)
            float containerMargin = 1;
            Vector3 randomPosition = container.transform.position + GetRandomSpawn(container, containerMargin);
            GameObject newItem = Instantiate(itemPrefab, randomPosition, container.transform.rotation);
            Draggable newDraggable = newItem.GetComponent<Draggable>();
            newDraggable.rat = ratPlayer;
            newDraggable.itemType = item;
            newItem.GetComponent<SpriteRenderer>().sprite = itemSprites[item];
            newItem.transform.SetParent(container.transform);
        }
    }
}
