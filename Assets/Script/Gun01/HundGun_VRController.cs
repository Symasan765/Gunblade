using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HundGun_VRController : MonoBehaviour {

    public SteamVR_ControllerManager manager;
    public GameObject GunObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SteamVR_TrackedObject trackedObj = manager.right.GetComponent<SteamVR_TrackedObject>();
        SteamVR_Controller.Device rightDevice = SteamVR_Controller.Input((int)trackedObj.index);
        var value = rightDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
        Debug.Log(value);
        GunObject.GetComponent<GunState>().SetTriggerWeight(value);        
	}
}
