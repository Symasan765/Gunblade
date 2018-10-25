using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class ViveApp : SingletonMonoBehaviour<ViveApp>
{
	public bool m_ViveConnectionStatus = false;

	// Use this for initialization
	void Start()
	{
		//もしマネージャーシーン以外でカメラリグが作成されていたら消して早期return
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			Debug.Log("マネージャーシーン以外(" + SceneManager.GetActiveScene().name + ")にCameraRigが設置されていたので消しました。");
			//Destroy(gameObject);
			return;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Init()
	{
		// ここから初期化処理
		var vrType = XRDevice.model;
		Debug.Log("XRDevice.model = " + vrType);

		// VRが接続されている
		if (vrType != null)
		{
			m_ViveConnectionStatus = true;
		}

		ViveCtrl.Get.Init(gameObject.GetComponent<SteamVR_ControllerManager>());
	}
}
