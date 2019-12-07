using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    //Start up method
    void Start()
    {
        //set up a rigidbody for player object
        rb = GetComponent<Rigidbody>();
        
        //initalize count at game start
        count = 0;
        SetCountText();
        winText.text = "";
    }

    //Updates run at each frame
    void FixedUpdate()
    {
        //movement axes for horizontal and vertical movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //add physics for player movement using vector3 and game determined speed
        rb.AddForce(movement * speed);
    }

    //Method for collision trigger between player gameObject and collectible gameObject
    private void OnTriggerEnter(Collider other)
    {
        //Check to see if the collision is with Pick Up object
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            //rather than destroying the object, deactivate it
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    //Method to set the UI text
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 11)
        {
            winText.text = "You Win!";
        }
    }
}
