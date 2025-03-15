using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buildings", menuName = "ScriptableObjects")]
public class BuildingsDatabase : ScriptableObject
{
    public List<Building> buildingsData;
}


[Serializable]
public class Building
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; }
    [field: SerializeField] public GameObject BuildingPrefab { get; private set; }
}