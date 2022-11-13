using UnityEngine;

public class SlideBox : MonoBehaviour
{
    [SerializeField] private Transform _pointLeft;
    [SerializeField] private Transform _pointRight;
    [SerializeField] private float _sliderSpeed;
    [SerializeField] private AudioSource _sliderCollisionSound;
    [SerializeField] private Animator _slideAnimator;
    [SerializeField] private ParticleSystem _slideParticleSystem;

    [SerializeField] private float _slideScaleKoefficient = 2.4f;
    [SerializeField] private float _slideScaleMultiplier = 0.9f;
    [SerializeField] private float _slideSpeedMultiplier = 1.3f;

    private float _platformStartScaleY;
    private bool _isLeft;

    private void Start()
    {
        _platformStartScaleY = transform.localScale.y;
    }

    private void Update()
    {
        if (!_isLeft)
            if ((transform.position - _pointLeft.position).magnitude > Constants.MovableInaccuracy)
                transform.position = Vector3.Lerp(transform.position, _pointLeft.position, Time.deltaTime * _sliderSpeed);
            else
                _isLeft = true;
        else
            if ((transform.position - _pointRight.position).magnitude > Constants.MovableInaccuracy)
                transform.position = Vector3.Lerp(transform.position, _pointRight.position, Time.deltaTime * _sliderSpeed);
            else
                _isLeft = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>() && transform.localScale.y > _platformStartScaleY / _slideScaleKoefficient)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * _slideScaleMultiplier, transform.localScale.z);
            _sliderSpeed *= _slideSpeedMultiplier;
        }

        _sliderCollisionSound?.Play();
        _slideAnimator?.SetTrigger("DoScale");
        _slideParticleSystem?.Play();
    }
}
