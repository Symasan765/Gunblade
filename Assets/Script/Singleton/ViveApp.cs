using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class ViveApp : MonoBehaviour
{
	public bool m_ViveConnectionStatus = false;
	public SteamVR_ControllerManager manager;

	// Use this for initialization
	void Start()
	{
		// Init();
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
	}
}
