using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSpeech : MonoBehaviour {
   public static bool toggle = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // print(toggle);
            if (toggle == false)
            {
                toggle = true;
            }
            else
            {
                toggle = false;
            }
        }
}
}
