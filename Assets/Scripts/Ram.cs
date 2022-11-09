using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ram : MonoBehaviour
{
    [SerializeField] private Transform _cannonBallAxisZRotation;
    [SerializeField] private Slider _sliderCannonBallRotationCount;

    private Rigidbody[] _rigidbodiesDoAction;
    private bool _isBoom = false;

    public void MakeBoom()
    {
        if (!_isBoom)
        {
            _isBoom = true;

            RigidbodiesIsKinimaticOn(false);
        }
    }

    private void Awake()
    {
        _rigidbodiesDoAction = GetRigidbodiesFromParentObject(_cannonBallAxisZRotation.gameObject);
        RigidbodiesIsKinimaticOn();

        _cannonBallAxisZRotation.rotation = Quaternion.Euler(0f, 0f, _sliderCannonBallRotationCount.value);
    }

    private void Update()
    {
        if (!_isBoom)
            _cannonBallAxisZRotation.rotation = Quaternion.Euler(0f, 0f, _sliderCannonBallRotationCount.value);
    }

    private void RigidbodiesIsKinimaticOn(bool isKinematicOn = true)
    {
        foreach (var rigidbody in _rigidbodiesDoAction)
        {
            rigidbody.isKinematic = isKinematicOn;
        }
    }

    private Rigidbody[] GetRigidbodiesFromParentObject(GameObject parent) => parent.GetComponentsInChildren<Rigidbody>(true);
}
