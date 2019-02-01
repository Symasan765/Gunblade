using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGrabMotioner : MonoBehaviour
{

    public GameObject GrabHand;
    public GameObject FreeHand;
    public bool Grabbing;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GrabSwitch();   
    }

    void GrabSwitch()
    {
        GrabHand.SetActive( Grabbing );
        FreeHand.SetActive( !Grabbing );
    }
}
