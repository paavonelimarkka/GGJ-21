using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatInteract : MonoBehaviour
{


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ItemContainer" && Input.GetKey("e")) {
            Debug.Log("Item container stuff activated");
        }
        if (col.tag == "Queue" && Input.GetKey("e")) {
            Debug.Log("Queue stuff activated");
        }
    }

}
