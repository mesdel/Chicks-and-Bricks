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

    bool isActive;
    [SerializeField]
    private float numChicks;
    private float chickSpawnDelay = 3.0f;

    void Awake()
    {
        InvokeRepeating("SpawnHazards", 1.0f, 1.0f);
    }

    public void StartButton()
    {
        if(!isActive)
        {
            isActive = true;
            StartCoroutine(SpawnChick());
        }
    }

    private IEnumerator SpawnChick()
    {
        for(int i = 0; i < numChicks; i++)
        {
            Transform spawnTransform = chickStart.transform.Find("Spawn Position");
            SpawnChild(spawnTransform, chickPrefab);
            yield return new WaitForSeconds(chickSpawnDelay);
        }   
    }

    private void SpawnHazards()
    {
        for(int i = 0; i < hazards.transform.childCount; i++)
        {
            Transform spawnTransform = hazards.transform.GetChild(i).Find("Spawn Position");
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
