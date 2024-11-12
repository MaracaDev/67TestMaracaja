using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] List<Rigidbody> bodyPartRagdoll = new List<Rigidbody>();
    [SerializeField] private Collider playerCollider; // Referência ao Collider do jogador

    public float minForce = 5f; // Força mínima a ser aplicada
    public float maxForce = 15f; // Força máxima a ser aplicada
    public float forceDuration = 1f; // Duração da aplicação da força
    public StackObject stackGameObject;

    private void Start()
    {
        // Preenche automaticamente a lista com todos os Rigidbody dos filhos
        bodyPartRagdoll.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    public void TakeDamage()
    {
        int x = Random.Range(0, bodyPartRagdoll.Count);
        Rigidbody rb = bodyPartRagdoll[x];

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomForce = Random.Range(minForce, maxForce);

        rb.AddForce(randomDirection * randomForce, ForceMode.Impulse);

        StartCoroutine(CanStack());
    }

    public void ActivateRagdoll()
    {
        foreach (Rigidbody item in bodyPartRagdoll)
        {
            item.isKinematic = false;

            // Ignora a colisão entre cada parte do Ragdoll e o jogador
            Collider ragdollCollider = item.GetComponent<Collider>();
            if (ragdollCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(ragdollCollider, playerCollider);
            }
        }

        TakeDamage();
        StartCoroutine(DeactivateRagdollAfterDelay()); // Inicia a contagem para desativar o Ragdoll
    }

    public void DeactivateRagdoll()
    {
        foreach (Rigidbody item in bodyPartRagdoll)
        {
            item.isKinematic = true;

            // Remove o CharacterJoint se existir
            CharacterJoint joint = item.GetComponent<CharacterJoint>();
            if (joint != null)
            {
                Destroy(joint); // Remove o CharacterJoint do objeto
            }
        }

        bodyPartRagdoll.Clear(); // Limpa a lista após a remoção
        StackManager.instance.AddToPile();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMove>().PerformPunch();
            StartCoroutine(ActivateRagdollAfterDelay());
        }
    }

    // Delay para poder pegar stackar
    IEnumerator CanStack()
    {
        yield return new WaitForSeconds(1);
    }

    // Desativa o ragdoll após 1.5 segundos
    IEnumerator DeactivateRagdollAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        DeactivateRagdoll();
    }

    IEnumerator ActivateRagdollAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        ActivateRagdoll();
    }
}
