﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HundGun_VRController : MonoBehaviour {

    public SteamVR_ControllerManager manager;
    public GameObject GunObject;
	public GameObject m_RazerPrefab;
	MainPlayer m_Player;
    //
    private float stackTime;

	// Use this for initialization
	void Start () {
        stackTime = 0;
		m_Player = transform.root.GetComponent<MainPlayer>();
		Debug.Log(m_Player);
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
		if (ViveCtrl.Get.Trigger(ViveCtrl.ViveDeviceType.RightHand,ViveCtrl.ViveKey.Trigger))
		{
			GameObject obj = Instantiate(m_RazerPrefab);
			obj.GetComponent<LaserScript>().Firing(GunObject.GetComponent<GunState>().BulletCorePoint, m_Player,MainPlayer.HandData.Right);
            stackTime = 1/*second*/ * 60/*frame*/;

        }

        if(stackTime > 0)
        {
            rightDevice.TriggerHapticPulse(2000);
            --stackTime;
        }


    }
}
