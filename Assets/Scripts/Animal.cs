using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stupid class to keep Animals in TODO: maybe some common functions (i.e. speed up, slow down)

public class Animal : MonoBehaviour
{
    private string animalType;
    private List<string> wantedItems;
    private System.DateTime spawnTime;
    private Sprite animalSprite;

    public Vector3 wayPoint;
    private Vector3 wayPointBack;

    private float speed;
    private float defaultSpeed = 0.1f;
    private bool moving = false;
    private bool showingHint = false;

    private float fullTime;
    private float targetTime;

    private float progressFull = 360f;
    private SpriteRenderer progress;

    private GameObject hintObject;

    private bool destructOnArrival = false;
    public Queue queueReference;
    private GameController gameController;

    public void Initialize(string animal, float animalHurry, List<string> itemList) {
        animalType = animal;
        fullTime = animalHurry;
        targetTime = fullTime;
        speed = defaultSpeed + (animalHurry / 3f);
        wantedItems = itemList;
        spawnTime = new System.DateTime();
        animalSprite = this.GetComponent<AnimalResources>().animalSprites[animal];
        GetComponent<SpriteRenderer>().sprite = animalSprite;
    }
    void Start() {
        GameObject progressObject = this.gameObject.transform.GetChild(0).gameObject;
        hintObject = this.gameObject.transform.GetChild(2).gameObject;
        gameController = GameObject.Find("/GameController").GetComponent<GameController>();
        progress = progressObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        targetTime -= Time.deltaTime;
        progress.material.SetFloat("_Arc2", targetTime * (progressFull/fullTime));
        if (targetTime <= 0f && !moving) {
            AddStrikeAndLeave();
        }
        if (targetTime < fullTime/2 && !showingHint) {
            showingHint = true;
            StartCoroutine(ShowHint());
        }
        if (moving) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, step);
            if (transform.position == wayPoint) {
                moving = false;
                if (destructOnArrival) {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void AddStrikeAndLeave() {
        gameController.AddStrike();
        SetMood("Bad");
        Leave();
    }
    
    public string AnimalInfo() {
        return $"I am {name}, I need to get the item in {spawnTime.AddSeconds(fullTime).ToString("ss")} seconds || My Item: ";
    }

    public void Move(Vector3 moveTo) {
        Debug.Log(moveTo + "UPDATED MOVETO POSITION ON ANIMAL");
        wayPoint = moveTo;
        moving = true;
    }

    public void Move(Vector3 moveTo, Vector3 moveBack) {
        Debug.Log(moveTo + "MOVETO POSITION ON ANIMAL");
        wayPoint = moveTo;
        wayPointBack = moveBack;
        moving = true;
    }

    public void Leave() {
        queueReference.LeaveFromQueue(this.gameObject);
        PlaySound();
        GetComponent<SpriteRenderer>().flipX = true;
        wayPoint = wayPointBack;
        moving = true;
        destructOnArrival = true;
    }

    public void SetMood(string moodToSet) {
        hintObject.SetActive(false);
        GameObject mood = this.gameObject.transform.GetChild(1).gameObject;
        mood.GetComponent<SpriteRenderer>().sprite = this.GetComponent<AnimalResources>().moodSprites[moodToSet];
        mood.SetActive(true);
    }

    IEnumerator ShowHint() {
        progress.gameObject.SetActive(false);
        hintObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = gameController.itemSprites[wantedItems[Random.Range(0, wantedItems.Count)]];
        hintObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        hintObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = gameController.itemSprites[wantedItems[Random.Range(0, wantedItems.Count)]];
        yield return new WaitForSeconds(2f);
        hintObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = gameController.itemSprites[wantedItems[Random.Range(0, wantedItems.Count)]];
        hintObject.SetActive(false);
        progress.gameObject.SetActive(true);
        showingHint = false;
    }

    public void PlaySound() {
        AudioClip animalSound = this.GetComponent<AnimalResources>().animalSounds[animalType];
        SoundManager.Instance.Play(animalSound);
    }
    
    public bool OfferItem(string offeredItem) {
        gameController.ItemOffered();
        if (wantedItems.Contains(offeredItem)) {
            gameController.scoreInt += (int)targetTime * Random.Range(3,10);
            Debug.Log("Giitti mage!");
            SetMood("Good");
            Leave();
            return true;
        }
        AddStrikeAndLeave();
        return false;
    }
}
