using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindMovementTest : MonoBehaviour
{
    [SerializeField]
    private GameObject _grid;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float _speed;

    private Pathfinding _pathFinder;
    private bool _foundPath;
    private List<Node> _currentPath;
    private int _currentNodeIndex = 0;

    private void Awake()
    {
        SpawnPlayer.OnPlayerSpawn += StorePlayer;
    }

    private void StorePlayer(GameObject player)
    {
        _player = player;
        FindPath();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            FindPath();

        UpdateMovement();
    }

    private void FindPath()
    {
        _currentNodeIndex = 0;
        _pathFinder = new Pathfinding();
        _pathFinder.grid = FindObjectOfType<DungeonRoomGrid>();

        _currentPath = _pathFinder.FindPath(_grid.transform.position, _grid.transform.position);

        Debug.Log("CellTarget: " + GridChecker.GetGridCellFromPosition(_currentPath[0].worldPosition));

        if (_currentPath == null)
        {
            _foundPath = false;
        }
        else
        {
            _foundPath = true;
        }

        //Debug.Log("Found Path: " + _foundPath);
    }

    private void UpdateMovement()
    {
        if (_foundPath)
        {
            Debug.Log("Found Path");
            Debug.Log(_currentNodeIndex + " : " + _currentPath.Count);

            if (_currentNodeIndex < _currentPath.Count)
            {
                Debug.Log("Moving towards player");

                Vector2 direction = (_currentPath[_currentNodeIndex].worldPosition - (Vector2)transform.position).normalized;
                float dist = Mathf.Clamp01(Vector2.Distance(transform.position, _currentPath[_currentNodeIndex].worldPosition));

                transform.position += (Vector3)(direction * (_speed * dist) * Time.deltaTime);

                if (Vector2.Distance(transform.position, _currentPath[_currentNodeIndex].worldPosition) <= 0.2f)
                {
                    _currentNodeIndex++;
                }
            }
        }
    }
}
