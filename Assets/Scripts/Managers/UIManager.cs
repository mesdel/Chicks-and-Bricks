using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject winMenu;

    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider ambiSlider;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if(SceneLoader.IsMainMenu())
            StartCoroutine(InitializeSliders());
    }

    private IEnumerator InitializeSliders()
    {
        yield return StartCoroutine(DataSaver.WaitForData());

        musicSlider.value = DataSaver.instance.musicVolume;
        sfxSlider.value = DataSaver.instance.sfxVolume;
        ambiSlider.value = DataSaver.instance.ambiVolume;
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

    // when the slider is edited, save update to Settings struct
    public void SaveVolumes()
    {
        DataSaver.instance.musicVolume = musicSlider.value;
        DataSaver.instance.sfxVolume = sfxSlider.value;
        DataSaver.instance.ambiVolume = ambiSlider.value;
    }
}
