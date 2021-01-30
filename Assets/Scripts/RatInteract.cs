using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatInteract : MonoBehaviour
{

    public string activeItem;
    public GameObject itemChestOpen;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ItemContainer" && Input.GetKey("e")) {
            itemChestOpen.SetActive(true);
        }
        if (col.tag == "Queue" && Input.GetKey("e")) {
            Debug.Log("Queue stuff activated");
        }
    }

}
