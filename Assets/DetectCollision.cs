using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour {
    public static bool activated = false;
	// Use this for initialization
	void Start () {
		


	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Target")
        {
            activated = true;
            print("collided!");
            //print(activated);
        }
    }

        // Update is called once per frame
        void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //print(toggle);
            
               activated = true;
            
         
        }



    }
}
