using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = EndTimer(4.0f);
        StartCoroutine(coroutine);
    }

   IEnumerator EndTimer (float WaitTime) {
       yield return new WaitForSeconds(WaitTime);
       SceneManager.LoadScene("MainMenu");
   }

}
