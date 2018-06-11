using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public delegate void SpawnPlayerAction(GameObject playerInstance);
    public static SpawnPlayerAction OnPlayerSpawn;

    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private Transform _spawnPoint;

    private GameObject _instance;

    private void Start()
    {
        if (_instance == null)
            Spawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Spawn();
    }

    private void Spawn()
    {
        if (_instance != null)
            Destroy(_instance);

        _instance = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);
        OnPlayerSpawn?.Invoke(_instance);
    }
}
