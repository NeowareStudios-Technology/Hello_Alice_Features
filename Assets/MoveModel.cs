using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveModel : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "AT target")
        {
           
           GameObject.Find("Letter_Ss").GetComponent<Transform>().SetParent(col.collider.gameObject.transform);
            GameObject.Find("Letter_Ss").GetComponent<Transform>().position = GameObject.Find("Slot").GetComponent < Transform > ().position;
            GameObject.Find("Letter_Ss").transform.rotation = GameObject.Find("Letter_T").transform.rotation;
            print("Swapping Parents..");
            //print(activated);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
