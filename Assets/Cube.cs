
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour 
{
    [SerializeField] private float _explodeRadius;
    [SerializeField] private float _explodeForse;
    [SerializeField] private ParticleSystem _effect;

    [SerializeField] private int _generation;
    [SerializeField] private int _minAmount = 2;
    [SerializeField] private int _maxAmount = 6;

    private float _scaleReduce = 2;
    private float _actionChance;
    private Renderer _cubeRenderer;
    private GameObject _effectObject;

    private float minXZ = -10f;
    private float maxXZ = 10f;
    private float minY = 0;
    private float maxY = 10;

    private void OnEnable()
    {
        _generation++;

        _actionChance = 100 / _generation;

        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x / _scaleReduce, scale.y / _scaleReduce, scale.z / _scaleReduce);

        _cubeRenderer = GetComponent<Renderer>();
        _cubeRenderer.material.color = Random.ColorHSV();
    }

    private void OnMouseUpAsButton()
    {
        TryDuplicate();
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

    private void TryDuplicate()
    {
        if(Random.Range(0, 100) <= _actionChance)
        {
            int amount = Random.Range(_minAmount, _maxAmount);

            for (int i = 0; i < amount; i++)
            {
                Vector3 position = new Vector3(0f + Random.Range(minXZ,maxXZ), 0f + Random.Range(minY, maxY), 0f + Random.Range(minXZ, maxXZ));
                Instantiate(gameObject, position, Quaternion.identity);
            }
        } 
    }
}


