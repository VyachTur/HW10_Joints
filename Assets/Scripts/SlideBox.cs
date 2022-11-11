using UnityEngine;

public class SlideBox : MonoBehaviour
{
    [SerializeField] private Transform _pointLeft;
    [SerializeField] private Transform _pointRight;
    [SerializeField] private float _sliderSpeed;

    private bool _isLeft;

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
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.9f, transform.localScale.z);
            _sliderSpeed *= 1.2f;
        }
    }
}
