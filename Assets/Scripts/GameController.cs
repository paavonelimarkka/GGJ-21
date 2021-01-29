using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // UI Stuff
    [SerializeField]
    private Text statusText;

    // Draggable gameobjects
    [SerializeField]
    private List<GameObject> draggables;

    // Container for draggable objects
    [SerializeField]
    private GameObject container;

    void Start()
    {
        setStatusText("Status");
    }
    public void setStatusText(string text) {
        statusText.GetComponent<Text>().text = text;
    }
    void Update()
    {

    }
}
