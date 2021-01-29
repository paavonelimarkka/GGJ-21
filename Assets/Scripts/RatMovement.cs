using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    public float speed = 12.5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float hor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float ver = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(hor,ver,0);
    }
}
