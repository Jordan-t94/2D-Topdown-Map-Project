using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChecker : MonoBehaviour
{
    public static DungeonRoomGrid[] Grids = FindObjectsOfType<DungeonRoomGrid>();

    public static Node GetNodeFromWorldPosition(Vector3 position)
    {
        if (IsPositionAllowed(position))
        {
            for (int i = 0; i < Grids.Length; i++)
            {
                Node node = Grids[i].GetNodeFromPosition(position);
                if (node != null)
                    return node;
            }
        }

        return null;
    }

    public static Vector2 GetGridCellFromPosition(Vector3 position)
    {
        if (IsPositionAllowed(position))
        {
            for (int i = 0; i < Grids.Length; i++)
            {
                Node node = Grids[i].GetNodeFromPosition(position);
                if (node != null)
                    return new Vector2(node.gridX, node.gridY);
            }
        }

        return new Vector2(-1, -1);
    }

    public static int GetGridIndexFromPosition(Vector3 position)
    {
        for (int i = 0; i < Grids.Length; i++)
        {
            if (Grids[i].WorldToGridCell(position) != new Vector2(-1, -1))
            {
                return i;
            }
        }

        return -1;
    }

    public static bool IsPositionAllowed(Vector3 position)
    {
        foreach (DungeonRoomGrid grid in Grids)
        {
            if (grid.WorldToGridCell(position) != new Vector2(-1, -1))
            {
                return true;
            }
        }

        return false;
    }
}
