using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stupid class to keep Animals in TODO: maybe some common functions (i.e. speed up, slow down)

public class Animal : MonoBehaviour
{
    private string animalType;
    private float hurry;
    private AnimalItem item;
    private DateTime spawnTime;

    private Transform wayPoint;

    private float speed;
    private float defaultSpeed = 2f;
    private bool moving = false;

    public void Initialize(string animal, float animalHurry, string wantedItem) {
        animalType = animal;
        hurry = animalHurry;
        item = new AnimalItem(wantedItem);
        spawnTime = new DateTime();
        speed = defaultSpeed * animalHurry;
    }

    void Update() {
        float step = defaultSpeed * Time.deltaTime;
        if (moving) {
            transform.position = Vector2.MoveTowards(transform.position, wayPoint.position, step);
            if (transform.position == wayPoint.position) {
                moving = false;
            }
        }
    }
    
    public string AnimalInfo() {
        return $"I am {name}, I need to get the item in {spawnTime.AddSeconds(hurry).ToString("ss")} seconds || My Item: {item.ToString()}";
    }

    public void Move(Transform moveTo) {
        wayPoint = moveTo;
        moving = true;
    }
}
