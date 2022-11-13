using System.Collections;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private AudioSource _bounceSound;
    [SerializeField] private MeshRenderer[] _bounceMeshRenderers;

    private Color _startColor;

    private void Awake()
    {
        _startColor = _bounceMeshRenderers[0].material.GetColor("_EmissionColor");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out _))
        {
            _bounceSound?.Play();
            SetEmissionColorOnBounceMeshes(Color.red);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(SetEmissionWithDelay(0.1f));
    }

    private void SetEmissionColorOnBounceMeshes(Color color)
    {
        foreach (MeshRenderer meshRenderer in _bounceMeshRenderers)
        {
            meshRenderer.material.SetColor("_EmissionColor", color);
        }
    }

    private IEnumerator SetEmissionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetEmissionColorOnBounceMeshes(_startColor);
    }
}
