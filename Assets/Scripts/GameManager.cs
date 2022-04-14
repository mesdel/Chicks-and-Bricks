using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ChickStart;
    [SerializeField]
    private GameObject ChickGoal;
    [SerializeField]
    private GameObject ChickPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnChick", 2.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnChick()
    {
        Transform spawnTransform = ChickStart.transform.Find("Spawn Position");
        GameObject chick = GameObject.Instantiate(ChickPrefab, spawnTransform.position, spawnTransform.rotation);
        chick.transform.SetParent(ChickStart.transform);
    }
}
