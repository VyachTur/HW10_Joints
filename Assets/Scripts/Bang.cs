using UnityEngine;

public class Bang : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystemBang;
    [SerializeField] private float _power = 15f;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private float _upForce = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (ballRigidbody != null)
        {
            _particleSystemBang.Play();
            ballRigidbody.AddExplosionForce(_power, transform.position, _radius, _upForce, ForceMode.Impulse);
        }
    }
}
