using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static int MAIN_MENU_INDEX = 0;
    private static int TUTORIAL_INDEX = 1;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == MAIN_MENU_INDEX)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public static bool IsMainMenu()
    {
        return SceneManager.GetActiveScene().buildIndex == MAIN_MENU_INDEX;
    }

    public static bool IsTutorial()
    {
        return SceneManager.GetActiveScene().buildIndex == TUTORIAL_INDEX;
    }

    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_INDEX);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
