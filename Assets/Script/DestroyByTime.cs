using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
	float lifetime;

	void Start()
	{
		Destroy(gameObject, lifetime);
	}
}