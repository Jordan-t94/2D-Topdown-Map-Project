using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Text _debugText;

    [SerializeField]
    private float _walkSpeed = 5f;
    [SerializeField]
    private float _sprintSpeed = 8f;
    [SerializeField]
    private StatController _sprintController;

    private DungeonRoomGrid[] _grids;
    private Vector3 _movementInput;

    public StatController SprintController
    {
        get
        {
            return _sprintController;
        }
        private set
        {
            _sprintController = value;
        }
    }

    private void Awake()
    {
        _grids = FindObjectsOfType<DungeonRoomGrid>();
        _sprintController.Initialize();
    }

    private void Update()
    {
        _movementInput = Vector3.zero;
        _movementInput.z = 0;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _movementInput.x = horizontal;
        _movementInput.y = vertical;

        if (_movementInput != Vector3.zero)
        {
            _sprintController.Update("Sprint", _movementInput);
            float speed = _sprintController.IsCurrentlyUsing ? _sprintSpeed : _walkSpeed;
            Vector3 newMovement = _movementInput.normalized * speed * Time.deltaTime;

            Vector3 oldPos = transform.position;
            transform.position += newMovement;

            if (!GridChecker.IsPositionAllowed(transform.position))
            {
                transform.position = oldPos;
            }
            else
            {
                _debugText.text = "Grid: " + (GridChecker.GetGridIndexFromPosition(transform.position) + 1).ToString() + GridChecker.GetGridCellFromPosition(transform.position).ToString();
            }
        }
        else
        {
            _sprintController.Update("Sprint", Vector3.zero);
        }
    }
}
