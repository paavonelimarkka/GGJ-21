using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatInteract : MonoBehaviour
{

    public GameObject activeItem;
    public GameObject itemChestOpen;
    public GameObject itemChest;


    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;


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
        if (col.gameObject == itemChest && Input.GetKey("e")) {
            itemChestOpen.SetActive(true);
                 
        }
        if (col.tag == "Queue" && Input.GetKeyDown("e")) {
            if (activeItem) {
                bool success = col.gameObject.GetComponent<Animal>().OfferItem(activeItem.GetComponent<Draggable>().itemType);
                if (success) {
                    Destroy(activeItem);
                    SetActiveToHands(false);
                }
            }
        }
    }
    void Update()
    {
        if (itemChestOpen.activeSelf) {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        } else {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }  

}
