using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLightScript : MonoBehaviour {

	GameObject[] m_HandObj = new GameObject[2];
	public GameObject m_LightPrefab;

	// Use this for initialization
	void Start () {
		m_HandObj[0] = GameObject.Find("LeftHand");
		m_HandObj[1] = GameObject.Find("RightHand");
	}
	
	// Update is called once per frame
	void Update () {
		var val = ViveInput.GetTriggerValue(HandRole.LeftHand);

		if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.HairTrigger))
		{
			GameObject light = Instantiate(m_LightPrefab);
			light.transform.position = m_HandObj[0].transform.position;
			light.GetComponent<Rigidbody>().AddForce(m_HandObj[0].transform.forward.normalized * 10.0f, ForceMode.Impulse);
		}
	}
}
