using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Transform is set to multiply the vector rotation by 2 times the frame rate.
        transform.Rotate(new Vector3(15, 30, 45) * 2 * Time.deltaTime);
    }
}
