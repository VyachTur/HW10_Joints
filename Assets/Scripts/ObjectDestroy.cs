using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    [SerializeField] private GameObject _destroyedObjectPrefab;

    [SerializeField] private float _explosionRadius = 1.5f;
    [SerializeField] private float _explosionUpForce = 0.2f;
    [SerializeField] private float _explosionPowerCoefficient = 0.3f;
    [SerializeField] private float _secondsForBoxDestroyed = 15f;

    private float _explosionPower = 1.2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CannonBallMarker>())
        {
            Rigidbody collisionRB = collision.rigidbody;

            if (collisionRB)
            {
                _explosionPower = collisionRB.velocity.magnitude * _explosionPowerCoefficient;
            }

            Rigidbody[] rigidbodies = GetRigidbodiesFromPointRadius(collision.transform.position, _explosionRadius);

            SmallExplosion(rigidbodies);

            BoxDestroy();
        }
    }

    private Rigidbody[] GetRigidbodiesFromPointRadius(Vector3 point, float radius)
    {
        return new List<Collider>(Physics.OverlapSphere(point, _explosionRadius))
                                                .Select(collider => collider.GetComponent<Rigidbody>())
                                                .Where(rigidbody => rigidbody != null)
                                                .ToArray();
    }

    private void SmallExplosion(Rigidbody[] rigidbodies)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.AddExplosionForce(_explosionPower, transform.position, _explosionRadius, _explosionUpForce, ForceMode.Impulse);
    }

    private void BoxDestroy()
    {
        Destroy(gameObject);

        GameObject destroyedGO = Instantiate(_destroyedObjectPrefab, transform.position, transform.rotation);

        Destroy(destroyedGO, _secondsForBoxDestroyed);
    }
}
