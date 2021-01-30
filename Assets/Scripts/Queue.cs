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
    
    public void SpawnToQueue(KeyValuePair<string, List<string>> animalInfo, GameObject animalPrefab, Sprite animalSprite) {
        GameObject newObject = Instantiate(animalPrefab, tail.position, tail.rotation);
        Animal newAnimal = newObject.AddComponent<Animal>();
        newAnimal.Initialize(animalInfo.Key, Random.Range(30f,80f), animalInfo.Value[Random.Range(0, animalInfo.Value.Count)]);
        newObject.GetComponent<SpriteRenderer>().sprite = animalSprite;
        newObject.GetComponent<Animal>().Move(head);
        head.position += new Vector3(1f, 0, 0);
    }
}
