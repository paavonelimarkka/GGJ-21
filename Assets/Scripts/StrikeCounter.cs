using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeCounter : MonoBehaviour
{   
    public GameObject strike1;
    public GameObject strike2;
    public GameObject strike3;
    public int strikeCount = 0;

    void Update()
    {
        // I'm really sorry I made you see this :--(
        if (strikeCount <= 0) {
            strike1.SetActive(false);
            strike2.SetActive(false);
            strike3.SetActive(false);
        }
        if (strikeCount >= 1) {
            strike1.SetActive(true);
        }
        if (strikeCount >= 2) {
            strike2.SetActive(true);
        }
        if (strikeCount == 3) {
            strike3.SetActive(true);
        }
        else if (strikeCount >= 3) {
            strike1.SetActive(false);
            strike2.SetActive(false);
            strike3.SetActive(false);
            Debug.Log("strikeCount over 3, game over or something..");
        }
    }
}
