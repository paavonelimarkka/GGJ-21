using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class ChangeScene : MonoBehaviour
{
    public string LevelToLoad = "";

    private void OnMouseDown() {
        SceneManager.LoadScene(LevelToLoad);
    }

}
