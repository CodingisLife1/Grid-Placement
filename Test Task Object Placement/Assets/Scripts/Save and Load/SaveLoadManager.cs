using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;

    private string saveFileName = "placing_info.json";
    public List<BuildingSaveData> placedBuildings = new List<BuildingSaveData>();

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

    public void SaveBuildings()
    {
        string json = JsonUtility.ToJson(new SerializationWrapper(placedBuildings), true);

        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        File.WriteAllText(filePath, json);
    }
   

    public void LoadBuildings()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            SerializationWrapper wrapper = JsonUtility.FromJson<SerializationWrapper>(json);
            placedBuildings = wrapper.data;

            foreach (var objData in placedBuildings)
            {
                GameObject building = Instantiate(BuildingManager.instance.database.buildingsData[objData.id].BuildingPrefab, BuildingManager.instance.gameField);
                building.transform.localPosition = new Vector3(objData.x, objData.y, objData.z);
                BuildingObject buildingObject = building.GetComponent<BuildingObject>();
                buildingObject.id = objData.id;
                buildingObject.currentPosition = new Vector2Int(objData.column, objData.row);
                BuildingManager.instance.buildingPlacement.PlaceBuilding(new Vector2Int(objData.column, objData.row), BuildingManager.instance.database.buildingsData[objData.id].Size);
  
            }
        }
    }

    [System.Serializable]
    private class SerializationWrapper
    {
        public List<BuildingSaveData> data;

        public SerializationWrapper(List<BuildingSaveData> placedBuildings)
        {
            data = placedBuildings;
        }
    }
}
