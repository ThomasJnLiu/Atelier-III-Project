using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject[] Colors;

    GameObject CurrentColor;

    public int ColorNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        CurrentColor = Colors[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input From The Mouse Wheel
        // if mouse wheel gives a positive value add 1 to ColorNumber
        // if it gives a negative value decrease ColorNumber with 1
        
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                ColorNumber = (ColorNumber + 1);
                if (ColorNumber > Colors.Length-1)
                {
                    ColorNumber = 0;
                }
                
                    
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                ColorNumber = (ColorNumber - 1);
                if (ColorNumber < 0)
                {
                    ColorNumber = Colors.Length-1;
                }
            }
        
        CurrentColor = Colors[ColorNumber];
        Debug.Log(Colors[ColorNumber].gameObject.name);
    }
}
