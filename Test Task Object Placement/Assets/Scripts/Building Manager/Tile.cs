using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image tileImage;
    public Vector2Int tilePosition;
    private Color idleColor;
    private Color hoverColor = Color.black;

    private void Start()
    {
        tileImage = GetComponent<Image>();

        if (tileImage != null)
            idleColor = tileImage.color;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (BuildingManager.instance.activeBuilding != null)
        {
            if (!BuildingManager.instance.buildingPlacement.IsTilesOccupied(tilePosition, BuildingManager.instance.database.buildingsData[BuildingManager.instance.activeBuildingIndex].Size))
            {
                BuildingManager.instance.buildingPlacement.PlaceBuilding(tilePosition, BuildingManager.instance.database.buildingsData[BuildingManager.instance.activeBuildingIndex].Size);
                BuildingManager.instance.activeBuilding.GetComponent<BuildingObject>().currentPosition = new Vector2Int(tilePosition.x, tilePosition.y);
                SaveLoadManager.instance.placedBuildings.Add(new BuildingSaveData
                {
                    id = BuildingManager.instance.activeBuildingIndex,
                    x = BuildingManager.instance.activeBuilding.transform.localPosition.x,
                    y = BuildingManager.instance.activeBuilding.transform.localPosition.y,
                    z = BuildingManager.instance.activeBuilding.transform.localPosition.z,
                    row = tilePosition.y,
                    column = tilePosition.x,
                });

                SaveLoadManager.instance.SaveBuildings();

                BuildingManager.instance.activeBuilding = null;
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tileImage.color = hoverColor;

        if (BuildingManager.instance.activeBuilding != null)
        {
            BuildingManager.instance.activeBuilding.transform.localPosition = CalculateBuildingPosition(
                    BuildingManager.instance.tilesGrid.cellSize,
                    BuildingManager.instance.database.buildingsData[BuildingManager.instance.activeBuildingIndex].Size);
        }             
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tileImage.color = idleColor;
    }

    public Vector3 CalculateBuildingPosition(Vector2 cellSize, Vector2 buildingSize)
    {
        return new Vector3(
                transform.localPosition.x + ((cellSize.x / buildingSize.x) - cellSize.x),
                transform.localPosition.y + ((cellSize.y / buildingSize.y) + cellSize.y),
                0);
    }
}
