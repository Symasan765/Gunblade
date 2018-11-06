using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplash : MonoBehaviour {

    [Tooltip("ダメージエフェクトオブジェクト")]
    public GameObject DamageEffect;
    [Tooltip("エフェクト持続時間")]
    public float LifeTime = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        // 衝突位置からダメージエフェクトの生成
        Vector3 hitLocation = other.ClosestPointOnBounds(this.transform.position);
        var go = GameObject.Instantiate(DamageEffect, hitLocation, Quaternion.identity);
        Destroy(go.gameObject, LifeTime);
    }
}
