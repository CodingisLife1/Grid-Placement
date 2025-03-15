using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{

    [SerializeField] int gridRows;
    [SerializeField] int gridColumns;
    public bool[,] tilesOccupied;

    void Start()
    {
        SetTilesPosition();

        tilesOccupied = new bool[gridRows, gridColumns];

        SaveLoadManager.instance.LoadBuildings();
    }

    private void SetTilesPosition()
    {
        int x = 0, y = 0;
        for (int i = 0; i < BuildingManager.instance.tiles.Length; i++)
        {
            BuildingManager.instance.tiles[i].tilePosition = new Vector2Int(x, y);
            if (x < gridColumns - 1)
            {
                x += 1;
            }
            else
            {
                x = 0;
                y += 1;
            }
        }
    }

    public void PlaceBuilding(Vector2Int cellPos, Vector2Int buildingSize)
    {
        for (int i = (cellPos.y + buildingSize.y) - 1; i > cellPos.y - 1; i--)
        {
            for (int j = (cellPos.x - buildingSize.x) + 1; j < cellPos.x + 1; j++)
            {
                tilesOccupied[i, j] = true;
            }
        }
    }

    public bool IsTilesOccupied(Vector2Int cellPos, Vector2Int buildingSize)
    {
        for (int i = (cellPos.y + buildingSize.y) - 1; i > cellPos.y - 1; i--)
        {
            for (int j = (cellPos.x - buildingSize.x) + 1; j < cellPos.x + 1; j++)
            {
                try
                {
                    if (tilesOccupied[i, j])
                    {
                        return true;
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Debug.Log($"{i} {j}");
                    throw;
                }
            }
        }
        return false;
    }

    public void DeleteBuilding(Vector2Int cellPos, Vector2Int buildingSize)
    {
        for (int i = (cellPos.y + buildingSize.y) - 1; i > cellPos.y - 1; i--)
        {
            for (int j = (cellPos.x - buildingSize.x) + 1; j < cellPos.x + 1; j++)
            {
                tilesOccupied[i, j] = false;
            }
        }
    }

}
