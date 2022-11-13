using UnityEngine;

public class SlideBox : MonoBehaviour
{
    [SerializeField] private Transform _pointLeft;
    [SerializeField] private Transform _pointRight;
    [SerializeField] private float _sliderSpeed;
    [SerializeField] private AudioSource _sliderCollisionSound;
    [SerializeField] private Animator _slideAnimator;
    [SerializeField] private ParticleSystem _slideParticleSystem;

    private float _platformStartScaleY;
    private bool _isLeft;

    private void Start()
    {
        _platformStartScaleY = transform.localScale.y;
    }

    private void Update()
    {
        if (!_isLeft)
            if ((transform.position - _pointLeft.position).magnitude > 0.02f)
                transform.position = Vector3.Lerp(transform.position, _pointLeft.position, Time.deltaTime * _sliderSpeed);
            else
                _isLeft = true;
        else
            if ((transform.position - _pointRight.position).magnitude > 0.02f)
                transform.position = Vector3.Lerp(transform.position, _pointRight.position, Time.deltaTime * _sliderSpeed);
            else
                _isLeft = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>() && transform.localScale.y > _platformStartScaleY / 2.4f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.9f, transform.localScale.z);
            _sliderSpeed *= 1.3f;
        }

        _sliderCollisionSound?.Play();
        _slideAnimator?.SetTrigger("DoScale");
        _slideParticleSystem?.Play();
    }
}
