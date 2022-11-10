using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private float _rotateSpeed;

    private bool _isRotate;

    private void Update()
    {
        float rotationObstacle = Input.GetAxis("Horizontal");

        if (rotationObstacle < 0)
        {
            _isRotate = true;
        }

        if (rotationObstacle > 0)
        {
            _isRotate = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isRotate)
        {
            _cube.GetComponent<Rigidbody>().AddTorque(0f, 5f, 0f);
            print("CubeRotate");
        }
    }
}
