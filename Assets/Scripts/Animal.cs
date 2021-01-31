using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stupid class to keep Animals in TODO: maybe some common functions (i.e. speed up, slow down)

public class Animal : MonoBehaviour
{
    private string animalType;
    private List<string> wantedItems;
    private DateTime spawnTime;
    private Sprite animalSprite;

    public Vector3 wayPoint;
    private Vector3 wayPointBack;

    private float speed;
    private float defaultSpeed = 0.1f;
    private bool moving = false;

    private float fullTime;
    private float targetTime;

    private float progressFull = 360f;
    private SpriteRenderer progress;

    private bool destructOnArrival = false;
    public Queue queueReference;
    private GameController gameController;

    public void Initialize(string animal, float animalHurry, List<string> itemList) {
        animalType = animal;
        fullTime = 10f;
        targetTime = fullTime;
        speed = defaultSpeed + (animalHurry / 1000f);
        wantedItems = itemList;
        spawnTime = new DateTime();
        speed = defaultSpeed * animalHurry;
        animalSprite = this.GetComponent<AnimalResources>().animalSprites[animal];
        GetComponent<SpriteRenderer>().sprite = animalSprite;
    }
    void Start() {
        GameObject progressObject = this.gameObject.transform.GetChild(0).gameObject;
        gameController = GameObject.Find("/GameController").GetComponent<GameController>();
        progress = progressObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        targetTime -= Time.deltaTime;
        progress.material.SetFloat("_Arc2", targetTime * (progressFull/fullTime));
        if (targetTime <= 0f && !moving) {
            gameController.AddStrike();
            SetMood("Bad");
            Leave();
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
        GameObject mood = this.gameObject.transform.GetChild(1).gameObject;
        mood.GetComponent<SpriteRenderer>().sprite = this.GetComponent<AnimalResources>().moodSprites[moodToSet];
        mood.SetActive(true);
    }

    public void PlaySound() {
        AudioClip animalSound = this.GetComponent<AnimalResources>().animalSounds[animalType];
        SoundManager.Instance.Play(animalSound);
    }
    
    public void OfferItem(string offeredItem) {
        if (wantedItems.Contains(offeredItem)) {
            Debug.Log("Giitti mage!");
            SetMood("Good");
            Leave();
        }
    }
}
