using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    [SerializeField] private Transform _screwTransform;
    [SerializeField] private GameObject _screwPlatform;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _speedScrewCompress;
    [SerializeField] private float _ballPushForce;

    private float _startScrewScaleVertical;
    private float _startPlatformVerticalPosition;
    private float _lastPlatformVerticalPosition;

    private float _localX;
    private float _localY;
    private float _localZ;

    private void Start()
    {
        _startPlatformVerticalPosition = _screwPlatform.transform.localPosition.y;
        _lastPlatformVerticalPosition = _startPlatformVerticalPosition;
        _startScrewScaleVertical = _screwTransform.localScale.z;

        _localX = _screwTransform.localScale.x;
        _localY = _screwTransform.localScale.y;
        _localZ = _screwTransform.localScale.z;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200) && hit.transform?.gameObject.GetComponent<Screw>())
            {
                //Debug.DrawLine(ray.origin, hit.point);
                ScrewRun();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScrewRun();
        }

        VerticalScrewScaleTransformation();
    }

    private void VerticalScrewScaleTransformation()
    {
        if (_lastPlatformVerticalPosition != _screwPlatform.transform.localPosition.y)
        {
            _localZ = _startScrewScaleVertical * _screwPlatform.transform.localPosition.y / _startPlatformVerticalPosition;
            _lastPlatformVerticalPosition = _screwPlatform.transform.localPosition.y;
        }

        _screwTransform.localScale = new Vector3(_localX, _localY, _localZ);
    }

    private void ScrewRun() => StartCoroutine(CompressSpring());

    private IEnumerator CompressSpring()
    {
        PlatformIsKinematicSelector(true);

        for (float platformVerticalPosition = _startPlatformVerticalPosition; 
                            platformVerticalPosition > _startPlatformVerticalPosition / 2f; platformVerticalPosition -= Time.deltaTime * _speedScrewCompress)
        {
            _screwPlatform.transform.localPosition = new Vector3(_screwPlatform.transform.localPosition.x,
                                                            platformVerticalPosition,
                                                            _screwPlatform.transform.localPosition.z);

            yield return null;
        }
                
        PlatformIsKinematicSelector(false);
    }

    private void PlatformIsKinematicSelector(bool isKinematic)
    {
        Rigidbody rigidbodyPlatform = _screwPlatform.GetComponent<Rigidbody>();
        rigidbodyPlatform.isKinematic = isKinematic;
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody ballRB = other.GetComponent<Ball>()?.GetComponent<Rigidbody>();

        if (ballRB)
        {
            ballRB.AddForce(Vector3.up * _ballPushForce, ForceMode.Impulse);
        }
    }
}
