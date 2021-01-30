using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    private int inQueue;
    
    private GameObject head;
    private GameObject tail;
    private int animalsInQueue = 0;
    private Vector3 moveWayPoint = new Vector3(2f, 0, 0);

    private List<GameObject> animalsList = new List<GameObject>();
    private Animal currentAnimal;

    void Awake() {
        head = this.gameObject.transform.GetChild(0).gameObject;
        tail = this.gameObject.transform.GetChild(1).gameObject;
    }
    
    public void SpawnToQueue(KeyValuePair<string, List<string>> animalInfo, GameObject animalPrefab) {
        // Create the gameobject from animal prefab, attach the Animal class and Initialize it
        GameObject newAnimalObject = CreateAnimal(animalPrefab);
        InitializeAnimal(newAnimalObject, animalInfo);
        
        Vector3 wayBack = tail.transform.position + moveWayPoint;
        Vector3 wayPoint = head.transform.position + (animalsInQueue * moveWayPoint);
        animalsInQueue++;
        currentAnimal.Move(wayPoint, wayBack);
        currentAnimal.queueReference = this;
        animalsList.Add(newAnimalObject);
    }

    public void LeaveFromQueue(GameObject leavingAnimal) {
        animalsInQueue--;
        animalsList.Remove(leavingAnimal);
        foreach(GameObject animalObject in animalsList) {
            Animal animal = animalObject.GetComponent<Animal>();
            Vector3 newWayPoint = animal.wayPoint;
            newWayPoint -= moveWayPoint;
            Debug.Log(newWayPoint + "Uusi muutettu waypoint");
            animal.Move(newWayPoint);
        }
    }

    private GameObject CreateAnimal(GameObject prefab) {
        GameObject newObject = Instantiate(prefab, tail.transform.position, tail.transform.rotation);
        return newObject;
    }

    private void InitializeAnimal(GameObject animalObject, KeyValuePair<string, List<string>> animalInfo) {
        animalObject.AddComponent<Animal>();
        Animal animal = animalObject.GetComponent<Animal>();
        animal.Initialize(animalInfo.Key, Random.Range(30f,80f), animalInfo.Value[Random.Range(0, animalInfo.Value.Count)]);
        currentAnimal = animal;
    }
}
