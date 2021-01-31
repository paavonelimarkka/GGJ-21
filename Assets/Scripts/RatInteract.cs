using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatInteract : MonoBehaviour
{

    public GameObject activeItem;
    public GameObject itemChestOpen;

    public bool carrying = false;    
    public void SetActiveToHands(bool setItem) {
        GameObject hands = this.gameObject.transform.GetChild(0).gameObject;
        if (!setItem) {
            hands.GetComponent<SpriteRenderer>().sprite = null;
            carrying = false;
        } else {
            hands.GetComponent<SpriteRenderer>().sprite = activeItem.GetComponent<SpriteRenderer>().sprite;
            carrying = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ItemContainer" && Input.GetKey("e")) {
            itemChestOpen.SetActive(true);
        }
        if (col.tag == "Queue" && Input.GetKey("e")) {
            Debug.Log("Queue stuff activated");
            bool success = col.gameObject.GetComponent<Animal>().OfferItem(activeItem.GetComponent<Draggable>().itemType);
            if (success) {
                Destroy(activeItem);
                SetActiveToHands(false);
            }
        }
    }

}
