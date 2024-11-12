using UnityEngine;

public class StackTrigger : MonoBehaviour
{
    public StackManager stackManager; // Refer�ncia ao script StackManager
    public float interval = 0.5f;     // Intervalo entre cada remo��o do stack
    public int coinAmount = 10;       // Quantidade de moedas a adicionar por remo��o
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
            // Verifica se houve remo��o bem-sucedida
            bool itemRemoved = stackManager.RemoveFromPile();

            if (itemRemoved)
            {
                // Adiciona moedas ao jogador somente se houve remo��o
                GameManager.instance.AddCoins(coinAmount);
            }
            else
            {
                Debug.Log("Nenhum item foi removido da pilha, moedas n�o adicionadas.");
            }
        }
        else
        {
            Debug.LogWarning("StackManager n�o atribu�do no StackTrigger.");
        }
    }
}


