using UnityEngine;

public class Screw : MonoBehaviour
{
    [SerializeField] private Transform _screwTransform;
    [SerializeField] private GameObject _screwPlatform;
    [SerializeField] private AudioSource _screwSound;
    [SerializeField] private float _screwCompressForce = 60f;

    private Rigidbody _screwPlatformRigidbody;

    private float _startScrewScaleVertical;
    private float _startPlatformVerticalPosition;
    private float _lastPlatformVerticalPosition;

    private float _localX;
    private float _localY;
    private float _localZ;

    private float _timer;
    private bool _isBallDoRun;

    private void Start()
    {
        _screwPlatformRigidbody = _screwPlatform.GetComponent<Rigidbody>();

        _startPlatformVerticalPosition = _screwPlatform.transform.localPosition.y;
        _lastPlatformVerticalPosition = _startPlatformVerticalPosition;
        _startScrewScaleVertical = _screwTransform.localScale.z;

        _localX = _screwTransform.localScale.x;
        _localY = _screwTransform.localScale.y;
        _localZ = _screwTransform.localScale.z;
    }

    private void Update()
    {
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

    private void FixedUpdate()
    {
        PullBall();
    }

    private void PullBall()
    {
        if (_isBallDoRun)
        {
            _timer += Time.fixedDeltaTime;

            ReleaseScrew();
            MakeSpringCompress();
        }
    }

    private void ReleaseScrew()
    {
        if (_timer > Constants.TimeForSpringRelease)
        {
            _timer = 0f;
            _screwSound?.Play();
            _isBallDoRun = false;
        }
    }

    private void MakeSpringCompress()
    {
        if (_timer > Constants.TimeForSpringCompress)
        {
            SpringCompress((_timer - Constants.TimeForSpringCompress) * _screwCompressForce);
        }
    }

    private void SpringCompress(float forceKoefficient) =>
        _screwPlatformRigidbody.AddForce(Vector3.down * forceKoefficient, ForceMode.VelocityChange);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Ball>(out _))
        {
            _isBallDoRun = true;
        }
    }
}
