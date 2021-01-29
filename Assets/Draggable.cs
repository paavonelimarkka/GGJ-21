using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging;

    [SerializeField]
    private GameObject container;

    private GameController gameController;
    [SerializeField]
    public string objectType;

    public void OnMouseDown() {
        Debug.Log("Plölölöl");
        isDragging = true;
    }

    public void OnMouseUp() {
        isDragging = false;
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Container" && !isDragging) {
            gameController.setStatusText("Valitsit: " + objectType);
            Destroy(GetComponent<GameObject>());
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
