using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging;
    public string itemType;
    public GameObject rat;

    private GameObject current;

    private bool isCarried = false;

    private void OnMouseDown() {
        Debug.Log("Draggg");
        isCarried = false;
        isDragging = true;
        current = this.gameObject;
    }

    private void OnMouseUp() {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
    
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "ItemContainer"  && !isCarried) {
            isDragging = false;
            isCarried = true;
            Debug.Log(current.GetComponent<Draggable>().itemType + " has been taken from the chest");
            RatInteract ratInteract = rat.GetComponent<RatInteract>();
            ratInteract.activeItem = current;
            ratInteract.itemChestOpen.SetActive(false);
            ratInteract.SetActiveToHands(true);
            // current.SetActive(false);
        }
        
        Debug.Log(col);
        
    }
        
}
