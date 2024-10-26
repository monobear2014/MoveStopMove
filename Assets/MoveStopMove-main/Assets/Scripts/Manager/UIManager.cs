using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numBot;
    [SerializeField] TextMeshProUGUI rank;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject playAgainButton;

    private void Start()
    {
        UpdateUI();
        ActionManager.OnBotEDead += UpdateUI;
        ActionManager.OnBotMDead += UpdateUI;
        ActionManager.OnBotHDead += UpdateUI;
        ActionManager.OnGameOver += GameOverUI;
    }

    void UpdateUI()
    {
        numBot.text = LevelManager.Instance.GetTotalBot().ToString();
    }

    void UpdateRanking()
    {
        rank.text = "#" + LevelManager.Instance.GetTotalBot().ToString();
    }

    private void OnDestroy()
    {
        ActionManager.OnBotEDead -= UpdateUI;
        ActionManager.OnBotMDead -= UpdateUI;
        ActionManager.OnBotHDead -= UpdateUI;
        ActionManager.OnGameOver -= GameOverUI;
    }

    void GameOverUI()
    {
        UpdateRanking();
        gameOverPanel.SetActive(true);
        playAgainButton.SetActive(true);
    }
}
