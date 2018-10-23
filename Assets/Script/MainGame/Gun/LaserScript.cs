using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

	float m_Force = 1000.0f;
	float m_TimeCnt;
	public GameObject m_EffectPrefab;
	GameObject m_Effect;
	public GameObject m_ExpPrefab;
	public GameObject m_AudioPrefab;
	GameObject[] m_Exp;
	GameObject m_GunSE;

	const int m_MaxExpNum = 10;
	int m_Num;
	// Update is called once per frame
	void Update()
	{
		m_TimeCnt += Time.deltaTime;
		if (m_TimeCnt > 1.5f)
		{
			Destroy(gameObject);
		}
		if (m_TimeCnt > 0.4f)
		{
			Destroy(m_Effect);
		}
	}

	public void Firing(GameObject gunTransform)
	{
		//transform.localRotation = gunTransform.transform.localRotation;
		
		m_Effect = Instantiate(m_EffectPrefab);
		m_Effect.transform.localRotation = gunTransform.transform.localRotation;
		m_Effect.transform.position = gunTransform.transform.position;
		m_Num = 0;
		m_GunSE = Instantiate(m_AudioPrefab);
		m_GunSE.GetComponent<AudioSource>().Play();
		Destroy(m_GunSE, 1.0f);

		m_TimeCnt = 0.0f;
		GetComponent<Rigidbody>().AddForce(gunTransform.transform.forward * m_Force);
		this.transform.LookAt(gunTransform.transform.forward);
		m_Exp = new GameObject[m_MaxExpNum];
		transform.position = gunTransform.transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (m_Num < m_MaxExpNum)
		{
			m_Exp[m_Num] = Instantiate(m_ExpPrefab);
			m_Exp[m_Num].transform.position = other.gameObject.transform.position;
			Destroy(m_Exp[m_Num], 1.5f);
			m_Num++;
		}
	}
}