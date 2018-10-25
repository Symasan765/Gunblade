using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViveCtrl : SingletonMonoBehaviour<ViveCtrl>
{
	GameObject originObj;
	SteamVR_ControllerManager manager;
	SteamVR_TrackedObject rightTrackedObj;
	SteamVR_TrackedObject leftTrackedObj;

	SteamVR_Controller.Device rightDevice;
	SteamVR_Controller.Device leftDevice;

	bool m_ConnectFlag;
	bool m_SceneFlag = false;
	bool m_GameInitFlag = false;

	public enum ViveDeviceType
	{
		RightHand = 0,
		LeftHand = 1
	}

	public enum ViveKey
	{
		Trigger = 0,
		Touchpad = 1,
		TouchpadClick = 2,
		ApplicationMenu = 3,
		Grip = 4,
		MAX_NUM = 5
	}

	public enum ViveInputCode
	{
		k_System = 0,
		k_ApplicationMenu = 1,
		k_Grip = 2,
		k_DPad_Left = 3,
		k_DPad_Up = 4,
		k_DPad_Right = 5,
		k_DPad_Down = 6,
		k_A = 7,
		k_ProximitySensor = 31,
		k_Axis0 = 32,
		k_Axis1 = 33,
		k_Axis2 = 34,
		k_Axis3 = 35,
		k_Axis4 = 36,
		k_SteamVR_Touchpad = 32,
		k_SteamVR_Trigger = 33,
		k_Dashboard_Back = 2,
		k_Max = 64,
	}

	public enum ViveAnalog
	{
		Trigger = 0,
		Touchpad = 1
	}

	bool[] m_LeftDown = new bool[(int)ViveKey.MAX_NUM];
	bool[] m_LeftPress = new bool[(int)ViveKey.MAX_NUM];
	bool[] m_LeftUp = new bool[(int)ViveKey.MAX_NUM];

	bool[] m_RightDown = new bool[(int)ViveKey.MAX_NUM];
	bool[] m_RightPress = new bool[(int)ViveKey.MAX_NUM];
	bool[] m_RightUp = new bool[(int)ViveKey.MAX_NUM];

	Vector2 m_LeftTouchpad;
	Vector2 m_LeftTrigger;
	Vector2 m_RightTouchpad;
	Vector2 m_RightTrigger;

	public bool Press(ViveDeviceType hand,ViveKey code)
	{
		ViveUpdate();
		switch (hand)
		{
			case ViveDeviceType.LeftHand:
				return m_LeftPress[(int)code];
			case ViveDeviceType.RightHand:
				return m_RightPress[(int)code];
		}

		Debug.Log("何か入力機能がうまくいってないよ");
		return false;
	}

	public bool Trigger(ViveDeviceType hand, ViveKey code)
	{
		ViveUpdate();
		switch (hand)
		{
			case ViveDeviceType.RightHand:
				return m_RightDown[(int)code];
			case ViveDeviceType.LeftHand:
				return m_LeftDown[(int)code];
		}

		Debug.Log("何か入力機能がうまくいってないよ");
		return false;
	}

	public bool Release(ViveDeviceType hand, ViveKey code)
	{
		ViveUpdate();
		switch (hand)
		{
			case ViveDeviceType.LeftHand:
				return m_LeftUp[(int)code];
			case ViveDeviceType.RightHand:
				return m_RightUp[(int)code];
		}

		Debug.Log("何か入力機能がうまくいってないよ");
		return false;
	}

	public Vector2 AnalogValu(ViveDeviceType hand, ViveAnalog code)
	{
		ViveUpdate();
		switch (hand)
		{
			case ViveDeviceType.LeftHand:
				{
					if (code == ViveAnalog.Touchpad)
						return m_LeftTouchpad;
					if (code == ViveAnalog.Trigger)
						return m_LeftTrigger;
				}
				break;
			case ViveDeviceType.RightHand:
				{
					if (code == ViveAnalog.Touchpad)
						return m_RightTouchpad;
					if (code == ViveAnalog.Trigger)
						return m_RightTrigger;
				}
				break;
		}

		Debug.Log("何か入力機能がうまくいってないよ");
		return Vector2.zero;
	}

	// Update is called once per frame
	void Update () {
		m_ConnectFlag = false;
		if(m_GameInitFlag == false)
		{
			SceneManager.activeSceneChanged += OnActiveSceneChanged;
			m_GameInitFlag = true;
		}
	}
	

	public SteamVR_Controller.Device GetDevice(ViveDeviceType ctrlType)
	{
		switch (ctrlType)
		{
			case ViveDeviceType.RightHand:
				return rightDevice;
			case ViveDeviceType.LeftHand:
				return leftDevice;
		}
		return null;
	}

	void ViveUpdate()
	{
		SceneUpdate();      // シーンで初めて呼ばれる場合はここに来る
		if (m_ConnectFlag == false)
		{
			KeyInit();			// そのフレームで初めて呼ばれる場合はここに来る
			LeftHandUpdate();
			RightHandUpdate();
			m_ConnectFlag = true;
		}
	}

		void LeftHandUpdate()
	{
		var device = GetDevice(ViveDeviceType.LeftHand);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			//Debug.Log("トリガーを浅く引いた");
			m_LeftDown[(int)ViveKey.Trigger] = true;
		}
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			m_LeftDown[(int)ViveKey.Trigger] = true;
		}
		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			//Debug.Log("トリガーを離した");
			m_LeftUp[(int)ViveKey.Trigger] = true;
		}
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
		{
			//Debug.Log("トリガーを浅く引いている");
			m_LeftPress[(int)ViveKey.Trigger] = true;
		}
		if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
		{
			//Debug.Log("トリガーを深く引いている");
			m_LeftPress[(int)ViveKey.Trigger] = true;
		}


		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドをクリックした");
			m_LeftDown[(int)ViveKey.TouchpadClick] = true;
		}
		if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドをクリックしている");
			m_LeftPress[(int)ViveKey.TouchpadClick] = true;
		}
		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドをクリックして離した");
			m_LeftUp[(int)ViveKey.TouchpadClick] = true;
		}
		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドに触った");
			m_LeftDown[(int)ViveKey.Touchpad] = true;
			m_LeftPress[(int)ViveKey.Touchpad] = true;
		}
		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドを離した");
			m_LeftUp[(int)ViveKey.Touchpad] = true;
		}
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
		{
			//Debug.Log("メニューボタンをクリックした");
			m_LeftDown[(int)ViveKey.ApplicationMenu] = true;
		}
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log("グリップボタンをクリックした");
			m_LeftDown[(int)ViveKey.Grip] = true;
		}

		// トリガーの値とタッチパッド座標
		m_LeftTrigger = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		m_LeftTouchpad = device.GetAxis();
	}

	void RightHandUpdate()
	{
		var device = GetDevice(ViveDeviceType.RightHand);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			//Debug.Log("トリガーを浅く引いた");
			m_RightDown[(int)ViveKey.Trigger] = true;
		}
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			m_RightDown[(int)ViveKey.Trigger] = true;
		}
		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			//Debug.Log("トリガーを離した");
			m_RightUp[(int)ViveKey.Trigger] = true;
		}
		if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
		{
			//Debug.Log("トリガーを浅く引いている");
			m_RightPress[(int)ViveKey.Trigger] = true;
		}
		if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
		{
			//Debug.Log("トリガーを深く引いている");
			m_RightPress[(int)ViveKey.Trigger] = true;
		}


		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドをクリックした");
			m_RightDown[(int)ViveKey.TouchpadClick] = true;
		}
		if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドをクリックしている");
			m_RightPress[(int)ViveKey.TouchpadClick] = true;
		}
		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドをクリックして離した");
			m_RightUp[(int)ViveKey.TouchpadClick] = true;
		}
		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドに触った");
			m_RightDown[(int)ViveKey.Touchpad] = true;
			m_RightPress[(int)ViveKey.Touchpad] = true;
		}
		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Debug.Log("タッチパッドを離した");
			m_RightUp[(int)ViveKey.Touchpad] = true;
		}
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
		{
			//Debug.Log("メニューボタンをクリックした");
			m_RightDown[(int)ViveKey.ApplicationMenu] = true;
		}
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			//Debug.Log("グリップボタンをクリックした");
			m_RightDown[(int)ViveKey.Grip] = true;
		}

		// トリガーの値とタッチパッド座標
		m_RightTrigger = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
		m_RightTouchpad = device.GetAxis();
	}
	

	void KeyInit()
	{
		for(int i = 0; i < (int)ViveKey.MAX_NUM; i++)
		{
			m_LeftDown[i] = false;
			m_LeftPress[i] = false;
			m_LeftUp[i] = false;

			m_RightDown[i] = false;
			m_RightPress[i] = false;
			m_RightUp[i] = false;
		}
	}

	public SteamVR_Controller.Device UseController(ViveDeviceType _side)
	{
		manager = originObj.GetComponent<SteamVR_ControllerManager>();

		if (_side == ViveDeviceType.RightHand)
			return SteamVR_Controller.Input((int)manager.right.GetComponent<SteamVR_TrackedObject>().index);
		if (_side == ViveDeviceType.LeftHand)
			return SteamVR_Controller.Input((int)manager.left.GetComponent<SteamVR_TrackedObject>().index);
		return null;
	}

	void SceneUpdate()
	{
		if(m_SceneFlag == false)
		{
			originObj = GameObject.Find("[CameraRig]");
			manager = originObj.GetComponent<SteamVR_ControllerManager>();
			rightDevice = SteamVR_Controller.Input((int)manager.right.GetComponent<SteamVR_TrackedObject>().index);
			leftDevice = SteamVR_Controller.Input((int)manager.left.GetComponent<SteamVR_TrackedObject>().index);
			m_SceneFlag = true;
		}
	}


	void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
	{
		m_SceneFlag = false;
	}
}