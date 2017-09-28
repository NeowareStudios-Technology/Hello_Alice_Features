using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnableDisable : MonoBehaviour
{
    bool connected = DetectCollision.gotright;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (connected == true)
        {
           // gameObject.SetActive(true);

        }
    }
}
