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
    [SerializeField] private GameObject _menuPinballHelp;
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
        SetPlacePinball();
    }

    private void Update()
    {
        CameraMoveToPosition();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Escape)) ExitGame();

#endif
    }

    private void CameraMoveToPosition()
    {
        if ((transform.position - _cameraTargetPoint.position).magnitude > Constants.MovableInaccuracy)
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
        _menuPinballHelp.SetActive(false);
    }

    private void SetPlacePinball()
    {
        _position = Positions.Pinball;
        SwitchCameraFollow(_placePinball);
        _menuPinballHelp.SetActive(true);
        _menuRamActivate.SetActive(false);
    }

    private void SwitchCameraFollow(Transform cameraFollowObject)
    {
        _cameraTargetPoint = cameraFollowObject;
    }

    private void ExitGame() => Application.Quit();
}
