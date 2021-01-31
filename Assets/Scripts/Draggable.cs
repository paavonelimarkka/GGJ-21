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
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "ItemContainer"  && !isCarried) {
            isDragging = false;
            isCarried = true;
            RatInteract ratInteract = rat.GetComponent<RatInteract>();
            ratInteract.activeItem = this.gameObject;
            ratInteract.itemChestOpen.SetActive(false);
            if (ratInteract.activeItem) {
                ratInteract.SetActiveToHands(true);
            }
        }
        
        Debug.Log(col);
        
    }
        
}
