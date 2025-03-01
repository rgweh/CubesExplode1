using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour 
{
    [SerializeField] private float _explodeRadius;
    [SerializeField] private float _explodeForse;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private int _generation;

    private float _scaleReduce = 2;

    public event Action<GameObject> TryDuplicateCube;
    public float ActionChance { get; private set; }

    private void OnEnable()
    {
        _generation++;

        ActionChance = 100 / _generation;

        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x / _scaleReduce, scale.y / _scaleReduce, scale.z / _scaleReduce);

        var renderer = GetComponent<Renderer>();
        renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private void OnMouseUpAsButton()
    {
        TryDuplicateCube?.Invoke(gameObject);
        Explode();
        Instantiate(_effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explodeRadius);

        List<Rigidbody> affectedObjects = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody == true)
                affectedObjects.Add(hit.attachedRigidbody);
        }

        return affectedObjects;
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explodeForse, transform.position, _explodeRadius);
        }
    }

}
