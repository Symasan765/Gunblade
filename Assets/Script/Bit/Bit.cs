using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit : MonoBehaviour {

    //
    public GameObject[] BitObject;
    public GameObject MoveAnchor;
    public float OpenSpeed = 1.0f;
    public float OpenDistance = 30.0f;
    public float AutoCloseTime = 2.0f;

    public GameObject Bullet;

    //
    private float OpenWeight = 0.0f;
    public bool bIsBitOpen = false;
    private bool bIsBitOpened = false;
    private Transform ImageTransform;
    private float OpenLife = 0.0f;
    private int ShotStack = 0;

	// Use this for initialization
	void Start () {
        ImageTransform = MoveAnchor.transform;
        ShotStack = 0;

    }
	
	// Update is called once per frame
	void Update () {


        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
        BitOpener();
        InstanceBullet();
    }

    void BitOpener()
    {
        OpenWeight = Mathf.Clamp01(OpenWeight + (bIsBitOpen ? Time.deltaTime * OpenSpeed : Time.deltaTime * -OpenSpeed));
        if(OpenWeight < 1)
        {
            bIsBitOpened = false;
        }
        else
        {
            bIsBitOpened = true;
        }
        ImageTransform.localRotation = Quaternion.identity;
        ImageTransform.Rotate(Vector3.forward, 45);
        
        BitObject[0].transform.localPosition = ImageTransform.up * OpenDistance * OpenWeight;
        for(int i = 1; i < 4; i ++)
        {
            ImageTransform.Rotate(Vector3.forward, 90);
            BitObject[i].transform.localPosition = ImageTransform.up * OpenDistance * OpenWeight;
        }

        OpenLife = Mathf.Max(OpenLife - Time.deltaTime, 0);
        if(OpenLife == 0)
        {
            bIsBitOpen = false;
        }
        else
        {
            bIsBitOpen = true;
        }
    }

    void Shot()
    {
        OpenLife = AutoCloseTime;
        ShotStack += 1;
        if(!bIsBitOpened)
        {
            ShotStack = Mathf.Min(ShotStack, 1);
        }

    }

    void InstanceBullet()
    {
        if (!bIsBitOpened) return;
        bIsBitOpened = false;

        if (ShotStack > 0)
        {
            ShotStack -= 1;
            var go = Instantiate(Bullet, this.transform.position, this.transform.rotation);
        }
    }
}
