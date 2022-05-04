using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool chicksActive;
    [SerializeField]
    private int numChicks;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int currScore;
    public bool gameOver { private set; get; }
    public static GameManager instance;
    private bool gamePaused;
    KeyCode pauseKey;

    private bool isTutorial;
    private Spawner[] spawners;

    void Awake()
    {
        isTutorial = SceneLoader.IsTutorial();
        Time.timeScale = 1;
        gamePaused = false;
        gameOver = false;
        currScore = 0;
        instance = this;

        // todo: possibly change
        // set pause key depending on editor/application
        pauseKey = KeyCode.Escape;
#if UNITY_EDITOR
        pauseKey = KeyCode.P;
#endif
        
        scoreText.text = currScore + "/" + numChicks + " chicks";
        spawners = gameObject.GetComponents<Spawner>();
        foreach(Spawner spawner in spawners)
        {
            spawner.StartHazards();
        }
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
            spawners[0].SpawnChicks(numChicks);
        }
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
