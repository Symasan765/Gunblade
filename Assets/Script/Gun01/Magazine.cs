using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {

    //
    public int iBulletMax = 15;

    //
    private bool bEmpty;
    

	// Use this for initialization
	void Start () {
        bEmpty = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localRotation = Quaternion.identity;
	}

    public int GetBulletNum()
    {
        return iBulletMax;
    }
    
    public void ToEmpty()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.transform.SetParent(null);
        bEmpty = true;
    }

    public bool IsEmpty()
    {
        return bEmpty;
    }
}
