using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {
	public float m_PushInTime = 1.0f;
	public GameObject m_LightningPrefab;
	public GameObject m_FirePrefab;

	public SteamVR_ControllerManager manager;

	GameObject m_GunObj;
	GameObject m_SwordObj;
	float m_TimeCnt;

	enum WeaponName
	{
		Gun,
		Sword
	}
	WeaponName m_NowWeapon;


	// Use this for initialization
	void Start () {
		m_TimeCnt = 0.0f;
		m_NowWeapon = WeaponName.Gun;

		m_GunObj = transform.Find("Handgun_Body").gameObject;
		if (m_GunObj == null)
			Debug.Log("ガンオブジェクトがおらんぞ");

		m_SwordObj = transform.Find("greatsword_of_fn").gameObject;
		if (m_SwordObj == null)
			Debug.Log("Swordオブジェクトがおらんぞ");
		m_SwordObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		WeaponUpdate();
	}

	void WeaponUpdate()
	{
		SteamVR_TrackedObject trackedObj = manager.right.GetComponent<SteamVR_TrackedObject>();
		SteamVR_Controller.Device rightDevice = SteamVR_Controller.Input((int)trackedObj.index);

		switch (m_NowWeapon)
		{
			case WeaponName.Gun:
				// 入力を受け付けている
				//if (Input.GetMouseButton(0))
				if (rightDevice.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
				{
					m_TimeCnt += Time.deltaTime;

					// 規定秒数を超えたら武器チェンジ
					if (m_TimeCnt > m_PushInTime)
					{
						SwitchWeapon();
						m_TimeCnt = -400000.0f;     // 連続で武器が切り替わることを適当に阻止
					}
				}
				else
				{
					// 離されている
					m_TimeCnt = 0.0f;
				}
				break;
			case WeaponName.Sword:
				// 入力を離した
				//if (!Input.GetMouseButton(0))
				if (rightDevice.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
				{
					SwitchWeapon();
					m_TimeCnt = 0;
				}
					break;
		}
	}

	void SwitchWeapon()
	{
		switch (m_NowWeapon)
		{
			case WeaponName.Gun:
				// 剣に変わる瞬間に雷を発生させる
				GameObject effect = Instantiate(m_LightningPrefab);
				effect.transform.position = m_GunObj.transform.position;
				Destroy(effect, 1.9f);

				m_GunObj.SetActive(false);
				m_SwordObj.SetActive(true);

				m_NowWeapon = WeaponName.Sword;
				break;
			case WeaponName.Sword:
				// ガンに切り替わる瞬間に炎を発生させる
				GameObject fire = Instantiate(m_FirePrefab);
				fire.transform.position = m_SwordObj.transform.position;
				Destroy(fire, 3.0f);
				m_GunObj.SetActive(true);
				m_SwordObj.SetActive(false);
				m_NowWeapon = WeaponName.Gun;
				break;
		}
	}
}
