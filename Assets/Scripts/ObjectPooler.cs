using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPooler
{
    [SerializeField]
    private Transform _objectParent;
    [SerializeField]
    private GameObject _objectPrefab;

    [Header("Pool Settings")]
    [SerializeField]
    private bool _poolShouldExpand = true;
    [SerializeField]
    private List<GameObject> _pooledObjects;
    [SerializeField]
    private int _amountToPool;

    public void PoolObjects()
    {
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject newObject = GameObject.Instantiate(_objectPrefab) as GameObject;
            newObject.SetActive(false);
            _pooledObjects.Add(newObject);
        }
    }

    public void PoolObjects(Transform parent)
    {
        _objectParent = parent;
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject newObject = GameObject.Instantiate(_objectPrefab, _objectParent) as GameObject;
            newObject.SetActive(false);
            _pooledObjects.Add(newObject);
        }
    }

    public void ClearPool()
    {
        foreach (GameObject obj in _pooledObjects)
        {
            GameObject.Destroy(obj);
        }
        _pooledObjects = new List<GameObject>();
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in _pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        if (_poolShouldExpand)
        {
            GameObject newObject = GameObject.Instantiate(_objectPrefab, _objectParent) as GameObject;
            newObject.SetActive(false);
            _pooledObjects.Add(newObject);
            return newObject;
        }
        else
        {
            return null;
        }
    }
}
