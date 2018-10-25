using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HundGun_VRController : MonoBehaviour {

    public SteamVR_ControllerManager manager;
    public GameObject GunObject;
	public GameObject m_RazerPrefab;

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
		
		if(rightDevice.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
		{
			GunObject.GetComponent<GunState>().EjectMagazine();
		}
		if (ViveCtrl.Get.Press(ViveCtrl.ViveDeviceType.RightHand,ViveCtrl.ViveKey.Trigger))
		{
			GameObject obj = Instantiate(m_RazerPrefab);
			obj.GetComponent<LaserScript>().Firing(GunObject.GetComponent<GunState>().BulletCorePoint);
		}

		if(GunObject.GetComponent<GunState>().bShot)
		{
			rightDevice.TriggerHapticPulse(1500);
			
		}

	}
}
