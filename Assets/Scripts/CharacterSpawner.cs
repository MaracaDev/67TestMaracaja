using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab; // Prefab do personagem a ser instanciado
    [SerializeField] private int maxCharacters = 10; // N�mero m�ximo de personagens que devem estar ativos na tela
    [SerializeField] private float rangeX = 10f; // Alcance no eixo X para spawn
    [SerializeField] private float rangeY = 10f; // Alcance no eixo Y para spawn
    private List<GameObject> characters = new List<GameObject>(); // Lista para manter o controle dos personagens instanciados

    void Start()
    {
        // Garante que haver� personagens ativos no in�cio
        MaintainCharacterCount();
    }

    void Update()
    {
        // Verifica constantemente se o n�mero de personagens na lista � menor que o m�ximo e reponha se necess�rio
        MaintainCharacterCount();
    }

    // Fun��o que mant�m o n�mero correto de personagens na cena
    private void MaintainCharacterCount()
    {
        // Remove personagens destru�dos da lista
        characters.RemoveAll(character => character == null);

        // Gera novos personagens se o n�mero atual for menor que o m�ximo
        while (characters.Count < maxCharacters)
        {
            Vector3 spawnPosition = GetRandomPosition();
            GameObject newCharacter = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
            characters.Add(newCharacter);
        }
    }

    // Fun��o para obter uma posi��o aleat�ria dentro do range X e Y
    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-rangeX, rangeX);
        float randomY = Random.Range(-rangeY, rangeY);
        return new Vector3(randomX, 0f, randomY); // Considerando um mundo 3D com Y fixo em 0
    }
}
