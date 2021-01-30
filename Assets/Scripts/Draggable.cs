using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging;

    private GameController gameController;
    [SerializeField]
    public string objectType;

    public void OnMouseDown() {
        isDragging = true;
    }

    public void OnMouseUp() {
        isDragging = false;
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Container" && !isDragging) {
            gameController.setStatusText("Valitsit: " + objectType);
        }
    }

    void Awake() {
        gameController = GameObject.Find("/GameController").GetComponent<GameController>();
    }
    void Update()
    {
        if (isDragging) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
