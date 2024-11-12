using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiManager1 : MonoBehaviour
{
    public TMP_Text coins;
    public TMP_Text level;
    public static UiManager1 instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateCoins(int value)
    {
        coins.text = "coins: " + value;
    }

    public void UpdateLevel(int value)
    {
        level.text = "Level: " + value;
    }
}
