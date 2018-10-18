using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController : MonoBehaviour {

    // 追跡するオブジェクトの参照を持つ変数。この場合はコントローラ
    private SteamVR_TrackedObject trackedObject;

    //簡単にコントローラの情報へアクセスできるための取得関数
    //indedxの値を使って追跡されているオブジェクトを指定できる
    private SteamVR_Controller.Device CtlDevice {
        get { return SteamVR_Controller.Input((int)trackedObject.index); }
    }

    private void Awake()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();    
    }
	
	// Update is called once per frame
	void Update () {
        // Touchpadでの指の位置を出力する
        if (CtlDevice.GetAxis() != Vector2.zero) {
            Debug.Log(gameObject.name + CtlDevice.GetAxis());
        }
	}
}
