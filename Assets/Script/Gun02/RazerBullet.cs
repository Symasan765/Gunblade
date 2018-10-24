﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerBullet : MonoBehaviour {

    //
    [Header("Parameter")]
    public float Speed = 1.0f;
    public float TrailOffset;
    public float LifeTime = 2.0f;
    public AnimationCurve TrailScale;

    [Header("Object")]
    public GameObject TrailCylinderObject;
    public GameObject LightningStartPointObject;


    //
    private Vector3 CurrentScale = Vector3.zero;
    private Vector3 SpawnPoint = Vector3.zero;
    private float LeftLifeTime = 0.0f;

	// Use this for initialization
	void Start () {
        SpawnPoint = this.transform.position + (this.transform.forward * TrailOffset);
        LeftLifeTime = LifeTime;
        Destroy(this.gameObject, LifeTime);
	}
	
	// Update is called once per frame
	void Update () {
        {
            var accel = this.transform.forward * Speed;
            this.transform.position += accel;
        }
        {
            LightningStartPointObject.transform.position = SpawnPoint;

            float _trailLength = Vector3.Distance(this.transform.position ,SpawnPoint);
            LeftLifeTime -= Time.deltaTime;
            float _getTime = 1.0f - (LeftLifeTime / LifeTime);
            _getTime = Mathf.Clamp01(_getTime + Time.deltaTime);
            Vector3 _trailScale = new Vector3(TrailScale.Evaluate(_getTime), _trailLength * 5, TrailScale.Evaluate(_getTime));

            TrailCylinderObject.transform.position = (this.transform.position + SpawnPoint) / 2.0f;
            TrailCylinderObject.transform.localScale = _trailScale;
        }

    }
}