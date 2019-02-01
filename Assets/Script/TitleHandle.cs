﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleHandle : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    // コントローラと当たっているオブジェクト
    private GameObject collidingObject;
    // 手に持っているオブジェクト
    private GameObject objectInHand;

    public GameObject ScenetransScript;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        // 常にプレイヤーが手に物を持っている　または
        // 当たっているオブジェクトはrigidbodyがない場合何もしない
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 掴むことが可能なオブジェクトとして取得する
        collidingObject = col.gameObject;
    }



    // =========================
    /// OnTrigger系
    // =========================

    // コントローラがColliderがついているオブジェクトと当たると
    // そのオブジェクトを掴めるようにする。
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    // 上と同じ。バグ予防
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }
    // オブジェクトと離れていて掴むことが可能なものの参照を消す
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }

    // ========================================
    // オブジェクトをコントローラにつける関数
    // ========================================
    private void GrabObject()
    {
        // 掴むことが可能なオブジェクトを手に持っている変数にコピーして
        // collidingObjectの参照を消す
        objectInHand = collidingObject;
        collidingObject = null;
        // オブジェクトをコントローラにつけるためにジョイントを作り
        // オブジェクトのRigidbodyをジョイントにコピーする
        //var joint = AddFixedJoint();
        //joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

    }
    // ジョイントを生成する
    // 簡単に外せないようにbreakForceとbreakTorqueを高い位置にする。
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakForce = 20000;
        return fx;
    }

    // ========================================
    // 手に持っているオブジェクトを放す関数
    // ========================================
    private void ReleaseObject()
    {

        // 手に持っているオブジェクトへの参照をnullにする
        objectInHand = null;

    }

    // ========================================
    // Update()で入力管理
    // ========================================
    private void Update()
    {
        // Triggerを押されたらコントローラと当たっているオブジェクト
        // (Rigidbody)があればオブジェクトを握る関数を呼び出す
        //if (Controller.GetHairTriggerDown())
        //{

        //    //ScenetransScript.GetComponent< SceneTrans >().TitleToGame();
        //}

    }
}

