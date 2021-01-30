using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRandomizer : MonoBehaviour
{

    public enum Animals {
        Bulldog,
        Lion,
        Shark
        };
    public float randTimer = 1.0f;
    private int animalsLength;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        animalsLength = Animals.GetValues(typeof(Animals)).Length;
        coroutine = AnimalRandTimer(randTimer);
        StartCoroutine(coroutine);
    }

    IEnumerator AnimalRandTimer(float Wait)
    {
        while(true) {
            yield return new WaitForSeconds(Wait);
            
            Debug.Log(Wait + " seconds went by");
            Animals chosenAnimal = (Animals)Random.Range(0,animalsLength);
            Debug.Log(chosenAnimal);
        }
    }

}
