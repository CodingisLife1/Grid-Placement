using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseBuilding : MonoBehaviour
{
    [SerializeField] Button[] buildingButtons;
    [SerializeField] List<Image> buildingButtonsImages = new List<Image>();
    [SerializeField] Button placeButton;
    [SerializeField] Button deleteButton;
    

    private bool isBuildingChosen;
    [SerializeField] private bool isDeleteActive;

    void Start()
    {
        isBuildingChosen = false;

        for (int i = 0; i < buildingButtons.Length; i++)
        {
            int copyIndex = i;
            buildingButtonsImages.Add(buildingButtons[i].transform.parent.GetComponent<Image>());        
            buildingButtons[i].onClick.AddListener(() => SwitchBuilding(copyIndex));
        }

        placeButton.onClick.AddListener(PlaceBuilding);
        deleteButton.onClick.AddListener(() => isDeleteActive = true);
    }

    private void Update()
    {
        if (isDeleteActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                {
                    BuildingObject deletingBuilding = hit.collider.gameObject.GetComponent<BuildingObject>();
                    BuildingManager.instance.buildingPlacement.DeleteBuilding(deletingBuilding.currentPosition, BuildingManager.instance.database.buildingsData[deletingBuilding.id].Size);
                    foreach (var building in SaveLoadManager.instance.placedBuildings)
                    {
                        if (building.column == deletingBuilding.currentPosition.x && building.row == deletingBuilding.currentPosition.y)
                        {
                            SaveLoadManager.instance.placedBuildings.Remove(building);
                            break;
                        }
                    }
                    SaveLoadManager.instance.SaveBuildings();

                    Destroy(hit.collider.gameObject);
                    isDeleteActive = false;
                }
            }
        }
    }

    private void SwitchBuilding(int index)
    {
        for (int i = 0; i < buildingButtonsImages.Count; i++)
        {
            buildingButtonsImages[i].color = new Color(0, 1, 0, 0);
            
        }
        
        buildingButtonsImages[index].color = new Color(0, 1, 0, 1);

        isBuildingChosen = true;
        BuildingManager.instance.activeBuildingIndex = index;
    }

    private void PlaceBuilding()
    {
        if (isBuildingChosen)
        {
            BuildingManager.instance.activeBuilding = Instantiate(BuildingManager.instance.database.buildingsData[BuildingManager.instance.activeBuildingIndex].BuildingPrefab, BuildingManager.instance.gameField);
            BuildingManager.instance.activeBuilding.GetComponent<BuildingObject>().id = BuildingManager.instance.activeBuildingIndex;
        }
            
    }


}