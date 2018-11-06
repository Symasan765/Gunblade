using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyBaseAI : MonoBehaviour {

    // State
    public enum EEnemyState
    {
        E_Idle,
        E_Move,
        E_Attack01
    }

    //
    public EEnemyState state = EEnemyState.E_Idle;
    public float gravity = 10.0f;

    //
    protected CharacterController characterController;
    protected Animator animator;

	// Use this for initialization
	void Start () {
        characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDirection = Vector3.zero;
        if(characterController.isGrounded)
        {

        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
	}

    void StateIdle()
    {
        if (state != EEnemyState.E_Idle) return;
        // animator.SetBool()
    }

    void StateMove()
    {
        if (state != EEnemyState.E_Move) return;
    }




}
