using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        // this object was clicked - do something
        print("I have been clicked!");
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
      
    }
}
