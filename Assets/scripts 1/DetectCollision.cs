using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetectCollision : MonoBehaviour {
    public static bool activated = false;
    public GameObject letterS;
    public GameObject slt;
    public GameObject shell;
    public static bool gotright = false;
    float begintime = 0;
    float endtime = 0;
    public static bool wedidit;
    string target1 = "sat";
  
    Animator otherAnimator;
    // Use this for initialization
    void Start () {
        letterS = GameObject.Find("Letter_Ss");
        slt = GameObject.Find("Slot");
        shell = GameObject.Find("Shell");
        otherAnimator = letterS.GetComponent<Animator>();
        wedidit = false;
        
        //2048 x 1536
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Target" && activated == false)
        {
            activated = true;
            print("collided!");
          //  print(activated);
            begintime = Time.time;
            
           // print("Time of recording:" + Time.time);
            otherAnimator.SetBool("IsJumping", true);
            
            //print(activated);
        }
    }

        // Update is called once per frame
        void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //print(toggle);
            
               activated = true;
            print("Time of recording:" + Time.time);

        }
        if (shell.transform.position == slt.transform.position)
        {
            otherAnimator.SetBool("IsJumping", false);
      
        }
 


    }
}
