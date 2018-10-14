using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HundGun_VRController : MonoBehaviour {

    public HandRole UsingController = HandRole.LeftHand;
    public GameObject GunObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GunObject.GetComponent<GunState>().SetTriggerWeight(ViveInput.GetTriggerValue(UsingController));        
	}
}
