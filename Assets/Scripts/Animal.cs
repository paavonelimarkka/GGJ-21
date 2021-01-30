using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stupid class to keep Animals in TODO: maybe some common functions (i.e. speed up, slow down)

public class Animal : MonoBehaviour
{
    private string animalType;
    private AnimalItem item;
    private DateTime spawnTime;

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

    public void Initialize(string animal, float animalHurry, string wantedItem) {
        animalType = animal;
        fullTime = 10f;
        targetTime = fullTime;
        speed = defaultSpeed + (animalHurry / 1000f);
        item = new AnimalItem(wantedItem);
        spawnTime = new DateTime();
        speed = defaultSpeed * animalHurry;
    }
    void Awake() {
        GameObject progressObject = this.gameObject.transform.GetChild(0).gameObject;
        progress = progressObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        targetTime -= Time.deltaTime;
        progress.material.SetFloat("_Arc2", targetTime * (progressFull/fullTime));
        if (targetTime <= 0f && !moving) {
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
        return $"I am {name}, I need to get the item in {spawnTime.AddSeconds(fullTime).ToString("ss")} seconds || My Item: {item.ToString()}";
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
        GameObject mood = this.gameObject.transform.GetChild(1).gameObject;
        mood.GetComponent<SpriteRenderer>().sprite = this.GetComponent<AnimalResources>().moodSprites["Bad"];
        mood.SetActive(true);
        queueReference.LeaveFromQueue(this.gameObject);
        GetComponent<SpriteRenderer>().flipX = true;
        wayPoint = wayPointBack;
        moving = true;
        destructOnArrival = true;
    }
}
