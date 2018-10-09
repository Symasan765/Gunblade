using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfState : MonoBehaviour {

    public enum EMotionState
    {
        Idle    = 0x01,     // 待機
        Walk    = 0x02,     // 歩き
        Run     = 0x04,     // 走り
        Seat    = 0x08,     // おすわり
        Creep   = 0x16      // しゃがみ歩き
    }
    public EMotionState eMotion = EMotionState.Idle;
    public float fAnimSpeed = 1.0f;

    //
    private Animator animator;
    private bool bSquat;
    private bool bSeat;
    private bool bWalk;
    private bool bRun;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        bSquat =    (eMotion == EMotionState.Creep);
        bSeat =     (eMotion == EMotionState.Seat);
        bWalk =     (eMotion == EMotionState.Walk);
        bRun =      (eMotion == EMotionState.Run);


        SetAnimatorParameter();
    }

    public void SetState(EMotionState state, float animSpeed = 1.0f)
    {
        eMotion = state;
        fAnimSpeed = animSpeed;
    }

    private void SetAnimatorParameter()
    {
        animator.SetBool("Seat", bSeat);
        animator.SetBool("Squat", bSquat);
        animator.SetBool("Walk", bWalk);
        animator.SetBool("Run", bRun);
        animator.speed = fAnimSpeed;
    }





}
