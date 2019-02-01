using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject SpawnObject;
    public float SpawnTimeSpan = 5;

    private float AreaLength;

    public GameObject SpawnEffect;

    // Start is called before the first frame update
    void Start()
    {
        AreaLength = this.transform.localScale.y;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;


        StartCoroutine(AutoSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AutoSpawner()
    {
        while (true)
        {
            // Do anything
            float SpawnPoint = 0;
            float halfLength = AreaLength / 2.0f;
            SpawnPoint = Random.Range(-halfLength, halfLength);

            Vector3 SpawnPosition = this.transform.position + this.transform.up * SpawnPoint;

            var go = Instantiate(SpawnObject, SpawnPosition, Quaternion.identity);
            go.transform.LookAt(go.transform.position + this.transform.forward);
            var go2 = Instantiate(SpawnEffect, SpawnPosition, Quaternion.identity);
            Destroy(go2, 1.0f);



            yield return new WaitForSeconds(SpawnTimeSpan);
        }
    }

}
