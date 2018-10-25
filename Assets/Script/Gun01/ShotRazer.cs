using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRazer : MonoBehaviour {

	public GameObject m_RazerPrefab;

	float timeCnt;

	// Use this for initialization
	void Start () {
		timeCnt = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeCnt += Time.deltaTime;
		if (timeCnt / 0.3f >= 1.0f)
		{
			GameObject obj = Instantiate(m_RazerPrefab);
			//obj.GetComponent<LaserScript>().Firing(gameObject);
			timeCnt = 0.0f;
		}
	}
}
