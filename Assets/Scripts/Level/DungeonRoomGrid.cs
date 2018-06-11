using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRoomGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointPrefab;
    [SerializeField]
    private float _gridTargetScale;

    private Node[,] _grid;
    private SpriteRenderer _spriteRend;

    [SerializeField]
    private bool _drawPoints;

    private int _gridWidth;
    private int _gridHeight;

    private Vector3 _oldPosition;
    private int _oldGridWidth = 0;
    private int _oldGridHeight = 0;

    private List<GameObject> _points = new List<GameObject>();

    private void Awake()
    {
        _spriteRend = GetComponent<SpriteRenderer>();

        CreateGrid();
    }

    public Vector2 WorldToGridCell(Vector3 position)
    {
        if (position.x < _grid[0, 0].worldPosition.x || position.x > _grid[_grid.GetLength(0) - 1, 0].worldPosition.x
            || position.y < _grid[0, 0].worldPosition.y || position.y > _grid[0, _grid.GetLength(1) - 1].worldPosition.y)
        {
            return new Vector2(-1, -1);
        }

        if ((Vector2)position == Vector2.zero)
        {
            return _grid[(int)Mathf.Ceil(_grid.GetLength(0) / 2.0f) - 1, (int)Mathf.Ceil(_grid.GetLength(1) / 2.0f) - 1].worldPosition;
        }

        position.x += _spriteRend.bounds.size.x / 2;
        position.y += _spriteRend.bounds.size.y / 2;

        int posX = (int)Mathf.Floor(position.x / _gridTargetScale);
        int posY = (int)Mathf.Floor(position.y / _gridTargetScale);

        return new Vector2(posX, posY);
    }

    public Vector2 GetPositionFromCell(int x, int y)
    {
        foreach (Node node in _grid)
        {
            if (node.gridX == x && node.gridY == y)
                return node.worldPosition;
        }

        return new Vector2(-1, -1);
    }

    public Node GetNodeFromPosition(Vector3 worldPosition)
    {
        Vector2 gridCell = WorldToGridCell(worldPosition);

        Debug.Log("Calculated Cell: " + gridCell);

        if (gridCell != new Vector2(-1, -1))
        {
            foreach (Node node in _grid)
            {
                if (node.gridX == gridCell.x && node.gridY == gridCell.y)
                {
                    return node;
                }
            }
        }

        return null;
    }

    public Node GetNode(int x, int y)
    {
        foreach (Node node in _grid)
        {
            if (node.gridX == x && node.gridY == y)
                return node;
        }

        return null;
    }

    public List<Node> GetNeighbourNodes(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < _gridWidth && checkY >= 0 && checkY < _gridHeight)
                {
                    neighbours.Add(_grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    private void CreateGrid()
    {
        float gridWidth = _spriteRend.bounds.size.x;
        float gridHeight = _spriteRend.bounds.size.y;

        _gridWidth = Mathf.RoundToInt(gridWidth / _gridTargetScale) + 1;
        _gridHeight = Mathf.RoundToInt(gridHeight / _gridTargetScale) + 1;

        ResizeDungeonRoom();

        if (_gridWidth != _oldGridWidth || _gridHeight != _oldGridHeight || transform.position != _oldPosition)
        {
            _oldPosition = transform.position;
            _oldGridWidth = _gridWidth;
            _oldGridHeight = _gridHeight;

            foreach (GameObject p in _points)
                Destroy(p);

            _grid = new Node[_gridWidth, _gridHeight];

            CreateGridPoints();
        }
    }

    private void CreateGridPoints()
    {
        Vector3 pointPosition = _spriteRend.bounds.min;

        for (int h = 0; h < _grid.GetLength(1); h++)
        {
            for (int w = 0; w < _grid.GetLength(0); w++)
            {
                GameObject newPoint = Instantiate(_pointPrefab, pointPosition, Quaternion.identity);
                _points.Add(newPoint);
                newPoint.SetActive(_drawPoints ? true : false);

                //When creating a new node, we do our collision check here
                //to determine if the node is walkable
                _grid[w, h] = new Node(true, pointPosition, w, h);

                pointPosition.x += _gridTargetScale;
            }
            pointPosition.y += _gridTargetScale;
            pointPosition.x = _spriteRend.bounds.min.x;
        }
    }

    private void ResizeDungeonRoom()
    {
        Vector3 scale = _spriteRend.transform.localScale;
        scale.x *= ((_gridWidth - 1) * _gridTargetScale) / _spriteRend.bounds.size.x;
        scale.y *= ((_gridHeight - 1) * _gridTargetScale) / _spriteRend.bounds.size.y;

        _spriteRend.transform.localScale = scale;
    }
}
