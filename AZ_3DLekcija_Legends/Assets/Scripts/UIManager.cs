using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthGlobe, manaGlobe;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private TMP_Text levelText;

    private void Update()
    {
        // healthGlobe.fillAmount = health.GetHealthRatio();
        healthGlobe.fillAmount = Mathf.Lerp(healthGlobe.fillAmount, health.GetHealthRatio(), 2 * Time.deltaTime);
    }
    public void UpdateXPSlider(float xpRatio)
    {
        xpSlider.value = xpRatio;
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = level.ToString();
    }
}