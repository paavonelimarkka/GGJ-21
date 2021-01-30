using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging;
    public GameObject rat;

    public void OnMouseDown() {
        isDragging = true;
    }

    public void OnMouseUp() {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        Debug.Log(gameObject.name + " has been taken from the chest");
        RatInteract kekkala = rat.GetComponent<RatInteract>();
        kekkala.activeItem = gameObject.name;
    }
        
}
