using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss : MonoBehaviour
{
	GameObject m_PlayerObj;
	Animator m_Animotor;

	float PlayerDistance = 4.3f;
	float m_ReturnDistance = 15.0f;
	float m_RotSpeed = 5.0f;

	int m_AttackAnime = 0;

	enum State
	{
		Wait,
		Run,
		Attack,
		Return,
		ReturnMove,
		Damage
	}

	State m_State;
	float m_TimeCnt = 0.0f;

	// Use this for initialization
	void Start()
	{
		m_PlayerObj = GameObject.FindGameObjectWithTag("MainCamera");
		m_State = State.Wait;

		m_Animotor = GetComponent<Animator>();

		m_AttackAnime = Animator.StringToHash("Base Layer.attack01");
	}

	// Update is called once per frame
	void Update()
	{
		AllAnimeFlagInit();

		switch (m_State)
		{
			case State.Wait:
				WaitUpdate();
				break;
			case State.Run:
				RunUpdate();
				break;
			case State.Attack:
				AttackUpdate();
				break;
			case State.Return:
				ReturnUpdate();
				break;
			case State.Damage:
				DamageUpdate();
				break;
			case State.ReturnMove:
				ReturnMoveUpdate();
				break;
		}
	}

	void WaitUpdate()
	{
		m_Animotor.SetBool("idle01", true);
		Quaternion m_InitRot = transform.rotation;
		transform.LookAt(m_PlayerObj.transform);
		
		transform.rotation = Quaternion.RotateTowards(m_InitRot, transform.rotation, m_RotSpeed);

		if(transform.rotation == m_InitRot)
		{
			m_State = State.Run;
		}
	}

	void RunUpdate()
	{
		m_Animotor.SetBool("run", true);

		transform.position += transform.forward.normalized * (Time.deltaTime * 5.0f);

		// ボスがプレイヤーに一定距離近づいたら攻撃モーションに変更を行う
		if((transform.position - m_PlayerObj.transform.position).magnitude < PlayerDistance)
		{
			m_State = State.Attack;
		}
	}

	void AttackUpdate()
	{
		m_Animotor.SetBool("attack01", true);
		m_TimeCnt += Time.deltaTime;

		AnimatorStateInfo anim = m_Animotor.GetCurrentAnimatorStateInfo(0);

		if (anim.shortNameHash != m_AttackAnime && m_TimeCnt > 1.0f)
		{
			Debug.Log("モーション終わった");
			m_State = State.Return;
			m_TimeCnt = 0.0f;
		}
	}

	void ReturnUpdate()
	{
		m_Animotor.SetBool("idle01", true);
		Vector3 RetPos = transform.position - m_PlayerObj.transform.position;
		RetPos = RetPos.normalized * m_ReturnDistance + transform.position;

		Quaternion m_InitRot = transform.rotation;
		transform.LookAt(RetPos);

		transform.rotation = Quaternion.RotateTowards(m_InitRot, transform.rotation, m_RotSpeed);

		if (transform.rotation == m_InitRot)
		{
			m_State = State.ReturnMove;
		}
	}

	void ReturnMoveUpdate()
	{
		m_Animotor.SetBool("run", true);

		transform.position += transform.forward.normalized * (Time.deltaTime * 5.0f);

		// ボスがプレイヤーに一定距離近づいたら攻撃モーションに変更を行う
		if ((transform.position - m_PlayerObj.transform.position).magnitude > m_ReturnDistance)
		{
			m_State = State.Wait;
		}
	}

	void DamageUpdate()
	{
		m_Animotor.SetBool("damage", true);
	}

	void AllAnimeFlagInit()
	{
		m_Animotor.SetBool("walk", false);
		m_Animotor.SetBool("run", false);
		m_Animotor.SetBool("attack01", false);
		m_Animotor.SetBool("damage", false);
		m_Animotor.SetBool("dead", false);
		m_Animotor.SetBool("idle01", false);
	}
}