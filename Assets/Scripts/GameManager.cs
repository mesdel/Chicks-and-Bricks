using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject chickStart;
    [SerializeField]
    private GameObject chickGoal;
    [SerializeField]
    private GameObject chickPrefab;

    [SerializeField]
    private GameObject hazards;
    [SerializeField]
    private GameObject hazardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnChick", 2.0f, 3.0f);
        InvokeRepeating("SpawnHazards", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnChick()
    {
        Transform spawnTransform = chickStart.transform.Find("Spawn Position");
        SpawnChild(spawnTransform, chickPrefab);
    }

    private void SpawnHazards()
    {
        for(int i = 0; i < hazards.transform.childCount; i++)
        {
            Transform spawnTransform = hazards.transform.GetChild(i);
            SpawnChild(spawnTransform, hazardPrefab);
        }
    }

    // todo: object pooling for hazard projectiles

    private void SpawnChild(Transform spawnTransform, GameObject prefab)
    {
        GameObject child = GameObject.Instantiate(prefab, spawnTransform.position, spawnTransform.rotation);
        child.transform.SetParent(spawnTransform);
    }
}
