using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public Queue<GameObject> pooledObjects;
    [SerializeField] private GameObject _objectPrefab;

    [Space]


    [Header("Pool Values")]
    [SerializeField] private int _poolSize;
    [SerializeField] private float _spawnX;
    [SerializeField] private float _spawnY;
    [SerializeField] private float _spawnZ;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


        pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_objectPrefab);
            pooledObjects.Enqueue(obj);

            GetPoolObject();

        }
    }

    public void NewTransform(GameObject obj)
    {
        obj.transform.position = new Vector3(Random.Range(-_spawnX, _spawnX), _spawnY, (Random.Range(-_spawnZ, _spawnZ)));
    }

    public void GetPoolObject()
    {
        GameObject obj = pooledObjects.Dequeue();
        obj.transform.position = new Vector3(Random.Range(-_spawnX,_spawnX), _spawnY, (Random.Range(-_spawnZ,_spawnZ)));
        obj.SetActive(true);

    }
}
