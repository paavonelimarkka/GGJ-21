using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stupid class to keep Animals in TODO: maybe some common functions (i.e. speed up, slow down)

public class Animal
{
    private string name;
    private float hurry;
    private AnimalItem item;
    private DateTime spawnTime;

    private int hurryMultiplier = 6;

    public Animal(string animalName, float animalHurry, string wantedItem) {
        name = animalName;
        hurry = animalHurry;
        item = new AnimalItem(wantedItem);
        spawnTime = new DateTime();
    }

    // Override ToString() for easier debuggin
    public override string ToString() {
        return $"I am {name}, I need to get the item in {spawnTime.AddSeconds(hurry * hurryMultiplier).ToString("ss")} seconds || My Item: {item.ToString()}";
    }
}
