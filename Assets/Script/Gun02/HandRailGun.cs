using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRailGun : MonoBehaviour {

    //
    [Header("Prefabs")]
    public GameObject BulletObject;
    public ParticleSystem[] OnceParticle;
    [Header("GuidPivots")]
    public Transform MuzzlePoint;

    //
    private AudioSource audioSource;


	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            OneShot();
        }
	}

    public void OneShot()
    {
        // Do Effect
        for(int i=0;i<OnceParticle.Length;i++)
        {
            OnceParticle[i].Play();
        }

        // Do Shot
        if(BulletObject)
        {
            var go = GameObject.Instantiate(BulletObject, MuzzlePoint.transform);
        }

        audioSource.PlayOneShot(audioSource.clip);
    }
}
