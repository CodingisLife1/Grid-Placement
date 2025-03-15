using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject[] managers;

    private void Awake()
    {
        InitializeManagers();

    }

    private void InitializeManagers()
    {
        foreach (var manager in managers)
        {
            if (manager != null)
            {
                Instantiate(manager);
            }
        }
    }
}
