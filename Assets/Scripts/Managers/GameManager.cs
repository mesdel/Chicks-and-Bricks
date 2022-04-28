using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    bool chicksActive;
    [SerializeField]
    private float numChicks;
    private float chickSpawnDelay = 3.0f;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int currScore;
    public bool gameOver { private set; get; }
    public static GameManager instance;
    private bool gamePaused;
    KeyCode pauseKey;

    void Awake()
    {
        Time.timeScale = 1;
        gamePaused = false;
        gameOver = false;
        currScore = 0;
        scoreText.text = currScore + "/" + numChicks + " chicks";
        InvokeRepeating("SpawnHazards", 1.0f, 1.0f);
        instance = this;

        pauseKey = KeyCode.Escape;
#if UNITY_EDITOR
        pauseKey = KeyCode.P;
#endif
    }

    private void Update()
    {

        if (!gameOver && Input.GetKeyDown(pauseKey))
        {
            if(gamePaused)
            {
                ResumeGame();
                gamePaused = false;
            }
            else
            {
                PauseGame();
                gamePaused = true;
            }
        }
    }

    public void StartButton()
    {
        if(!chicksActive)
        {
            chicksActive = true;
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

    public void ChickArrives()
    {
        currScore++;
        scoreText.text = currScore + "/" + numChicks + " chicks";
        if(currScore == numChicks)
        {
            // todo: winstate; pause game and prompt user to go to main menu or next level
            StopTime();
            UIManager.instance.GameWin();
        }
    }

    public void ChickFails()
    {
        // todo: failstate; pause game and prompt user to go to main menu or restart level
        gameOver = true;
        StopTime();
        UIManager.instance.GameOver();
    }

    private void StopTime()
    {
        gamePaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PauseGame()
    {
        StopTime();
        UIManager.instance.PauseGame();
    }
    public void ResumeGame ()
    {
        if(!gameOver)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            UIManager.instance.ResumeGame();
        }
    }
}
