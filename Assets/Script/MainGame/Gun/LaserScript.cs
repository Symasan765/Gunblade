using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

	float m_Force = 500.0f;
	float m_TimeCnt;
	public GameObject m_EffectPrefab;
	GameObject m_Effect;
	
	// Update is called once per frame
	void Update () {
		m_TimeCnt += Time.deltaTime;
		if (m_TimeCnt > 1.5f)
		{
			Destroy(m_Effect);
			Destroy(gameObject);
		}
	}

	public void Firing(GameObject gunTransform)
	{
		transform.localRotation = gunTransform.transform.localRotation;
		transform.position = gunTransform.transform.position;
		m_Effect = Instantiate(m_EffectPrefab);
		m_Effect.transform.localRotation = gunTransform.transform.localRotation;
		m_Effect.transform.position = gunTransform.transform.position;

		m_TimeCnt = 0.0f;
		GetComponent<Rigidbody>().AddForce(transform.forward * m_Force);
	}
}