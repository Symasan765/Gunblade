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
    public float MoveDelay = 0.0f;
    public float LocalMoveSpeed;
    public float LocalMoveRange;

    public AnimationCurve LocalCurve1;
    public AnimationCurve LocalCurve2;


    public GameObject Bullet;

    //
    private float OpenWeight = 0.0f;
    private bool bIsBitOpen = false;
    private bool bIsBitOpened = false;
    private Transform ImageTransform;
    private float OpenLife = 0.0f;
    private int ShotStack = 0;
    public float LocalMoveWeight;
    public float GlobalMoveWeight;

    // Use this for initialization
    void Start () {
        ImageTransform = MoveAnchor.transform;
        ShotStack = 0;

    }
	
	// Update is called once per frame
	void Update () {
        CalcLocalMoveWeight();

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Shot();
        //}
        BitOpener();
        InstanceBullet();

        MoveLocalFloat();
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

    public void Shot()
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

    public bool IsShooting()
    {
        return bIsBitOpen;
    }

    void CalcLocalMoveWeight()
    {
        if (bIsBitOpen) return;
        LocalMoveWeight += Time.deltaTime * LocalMoveSpeed / 100.0f;
        if (LocalMoveWeight > 1) LocalMoveWeight -= 1.0f;
        if (LocalMoveWeight < 0) LocalMoveWeight += 1.0f;

        GlobalMoveWeight += Time.deltaTime * GlobalMoveWeight / 10000.0f;
        if (GlobalMoveWeight > 1) GlobalMoveWeight -= 1.0f;
        if (GlobalMoveWeight < 0) GlobalMoveWeight += 1.0f;
    }

    public float GetBitWeight()
    {
        return GlobalMoveWeight;
    }

    void MoveLocalFloat()
    {
        float v = LocalCurve1.Evaluate(LocalMoveWeight);
        float h = LocalCurve2.Evaluate(LocalMoveWeight);
        Vector3 newpos;
        newpos.x = LocalMoveRange * h;
        newpos.y = LocalMoveRange * v;
        newpos.z = 0;
        this.transform.localPosition = newpos;
    }
}
