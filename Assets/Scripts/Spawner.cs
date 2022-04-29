using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject chickStart;
    [SerializeField]
    private GameObject chickPrefab;

    [SerializeField]
    private GameObject hazards;
    [SerializeField]
    private GameObject hazardPrefab;

    private float chickSpawnPeriod = 3.0f;
    [SerializeField]
    private bool tutorialSpawner;

    // Start is called before the first frame update
    void Start()
    {
        if(tutorialSpawner)
        {
            SpawnChicks(1);
        }
    }

    public void SpawnChicks(int numChicks)
    {
        StartCoroutine(SpawnChick(numChicks));
    }

    private IEnumerator SpawnChick(int numChicks)
    {
        do
        {
            for (int i = 0; i < numChicks; i++)
            {
                Transform spawnTransform = chickStart.transform.Find("Spawn Position");
                SpawnChild(spawnTransform, chickPrefab);
                yield return new WaitForSeconds(chickSpawnPeriod);
            }
        } while (tutorialSpawner);
    }

    public void StartHazards()
    {
        if (tutorialSpawner)
            return;
        InvokeRepeating(nameof(SpawnHazards), 1.0f, 1.0f);
    }

    private void SpawnHazards()
    {
        for (int i = 0; i < hazards.transform.childCount; i++)
        {
            Transform spawnTransform = hazards.transform.GetChild(i).Find("Spawn Position");
            SpawnChild(spawnTransform, hazardPrefab);
        }
    }

    // todo: object pooling for hazard projectiles

    public void SpawnChild(Transform spawnTransform, GameObject prefab)
    {
        GameObject child = GameObject.Instantiate(prefab, spawnTransform.position, spawnTransform.rotation);
        child.transform.SetParent(spawnTransform);
    }
}
