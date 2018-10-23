using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスでカメラの位置、回転を制御する
/// </summary>
public class VRCameraDebug : MonoBehaviour
{
    enum DragType
    {
        Move,
        Rotate,
    }

    private bool _isDragging = false;
    private Vector3 _prevPos = Vector3.zero;
    private DragType _currenType;
    private float _speedLimit = 100f;
    private Quaternion _originalRot;

    private float _x = 0f;
    private float _y = 0f;

    [SerializeField]
    [Range(0f, 10f)]
    public float _moveSpeed = -5f;

    [SerializeField]
    [Range(0f, 100f)]
    public float _rotateSpeed = 15f;

    [SerializeField]
    private Transform _controlTarget;
    private Transform ControlTarget
    {
        get
        {
            if (!_controlTarget)
            {
                _controlTarget = transform;
            }

            return _controlTarget;
        }
    }


    #region MonoBehaviour
    void Start()
    {
        _originalRot = ControlTarget.rotation;
    }

    void Update()
    {
        float wheelval = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = ControlTarget.position;
        pos += ControlTarget.forward * wheelval * 2f;
        ControlTarget.position = pos;

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown(DragType.Move);
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            OnMouseDown(DragType.Rotate);
        }

        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            OnMouseUp();
        }

        OnMouseMove();
    }
    #endregion


    void OnMouseDown(DragType type)
    {
        _isDragging = true;
        _currenType = type;
        _prevPos = Input.mousePosition;
    }

    void OnMouseUp()
    {
        _isDragging = false;
    }

    void OnMouseMove()
    {
        if (!_isDragging)
        {
            return;
        }

        Vector3 delta = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
            delta.x = -1.0f;
        else if (Input.GetKey(KeyCode.D))
            delta.x = 1.0f;
        if (Input.GetKey(KeyCode.W))
            delta.y = 1.0f;
        else if (Input.GetKey(KeyCode.S))
            delta.y = -1.0f;

        delta = delta.normalized;
        delta *= _rotateSpeed;

        switch (_currenType)
        {
            case DragType.Move:
                delta *= (_moveSpeed / _speedLimit);

                Vector3 pos = ControlTarget.position;
                pos += ControlTarget.up * -delta.y;
                pos += ControlTarget.right * delta.x;

                ControlTarget.position = pos;

                return;

            case DragType.Rotate:
                delta *= (_rotateSpeed / _speedLimit);

                _x += delta.x;
                if (_x <= -180)
                {
                    _x += 360;
                }
                else if (_x > 180)
                {
                    _x -= 360;
                }

                _y -= delta.y;
                _y = Mathf.Clamp(_y, -85f, 85f);

                ControlTarget.rotation = _originalRot * Quaternion.Euler(_y, _x, 0f);

                return;
        }

    }
}