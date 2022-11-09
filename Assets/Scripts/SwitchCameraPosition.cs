using System;
using UnityEngine;

public enum Positions
{
    Boom,
    Pinball
}


public class SwitchCameraPosition : MonoBehaviour
{
    public event Action<string> CameraSwitchPositionEvent;

    [SerializeField] private GameObject _menuRamActivate;
    [SerializeField] private Transform _placeBoom;
    [SerializeField] private Transform _placePinball;
    private Positions _position = Positions.Boom;

    [SerializeField] private float _followSpeed;
    [SerializeField] private float _rotateSpeed;
    private Transform _cameraTargetPoint;

    public void Switch()
    {
        CameraSwitchPositionEvent?.Invoke(_position.ToString());

        if (_position == Positions.Boom)
            SetPlacePinball();
        else
            SetPlaceBoom();
    }

    private void Awake()
    {
        SetPlaceBoom();
    }

    private void Update()
    {
        CameraMoveToPosition();
    }

    private void CameraMoveToPosition()
    {
        if ((transform.position - _cameraTargetPoint.position).magnitude > 0.02f)
        {
            transform.position = Vector3.Lerp(transform.position, _cameraTargetPoint.position, Time.deltaTime * _followSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, _cameraTargetPoint.rotation, Time.deltaTime * _rotateSpeed);
        }
    }

    private void SetPlaceBoom()
    {
        _position = Positions.Boom;
        SwitchCameraFollow(_placeBoom);
        _menuRamActivate.SetActive(true);
    }

    private void SetPlacePinball()
    {
        _position = Positions.Pinball;
        SwitchCameraFollow(_placePinball);
        _menuRamActivate.SetActive(false);
    }

    private void SwitchCameraFollow(Transform cameraFollowObject)
    {
        _cameraTargetPoint = cameraFollowObject;
    }
}
