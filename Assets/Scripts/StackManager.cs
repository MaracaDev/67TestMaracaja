using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public List<Transform> pile = new List<Transform>();
    public int height = 10;
    public float offset = 0.1f;
    public Vector2 rateRange = new Vector2(0.8f, 0.8f);
    public int maxPileSize = 3; // Limite m�ximo de objetos na pilha
    public static StackManager instance;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        // Cria o primeiro objeto e adiciona na lista
        Transform t = transform.GetChild(0); // Supondo que o primeiro item seja um cubo com y scale = 0.1f
        pile.Add(t);

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla "Q" foi pressionada
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddToPile();
        }

        // Atualiza as posi��es e rota��es para a simula��o do "Wobble"
        for (int i = 1; i < pile.Count; ++i)
        {
            float rate = Mathf.Lerp(rateRange.x, rateRange.y, (float)i / (float)pile.Count);
            pile[i].position = Vector3.Lerp(pile[i].position, pile[i - 1].position + (pile[i - 1].up * offset), rate);
            pile[i].rotation = Quaternion.Lerp(pile[i].rotation, pile[i - 1].rotation, rate);
        }
    }

    // Fun��o para adicionar um novo objeto � pilha
    public void AddToPile()
    {
        // Verifica se o limite da pilha foi atingido
        if (pile.Count >= maxPileSize)
        {
            Debug.Log("Limite da pilha atingido. N�o � poss�vel adicionar mais itens.");
            return;
        }

        else
        {
            // Instancia um novo objeto baseado no primeiro da pilha
            Transform newPileItem = GameObject.Instantiate(pile[0].gameObject).transform;

            // Define a posi��o e rota��o inicial do novo objeto
            newPileItem.position = pile[pile.Count - 1].position + (pile[pile.Count - 1].up * offset);
            newPileItem.rotation = pile[pile.Count - 1].rotation;

            // Adiciona o novo objeto � lista
            pile.Add(newPileItem);

            // Torna o objeto vis�vel
            newPileItem.gameObject.SetActive(true);

            Debug.Log("Novo item adicionado � pilha. Tamanho atual: " + pile.Count);
        }

        
    }

    // Fun��o para remover o �ltimo objeto da pilha
    public bool RemoveFromPile()
    {
        // Verifica se h� itens na pilha al�m do primeiro
        if (pile.Count > 1)
        {
            // Pega o �ltimo item da lista
            Transform lastPileItem = pile[pile.Count - 1];

            // Remove o �ltimo item da lista
            pile.RemoveAt(pile.Count - 1);

            // Remove o parenteamento do �ltimo item
            lastPileItem.parent = null;

            // Destr�i o �ltimo item
            Destroy(lastPileItem.gameObject);

            Debug.Log("�ltimo item removido da pilha. Tamanho atual: " + pile.Count);

            // Retorna true para indicar que houve remo��o
            return true;
        }
        else
        {
            Debug.Log("N�o h� itens suficientes na pilha para remover.");
            // Retorna false para indicar que n�o houve remo��o
            return false;
        }
    }

}
