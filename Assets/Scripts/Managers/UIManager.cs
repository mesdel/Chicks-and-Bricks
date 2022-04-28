using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject winMenu;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void GameWin()
    {
        winMenu.SetActive(true);
    }
}
