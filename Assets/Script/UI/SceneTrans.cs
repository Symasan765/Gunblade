using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrans : MonoBehaviour
{
	public string m_NextScene;
	public FadeScript m_Fade;

    // Update is called once per frame
    void Update()
    {
		if (ViveCtrl.Get.Trigger(ViveCtrl.ViveDeviceType.RightHand,ViveCtrl.ViveKey.Trigger))
		{
			m_Fade.m_FadeInFlag = false;
		}

		if (Input.GetKeyDown(KeyCode.Space))
			m_Fade.ChangeFlag(false);

		if (m_Fade.SceneTrans())
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(m_NextScene);
		}
	}
}
