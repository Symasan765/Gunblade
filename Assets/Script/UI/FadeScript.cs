using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
	public bool m_FadeInFlag = true;	// true : フェードイン false : フェードアウト

	float m_TimeCnt = 0.0f;
	public float m_FadeTime = 0.5f;
	Image m_Image;

	private void Start()
	{
		m_Image = GetComponent<Image>();
		m_Image.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
	}

	// Update is called once per frame
	void Update()
    {
		if (m_FadeInFlag)
		{
			m_TimeCnt += Time.deltaTime;
			if (m_TimeCnt > m_FadeTime) m_TimeCnt = m_FadeTime;
			float t = m_TimeCnt / m_FadeTime;

			m_Image.color = new Color(0.0f, 0.0f, 0.0f, 1.0f - t);
		}
		else
		{
			m_TimeCnt -= Time.deltaTime;
			if (m_TimeCnt < 0.0f) m_TimeCnt = 0.0f;
			float t = m_TimeCnt / m_FadeTime;

			m_Image.color = new Color(0.0f, 0.0f, 0.0f, 1.0f - t);
		}
	}

	public bool SceneTrans()
	{
		if(m_FadeInFlag == false)
		{
			if(m_TimeCnt == 0.0f)
			{
				return true;
			}
		}
		return false;
	}

	public void ChangeFlag(bool flag)
	{
		m_FadeInFlag = flag;
	}
}
