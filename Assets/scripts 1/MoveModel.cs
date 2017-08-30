using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveModel : MonoBehaviour {

   // public Transform target;
    bool collided = false;
    bool isattatched = false;
    public Transform startMarker;
    public Transform endMarker;
    public float speed;
    private float startTime;
    private float journeyLength;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "AT target")
        {
            collided = true;
          
        }
    }
    // Update is called once per frame
    void Update () {
      
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            print("moving");
            collided = true;
            //isattatched = true;
        }

        if (collided == true && isattatched == false)
        {
            /*
            float step = speed*Time.deltaTime;
           
          
            print("Swapping Parents..");
            GameObject.Find("Letter_Ss").GetComponent<Transform>().position = Vector3.MoveTowards(transform.position, target.position, step);
            collided = false;*/

            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / (journeyLength+2.5f);
            speed = speed + .005f;

            GameObject.Find("Shell").transform.position = Vector3.Lerp(startMarker.position, endMarker.position, speed);
           GameObject.Find("Shell").GetComponent<Transform>().SetParent(GameObject.Find("AT target").transform);
            GameObject.Find("Shell").transform.rotation = GameObject.Find("Letter_T").transform.rotation;
           

        }
        
        }
}

