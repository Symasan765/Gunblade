using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSystem : MonoBehaviour
{
    public GameObject GrabPoint;
    public GameObject AnchorA;
    public GameObject AnchorB;
    public GameObject LineEffectA;
    public GameObject LineEffectB;
    public GameObject ArrowSetPoint;
    public GameObject ArrowObject;

    private GameObject GrabArrow;

    public bool Grabing = false;
    float MaxGrabPoint = -30.0f;
    private bool ArrowInit = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Grabing = true;
        }
        // shot
        if(Input.GetKeyDown(KeyCode.B))
        {
            Grabing = false;
            ArrowInit = true;
            GrabArrow.GetComponent<Arrow>().Shot(Mathf.Abs(GrabPoint.transform.localPosition.z) * 1.0f);
            GrabArrow = null;
        }

        ArrowInitialize();

        LineToDefault();
        UpdateBowLine();

        UpdateArrow();

    }

    void UpdateBowLine()
    {
        Vector3 LineLocationA = (GrabPoint.transform.position + AnchorA.transform.position) / 2;
        Vector3 LineLocationB = (GrabPoint.transform.position + AnchorB.transform.position) / 2;

        LineEffectA.transform.position = LineLocationA;
        LineEffectB.transform.position = LineLocationB;

        LineEffectA.transform.LookAt(GrabPoint.transform.position);
        LineEffectB.transform.LookAt(GrabPoint.transform.position);

        ParticleSystem.ShapeModule shapeModuleA = LineEffectA.GetComponent<ParticleSystem>().shape;
        ParticleSystem.ShapeModule shapeModuleB = LineEffectB.GetComponent<ParticleSystem>().shape;

        float distA = Vector3.Distance(GrabPoint.transform.position, AnchorA.transform.position);
        float distB = Vector3.Distance(GrabPoint.transform.position, AnchorB.transform.position);

        shapeModuleA.radius = distA * 0.5f;
        shapeModuleB.radius = distB * 0.5f;

        GrabPoint.transform.localPosition = new Vector3(-1.14f, GrabPoint.transform.localPosition.y, Mathf.Max( GrabPoint.transform.localPosition.z, MaxGrabPoint ));
    }

    public void SetGrabPosition(float z)
    {
        GrabPoint.transform.localPosition = new Vector3(-1.14f, GrabPoint.transform.localPosition.y, Mathf.Max(z, MaxGrabPoint));
    }

    // 弦を離したときに元に戻る挙動
    void LineToDefault()
    {

        if (Grabing) return;
        Vector3 ZeroPoint = new Vector3(0, 0, -3);
        Vector3 accel = ZeroPoint - GrabPoint.transform.localPosition;

        GrabPoint.transform.localPosition += accel * 1.4f;
    }

    void UpdateArrow()
    {
        if (GrabArrow)
        {
            GrabArrow.transform.position = GrabPoint.transform.position;
            GrabArrow.transform.LookAt(ArrowSetPoint.transform.position);
        }
    }

    void ArrowInitialize()
    {

        if (!ArrowInit || !Grabing) return;
        // OnceEffects

        // Spawn Arrow
        GrabArrow = Instantiate(ArrowObject);
        ArrowInit = false;
        
    }

    public void Grab()
    {
        Grabing = true;

    }

    public void Shot()
    {

        Grabing = false;
        ArrowInit = true;
        GrabArrow.GetComponent<Arrow>().Shot(Mathf.Abs(GrabPoint.transform.localPosition.z) * 1.0f);
        GrabArrow = null;
        
    }




}
