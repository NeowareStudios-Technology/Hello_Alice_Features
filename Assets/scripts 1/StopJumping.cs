using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopJumping : MonoBehaviour {

    Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Slot")
        {

            print("hit");
            anim.SetBool("IsJumping", false);
            //print(activated);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
