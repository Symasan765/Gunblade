using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : MonoBehaviour
{

    //
    public GameObject TriggerModel;
    public GameObject SliderModel;
    public float TriggerWeight;
    public float SliderWeight;
    public AnimationCurve SliderMotion;

    public GameObject MagazineObject;
    public GameObject MagazinePoint;
    public GameObject BulletShell;
    public GameObject BulletPoint;
    public GameObject BulletCoreObject;
    public float ShellEjectPower = 1.0f;

    public bool bShot = false;


    //
    private Vector3 TriggerInit;
    private Vector3 SliderInit;
    private float SliderMotionTime = 0.0f;
    private float SliderMotionTimeMultiple = 10.0f;
    private int iBulletLeft = 0;
    private bool bMagazineExist;
    private bool bShellEject;

    // Use this for initialization
    void Start()
    {
        TriggerInit = TriggerModel.transform.localPosition;
        SliderInit = SliderModel.transform.localPosition;
        iBulletLeft = MagazineObject.GetComponent<Magazine>().GetBulletNum();
        bMagazineExist = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(iBulletLeft == 0)
        {
            bShot = true;
        }
        if (bShot)
        {
            if (iBulletLeft != 0)
            {
                if (SliderMotionTime == 0)
                {
                    iBulletLeft = Mathf.Max(0, iBulletLeft - 1);    // 残弾消費
                    bShellEject = false;
                }
                SliderMotionTime = Mathf.Min(1, SliderMotionTime + Time.deltaTime * SliderMotionTimeMultiple);
                SliderWeight = SliderMotion.Evaluate(SliderMotionTime);
                if (SliderMotionTime == 1)
                {
                    bShot = false;
                    SliderMotionTime = 0.0f;
                }
                if (iBulletLeft == 0)
                {
                    SliderWeight = 1.0f;
                    SliderMotionTime = 0.0f;
                }
                if(SliderMotionTime > 0.5f && !bShellEject)
                {
                    EjectBulletShell();
                    bShellEject = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            EjectMagazine();
        }
        TriggerUpdate();
        SliderUpdate();
    }

    private void TriggerUpdate()
    {
        TriggerWeight = Mathf.Clamp01(TriggerWeight);
        Vector3 newTrig = TriggerInit;
        newTrig.x += TriggerWeight * 0.0007f;
        TriggerModel.transform.localPosition = newTrig;
    }

    private void SliderUpdate()
    {
        SliderWeight = Mathf.Clamp01(SliderWeight);
        Vector3 newSlider = SliderInit;
        newSlider.x += SliderWeight * 0.00249f;
        SliderModel.transform.localPosition = newSlider;
    }

    public void EjectMagazine()
    {
        if (this.MagazineObject == null) return;
        this.MagazineObject.GetComponent<Magazine>().ToEmpty();
        this.MagazineObject = null;
        bMagazineExist = false;
        iBulletLeft = 0;
    }

    private void EjectBulletShell()
    {
        var go = Instantiate(BulletShell, BulletPoint.transform);
        go.transform.localPosition = Vector3.zero;
        Transform addVec = go.transform;
        addVec.Rotate(Vector3.forward, 300);

        go.GetComponent<Rigidbody>().AddForce(addVec.transform.up * ShellEjectPower, ForceMode.Impulse);
        Destroy(go.gameObject, 5.0f);
    }


    /// <summary>
    /// 当たり判定～～～
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // マガジンの装填
        if (!bMagazineExist)
        {
            if (other.gameObject.tag == "Magazine01")
            {
                if (other.GetComponent<Magazine>().IsEmpty()) return;
                other.transform.SetParent(MagazinePoint.transform);
                other.transform.localPosition = Vector3.zero;
                MagazineObject = other.gameObject;
                bMagazineExist = true;
                iBulletLeft = MagazineObject.GetComponent<Magazine>().GetBulletNum();

                SliderWeight = 0.0f;
                bShot = false;
            }
        }
    }
}
