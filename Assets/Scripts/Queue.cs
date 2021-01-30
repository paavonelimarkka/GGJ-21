using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    private int inQueue;
    
    private Transform head;
    private Transform tail;

    void Awake() {
        head = this.gameObject.transform.GetChild(0);
        tail = this.gameObject.transform.GetChild(1);
    }
    
    public void SpawnToQueue(KeyValuePair<string, List<string>> animalInfo, GameObject animalPrefab) {
        GameObject newObject = Instantiate(animalPrefab, tail.transform.position, tail.transform.rotation);
        Animal newAnimal = newObject.AddComponent<Animal>();
        newAnimal.Initialize(animalInfo.Key, Random.Range(6,60), animalInfo.Value[Random.Range(0, animalInfo.Value.Count)]);
        newObject.GetComponent<Animal>().Move(head.transform);
        head.transform.position =- new Vector3(-0.02f, 0, 0);
    }
}
