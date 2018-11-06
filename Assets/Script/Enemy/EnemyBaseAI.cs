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
    public GameObject PlayerObject;
    public float rotationSpeed = 1.0f;
    public float rotationStep = 10.0f;
    public Transform to;

    //
    protected CharacterController characterController;
    protected Animator animator;

    //

	// Use this for initialization
	void Start () {
        characterController = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        to = this.transform;
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDirection = Vector3.zero;
        if(characterController.isGrounded)
        {
            Vector3 vPlayer = PlayerObject.transform.position;
            vPlayer.y = 0;
            // this.transform.LookAt(vPlayer);
            to.LookAt(vPlayer);

            float step = rotationSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.RotateTowards(this.transform.rotation, to.rotation, rotationStep);
            this.transform.rotation = rotation;
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
