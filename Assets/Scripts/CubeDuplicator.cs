using System.Collections.Generic;
using UnityEngine;

public class CubeDuplicator : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private int _minAmount = 2;
    [SerializeField] private int _maxAmount = 6;

    private float _rangeX;
    private float _rangeY;
    private float _rangeZ;

    private void OnEnable()
    {
        _cube.TryDuplicateCube += OnTryDuplicateCube; 

        _rangeX = transform.localScale.x/2;
        _rangeY = transform.localScale.y/2;
        _rangeZ = transform.localScale.z/2;
    }

    private void OnTryDuplicateCube(GameObject cubeObject)
    {
        Debug.Log("Trying to duplicate");
        Cube parentCube = cubeObject.GetComponent<Cube>();

        if (Random.Range(0, 100) <= parentCube.ActionChance)
        {
            int amount = Random.Range(_minAmount, _maxAmount);

            for (int i = 0; i < amount; i++)
            {
                Vector3 position = GetRandomPosition();
                GameObject newborn = Instantiate(cubeObject, position, Quaternion.identity);
                Cube newbornCube = newborn.GetComponent<Cube>();
                newbornCube.TryDuplicateCube += OnTryDuplicateCube;
            }
        }

        Debug.Log("Tried succes!");
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(transform.position.x - _rangeX, transform.position.x + _rangeX);
        float y = Random.Range(transform.position.y - _rangeY, transform.position.y + _rangeY);
        float z = Random.Range(transform.position.z - _rangeZ, transform.position.z + _rangeZ);

        return new Vector3(x, y, z);
    }
}
