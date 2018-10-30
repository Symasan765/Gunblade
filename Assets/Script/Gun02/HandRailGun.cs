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
    private bool bIsShot = false;


    // Use this for initialization
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        bIsShot = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(ViveCtrl.Get.Trigger(ViveCtrl.ViveDeviceType.LeftHand, ViveCtrl.ViveKey.Trigger))
        {
            if (!bIsShot)
            {
                bIsShot = true;
                OneShot();
            }
        }
        if( ViveCtrl.Get.AnalogValu(ViveCtrl.ViveDeviceType.LeftHand, ViveCtrl.ViveAnalog.Trigger).x == 0)
        {
            bIsShot = false;
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
            var go = GameObject.Instantiate(BulletObject);
            go.transform.position = MuzzlePoint.transform.position;
            go.transform.rotation = MuzzlePoint.transform.rotation;
        }

        audioSource.PlayOneShot(audioSource.clip);
    }
}
