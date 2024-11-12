using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money;
    public int playerLevel = 1;
    public Transform stackPos;
    public StackManager manager;
    public static GameManager instance;
    public Renderer playerRenderer; // Referência ao Renderer do personagem para mudar a cor

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        money += coinsToAdd;
        UiManager1.instance.UpdateCoins(money);
    }



    public void LevelUp()
    {
        if (money >= 40)
        {
            money -= 40;
            // Aumenta o nível do jogador
            playerLevel++;
            Debug.Log("Nível do jogador: " + playerLevel);
            UiManager1.instance.UpdateLevel(playerLevel);
            UiManager1.instance.UpdateCoins(money);
            
            StackManager.instance.maxPileSize++;
            ChangePlayerColor();
        }
    }

    private void ChangePlayerColor()
    {
        if (playerRenderer != null)
        {
            // Escolhe uma nova cor aleatória (ou defina uma lista de cores específicas)
            Color newColor = new Color(Random.value, Random.value, Random.value);
            playerRenderer.material.color = newColor;

            Debug.Log("Cor do personagem alterada para: " + newColor);
        }
        else
        {
            Debug.LogWarning("Renderer do personagem não atribuído no GameManager.");
        }
    }
}
