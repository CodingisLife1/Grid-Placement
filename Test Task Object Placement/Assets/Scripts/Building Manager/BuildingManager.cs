using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;

    public BuildingPlacement buildingPlacement;

    public int activeBuildingIndex;
    public GameObject activeBuilding;
    public BuildingsDatabase database;
    public GridLayoutGroup tilesGrid;
    public Transform gameField;
    public Tile[] tiles;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    } 
}
