using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {
	GameObject m_Root;
	GameObject m_Tip;

	bool m_ThrustFlag;

	Vector3[] m_RootPos = new Vector3[2];
	Vector3[] m_TipPos = new Vector3[2];

	public GameObject[] m_Effects;

	float m_ThrustTimeCnt;
	public float m_ThrustInterval = 0.15f;

	int m_ThrustNum;        // 突き回数

	public float m_RandRange = 0.3f;

	// Use this for initialization
	void Start () {
		m_ThrustTimeCnt = 0;
		m_ThrustNum = 0;
		m_ThrustFlag = false;
		m_Root = transform.Find("SwordRoot").gameObject;
		m_Tip = transform.Find("SwordTip").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		ThrustUpdate();
	}

	void ThrustUpdate()
	{
		m_ThrustFlag = false;

		m_RootPos[1] = m_RootPos[0];
		m_TipPos[1] = m_TipPos[0];

		m_TipPos[0] = m_Tip.transform.position;
		m_RootPos[0] = m_Root.transform.position;

		Vector3 TipVelocity = m_TipPos[1] - m_TipPos[0];
		Vector3 RootVelocity = m_RootPos[1] - m_RootPos[0];

		// 先端を大きく動かしていて根本を動かしていなければ突きフラグを建てておく
		if(TipVelocity.magnitude > 0.5f)
		{
			if(RootVelocity.magnitude < 0.05f)
			{
				m_ThrustFlag = true;
			}
		}

		m_ThrustTimeCnt += Time.deltaTime;
	}

	private void OnTriggerStay(Collider collider)
	{
        Debug.Log("hello");
		// 3D同士が接触している間、常に呼び出される処理
		if (m_ThrustFlag)
		{
            Debug.Log("world");
            Thrust(collider.gameObject);
		}
	}

	void Thrust(GameObject hitObj)
	{
		if(m_ThrustTimeCnt > m_ThrustInterval)
		{
			int idx = m_ThrustNum % m_Effects.Length;
			Vector3 dir = gameObject.transform.position - hitObj.transform.position;
			GameObject m_effect = Instantiate(m_Effects[idx]);
			m_effect.transform.position = hitObj.transform.position + new Vector3(Random.Range(-m_RandRange, m_RandRange), Random.Range(-m_RandRange, m_RandRange), Random.Range(-m_RandRange, m_RandRange));

			m_effect.transform.position += dir.normalized * 0.5f;	// キャラ側にエフェクトを近づける

			Destroy(m_effect, 8.0f);

			m_ThrustTimeCnt = 0.0f;
			m_ThrustNum++;
		}
	}
}
