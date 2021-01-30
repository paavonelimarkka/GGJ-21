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

    void Awake() {
        head = this.gameObject.transform.GetChild(0).gameObject;
        tail = this.gameObject.transform.GetChild(1).gameObject;
    }
    
    public void SpawnToQueue(KeyValuePair<string, List<string>> animalInfo, GameObject animalPrefab, Sprite animalSprite) {
        GameObject newObject = Instantiate(animalPrefab, tail.transform.position, tail.transform.rotation);
        Animal newAnimal = newObject.AddComponent<Animal>();
        newAnimal.Initialize(animalInfo.Key, Random.Range(30f,80f), animalInfo.Value[Random.Range(0, animalInfo.Value.Count)]);
        newObject.GetComponent<SpriteRenderer>().sprite = animalSprite;
        Vector3 wayBack = tail.transform.position + moveWayPoint;
        Vector3 wayPoint = head.transform.position + (animalsInQueue * moveWayPoint);
        animalsInQueue++;
        newObject.GetComponent<Animal>().Move(wayPoint, wayBack);
        newObject.GetComponent<Animal>().queueReference = this;
        animalsList.Add(newObject);
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
}
