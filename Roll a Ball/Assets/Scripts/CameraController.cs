using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        //Set a vector pointing at the ball
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //Update every frame to have the camera keep offset vector towards player object
        transform.position = player.transform.position + offset;
    }
}
