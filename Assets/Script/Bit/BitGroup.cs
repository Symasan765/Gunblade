using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitGroup : MonoBehaviour
{

    //
    public GameObject[] BitObjects;
    public GameObject HeadDevice;
    public Transform Anchor;
    public float AimMaxDistance;
    public AnimationCurve RangeCurve;
    public float BitMoveSpeed;
    public float RaySphereSize;

    //
    private Vector3 v3Position;
    private Vector3 v3Target;
    private float   MoveWeight;


    // Use this for initialization
    void Start()
    {
        MoveWeight = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        SetBitPoint();
        LookTargetByBits();
    }

    void OnDrawGizmos()
    {
        
        Vector3 resV3 = HeadDevice.transform.position + HeadDevice.transform.forward * AimMaxDistance;
        RaycastHit hit;

        var radius = transform.lossyScale.x * RaySphereSize;
        int layerMask = LayerMask.GetMask( new string[] { "Enemy", "HitObj" } );

        var isHit = Physics.SphereCast(HeadDevice.transform.position, radius, HeadDevice.transform.forward, out hit, AimMaxDistance, layerMask);
       // var isHit = Physics.Raycast(HeadDevice.transform.position, HeadDevice.transform.forward, out hit, AimMaxDistance, layerMask);
        if ( isHit )
        {
            Debug.Log("1");
            Gizmos.DrawRay(HeadDevice.transform.position, HeadDevice.transform.forward * hit.distance);
            Gizmos.DrawWireSphere(HeadDevice.transform.position + HeadDevice.transform.forward * (hit.distance), radius);

            resV3 = hit.point;
        }
        else
        {
            Debug.Log("2");
            Gizmos.DrawRay(HeadDevice.transform.position, HeadDevice.transform.forward * 100);
            resV3 = HeadDevice.transform.position + HeadDevice.transform.forward * AimMaxDistance;
        }

        v3Target = resV3;
    }

    void SetBitPoint()
    {
        Vector3 resV3 = Vector3.zero;
        Anchor.rotation = Quaternion.identity;


        for ( int i = 0; i < BitObjects.Length; i++ )
        {
            // if (BitObjects[i].GetComponentInChildren<Bit>().IsShooting()) continue;


            MoveWeight = BitObjects[i].GetComponentInChildren<Bit>().GetBitWeight(); //Time.deltaTime * BitMoveSpeed / 100.0f;
            //if (MoveWeight > 1) MoveWeight -= 1.0f;
            //if (MoveWeight < 0) MoveWeight += 1.0f;
            float rot = 70.0f * RangeCurve.Evaluate(MoveWeight) + 110.0f;


            Anchor.Rotate(Anchor.forward, rot);
            Anchor.Rotate(Anchor.up     , rot);
            resV3 = Anchor.up * 4;

            BitObjects[i].transform.localPosition = resV3;
        }
    }

    void LookTargetByBits()
    {
        for ( int i = 0; i < BitObjects.Length; i++ )
        {
            if (BitObjects[i].GetComponentInChildren<Bit>().IsShooting()) continue;
            BitObjects[i].transform.LookAt(v3Target);
        }
    }

    public void BitShot(int index)
    {
        BitObjects[index].GetComponentInChildren<Bit>().Shot();
    }
}
