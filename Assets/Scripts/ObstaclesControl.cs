using System.Collections;
using UnityEngine;

public class ObstaclesControl : MonoBehaviour
{
    [SerializeField] private GameObject _leftObstacle;
    [SerializeField] private GameObject _rightObstacle;
    [SerializeField] private float _angleRotation = 30f;
    [SerializeField] private AudioSource _pullObstacleSound;

    private bool _isRotateLeftPressed;
    private bool _isRotateRightPressed;

    private void Update()
    {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR

        float rotationObstacle = Input.GetAxisRaw("Horizontal");

        PullObstacle(rotationObstacle);

#endif

    }

    public void PullObstacle(float minusIsLeftPlusIsRight)
    {
        if (minusIsLeftPlusIsRight < 0 && !_isRotateLeftPressed)
        {
            _isRotateLeftPressed = true;
            _pullObstacleSound.Play();
            StartCoroutine(RotateObstacle(_leftObstacle, -_angleRotation));
        }

        if (minusIsLeftPlusIsRight > 0 && !_isRotateRightPressed)
        {
            _isRotateRightPressed = true;
            _pullObstacleSound.Play();
            StartCoroutine(RotateObstacle(_rightObstacle, _angleRotation));
        }
    }

    private IEnumerator RotateObstacle(GameObject obstacle, float angle)
    {
        obstacle.transform.Rotate(Vector3.up, angle);

        yield return new WaitForSeconds(0.2f);

        obstacle.transform.Rotate(Vector3.up, -angle);

        ResetIsPressedObstacle(obstacle);
    }

    private void ResetIsPressedObstacle(GameObject obstacle)
    {
        if (obstacle == _leftObstacle)
        {
            _isRotateLeftPressed = false;
        }

        if (obstacle == _rightObstacle)
        {
            _isRotateRightPressed = false;
        }
    }
}
