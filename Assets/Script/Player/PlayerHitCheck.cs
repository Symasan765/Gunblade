using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider collider)
	{
		Debug.Log("当たった");
		transform.root.GetComponent<MainPlayer>().IsDamage(2.0f);
	}
}
