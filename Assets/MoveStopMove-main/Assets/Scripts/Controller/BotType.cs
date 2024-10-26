using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotType : MonoBehaviour
{
    // Định nghĩa enum BotType với các loại A, B, và C
    public enum BotTypes
    {
        Easy,
        Medium,
        Hard
    }

    // Biến để chọn loại Bot
    public BotTypes botType;

    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        switch (botType)
        {
            case BotTypes.Easy:
                BotE();
                break;
            case BotTypes.Medium:
                BotM();
                break;
            case BotTypes.Hard:
                BotH();
                break;
        }
    }

    void BotE()
    {
        ActionManager.OnBotEDead?.Invoke();
    }

    void BotM()
    {
        ActionManager.OnBotMDead?.Invoke();
    }

    void BotH()
    {
        ActionManager.OnBotHDead?.Invoke();
    }
}
