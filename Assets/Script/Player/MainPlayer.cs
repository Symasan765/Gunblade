using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{

	public float m_TransTimeSec = 0.2f;     // 武器変形の際の移動速度
	GameObject[] m_EnemyObjs;       // 0 = 左手、1 = 右手
	Vector3 m_OriginPos; // プレイヤーの基本座標
	Vector3 m_MovePos;   // 移動する方向の座標
	Vector3 m_LeavePos; // 移動元座標

	GameObject m_CameraObj; // プレイヤーの視点となるカメラオブジェクト
	float m_TransCnt;

	public float m_MaxHitPoint = 100.0f;
	float m_HitPoint = 0;

	public DamagedCamera m_DamageCamera;
	public float m_DamageSec = 1.0f;

	public enum HandData
	{
		Right,
		Left
	}

	enum PlayerTrans
	{
		Attack,     // 攻撃中
		Translate   // 移動中
	}

	PlayerTrans m_Trans;

	// Use this for initialization
	void Start()
	{
		m_TransCnt = 0;
		m_CameraObj = GameObject.Find("Camera (eye)");
		
		m_OriginPos = m_CameraObj.transform.position;
		m_LeavePos = m_OriginPos;
		m_EnemyObjs = new GameObject[2];
		m_EnemyObjs[0] = gameObject;
		m_EnemyObjs[1] = gameObject;

		m_Trans = PlayerTrans.Attack;

		m_HitPoint = m_MaxHitPoint;

		m_DamageCamera.m_DamageProductionSec = m_DamageSec;
	}

	// Update is called once per frame
	void Update()
	{
		switch (m_Trans)
		{
			case PlayerTrans.Attack:
				break;
			case PlayerTrans.Translate:
				m_CameraObj.transform.position = Vector3.zero;
				Translate();
				break;
		}

		if (Input.GetKeyDown(KeyCode.P))
			IsDamage(0.5f);
	}

		/// <summary>
		/// 弾が当たった敵の情報をプレイヤーに渡す関数
		/// </summary>
		/// <param name="hand"></param>
		/// <param name="obj"></param>
		public void SetEnemyObj(HandData hand, GameObject obj)
	{
		switch (hand)
		{
			case HandData.Right:
				m_EnemyObjs[0] = obj;
				break;
			case HandData.Left:
				m_EnemyObjs[1] = obj;
				break;
		}
	}

	/// <summary>
	///  敵の近づく際の命令
	/// </summary>
	public void Proximity(HandData hand)
	{
		switch (hand)
		{
			case HandData.Right:
				m_MovePos = m_EnemyObjs[0].transform.position;
				break;
			case HandData.Left:
				m_MovePos = m_EnemyObjs[1].transform.position;
				break;
		}

		Vector3 interpolation = transform.position - m_MovePos;
		interpolation = interpolation.normalized * 1.5f;
		m_MovePos += interpolation;
		interpolation.y = 0.0f;

		m_LeavePos = gameObject.transform.position;
		m_Trans = PlayerTrans.Translate;
	}

	/// <summary>
	/// 敵から離れる時の命令
	/// </summary>
	public void LongDistance()
	{
		m_LeavePos = gameObject.transform.position;
		m_MovePos = m_OriginPos;
		m_Trans = PlayerTrans.Translate;
	}

	// 移動中処理
	void Translate()
	{
		// 移動処理
		float t = m_TransCnt / m_TransTimeSec;
		gameObject.transform.position = Vector3.Lerp(m_LeavePos, m_MovePos,t);
		m_TransCnt += Time.deltaTime;

		// 移動が終わった
		if(m_TransCnt > m_TransTimeSec)
		{
			m_TransCnt = 0.0f;
			//m_CameraObj.transform.position = m_MovePos;
			m_Trans = PlayerTrans.Attack;
		}
	}

	public void IsDamage(float damageValue)
	{
		m_DamageCamera.Damage();
	}
}