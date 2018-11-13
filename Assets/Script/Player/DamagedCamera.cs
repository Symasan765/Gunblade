using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class DamagedCamera : MonoBehaviour {

	public float m_DamageProductionSec = 1.0f;
	float m_TimeCnt = 0.0f;

	PostProcessingBehaviour m_CameraPost;

	public GameObject m_BloodPrefab;

	// Use this for initialization
	void Start () {
		m_CameraPost = GetComponent<PostProcessingBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_TimeCnt > 0.0f)
		{
			var Settings = m_CameraPost.profile.colorGrading.settings;
			float t = (m_DamageProductionSec - m_TimeCnt) / m_DamageProductionSec;
			Settings.channelMixer.red = new Vector3(Mathf.Lerp(2.0f, 1.0f, t), 0, 0);
			m_CameraPost.profile.colorGrading.settings = Settings;
			m_TimeCnt -= Time.deltaTime;

			if (m_TimeCnt <= 0.0f)
			{
				Settings.channelMixer.red = new Vector3(1.0f, 0, 0);
				m_CameraPost.profile.colorGrading.settings = Settings;
				m_TimeCnt = 0.0f;
			}
		}
	}

	public void Damage()
	{
		m_TimeCnt = m_DamageProductionSec;
		GameObject obj = (GameObject)Instantiate(m_BloodPrefab, transform.position, transform.rotation);
		Destroy(obj, m_TimeCnt * 3.0f);
	}
}
