using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour
{

    public static int fixedCount = 0;
    Text fixedDisplay;

    // Start is called before the first frame update
    void Start()
    {
        fixedDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        fixedDisplay.text = fixedCount + " / 7 Total Robots Fixed!";
    }
}
