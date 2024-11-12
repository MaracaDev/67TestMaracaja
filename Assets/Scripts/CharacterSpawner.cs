using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab; // Prefab do personagem a ser instanciado
    [SerializeField] private int maxCharacters = 10; // Número máximo de personagens que devem estar ativos na tela
    [SerializeField] private float rangeX = 10f; // Alcance no eixo X para spawn
    [SerializeField] private float rangeY = 10f; // Alcance no eixo Y para spawn
    private List<GameObject> characters = new List<GameObject>(); // Lista para manter o controle dos personagens instanciados

    void Start()
    {
        // Garante que haverá personagens ativos no início
        MaintainCharacterCount();
    }

    void Update()
    {
        // Verifica constantemente se o número de personagens na lista é menor que o máximo e reponha se necessário
        MaintainCharacterCount();
    }

    // Função que mantém o número correto de personagens na cena
    private void MaintainCharacterCount()
    {
        // Remove personagens destruídos da lista
        characters.RemoveAll(character => character == null);

        // Gera novos personagens se o número atual for menor que o máximo
        while (characters.Count < maxCharacters)
        {
            Vector3 spawnPosition = GetRandomPosition();
            GameObject newCharacter = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
            characters.Add(newCharacter);
        }
    }

    // Função para obter uma posição aleatória dentro do range X e Y
    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-rangeX, rangeX);
        float randomY = Random.Range(-rangeY, rangeY);
        return new Vector3(randomX, 0f, randomY); // Considerando um mundo 3D com Y fixo em 0
    }
}
