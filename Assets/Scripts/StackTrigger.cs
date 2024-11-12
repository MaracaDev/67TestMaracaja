using UnityEngine;

public class StackTrigger : MonoBehaviour
{
    public StackManager stackManager; // Referência ao script StackManager
    public float interval = 0.5f;     // Intervalo entre cada remoção do stack
    public int coinAmount = 10;       // Quantidade de moedas a adicionar por remoção
    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador entrou no trigger
        {
            isPlayerInTrigger = true;
            InvokeRepeating("RemoveAndAddCoins", 0f, interval);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador saiu do trigger
        {
            isPlayerInTrigger = false;
            CancelInvoke("RemoveAndAddCoins");
        }
    }

    private void RemoveAndAddCoins()
    {
        if (stackManager != null)
        {
            // Verifica se houve remoção bem-sucedida
            bool itemRemoved = stackManager.RemoveFromPile();

            if (itemRemoved)
            {
                // Adiciona moedas ao jogador somente se houve remoção
                GameManager.instance.AddCoins(coinAmount);
            }
            else
            {
                Debug.Log("Nenhum item foi removido da pilha, moedas não adicionadas.");
            }
        }
        else
        {
            Debug.LogWarning("StackManager não atribuído no StackTrigger.");
        }
    }
}


