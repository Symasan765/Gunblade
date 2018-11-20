using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitControll : MonoBehaviour {



    public ViveCtrl.ViveDeviceType UsingControllerSide = ViveCtrl.ViveDeviceType.LeftHand;
    private Vector3 MoveTarget;
    public float BitGroupHeight;
    private int ShotBitNumber = 0;
    private BitGroup BitGroupComponent;
    public Transform HeadDevice;


    // Use this for initialization
    void Start () {
        BitGroupComponent = this.GetComponent<BitGroup>();
	}
	
	// Update is called once per frame
	void Update () {
        BitMoveControll();
        BitShotControll();
    }

    void BitMoveControll()
    {
        Vector2 xz = ViveCtrl.Get.AnalogValu(UsingControllerSide, ViveCtrl.ViveAnalog.Touchpad) * 10;
        MoveTarget = new Vector3(xz.x, 0, xz.y);// HeadDevice.forward;
        this.transform.localPosition = MoveTarget;//+= Vector3.Normalize(this.transform.position - MoveTarget ) * (Vector3.Distance(this.transform.position, MoveTarget) / 100000.0f );

        //this.transform.localPosition = new Vector3();
    }

    void BitShotControll()
    {
        if(ViveCtrl.Get.Trigger(UsingControllerSide, ViveCtrl.ViveKey.TouchpadClick))
        {
            
            BitGroupComponent.BitShot(ShotBitNumber);
            ShotBitNumber += 1;
            if(ShotBitNumber > 9)
            {
                ShotBitNumber = 0;
            }
        }

        Debug.Log("tetet");
    }
}
