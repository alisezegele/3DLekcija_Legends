using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthGlobe, manaGlobe;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerMana mana;
    [SerializeField] private TMP_Text levelText, levelUpText, finalLevelText;
    [SerializeField] private GameObject mainMenuPanel, statsPanel, winPanel, pausePanel, gameOverPanel;
    [SerializeField] private Collider houseTrigger;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        statsPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        statsPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        statsPanel.SetActive(false);
        Time.timeScale = 0f;
    }
    private void Update()
    {
        healthGlobe.fillAmount = Mathf.Lerp(healthGlobe.fillAmount, health.GetHealthRatio(), 2 * Time.deltaTime);
        manaGlobe.fillAmount = Mathf.Lerp(manaGlobe.fillAmount, mana.GetManaRatio(), 2 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void UpdateXPSlider(float xpRatio)
    {
        xpSlider.value = xpRatio;
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = level.ToString();
        ShowLevelUpText();
    }

    public void ShowLevelUpText()
    {
        StartCoroutine(LevelUpMessage());
    }

    IEnumerator LevelUpMessage()
    {
        levelUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        levelUpText.gameObject.SetActive(false);
    }
    
    public void TriggerHouseEntered()
    {
        int playerLevel = LevelManager.instance.GetCurrentLevel();
        finalLevelText.text = "LEVEL REACHED: " + playerLevel;
        
        winPanel.SetActive(true);
        statsPanel.SetActive(false);
        Time.timeScale = 0f;
        SoundManager.instance.PlayWinSound();
        SoundManager.instance.StopAmbience();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
        statsPanel.SetActive(false);
        SoundManager.instance.PlayGameOver();
        SoundManager.instance.StopAmbience();
    }
}