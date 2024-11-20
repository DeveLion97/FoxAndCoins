using UnityEngine;
using UnityEngine.UIElements;

public class AttackTriggerAI : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI; // Riferimento allo script EnemyAI
    [SerializeField] private float rayDistance = 5f; // Distanza del raycast
    [SerializeField] private LayerMask playerLayer; // Layer di collisione per rilevare il giocatore
    [SerializeField] private float attackCooldown = 2f; // Tempo di attesa tra un attacco e l'altro (in secondi)

    private float nextAttackTime = 0f; // Tempo al quale è possibile eseguire il prossimo attacco

    private void Update()
    {
        // Determina la direzione del raycast in base all'orientamento dell'oggetto (assumendo che la scala determini l'orientamento)
        Vector2 rayDirection = transform.right * (transform.localScale.x >= 0 ? -1 : 1);

        // Esegui il raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance, playerLayer);

        // Controlla se il raycast colpisce un oggetto nel layer specificato
        if (hit.collider != null)
        {
            // Cerca il componente Player nell'oggetto colpito
            var player = hit.collider.GetComponent<Player>();
            if (player != null && Time.time >= nextAttackTime)
            {
                enemyAI.Attack(); // Esegui l'attacco se il player è rilevato
                nextAttackTime = Time.time + attackCooldown; // Imposta il tempo per il prossimo attacco
            }
        }

        // Disegna il raycast nel Scene View per renderlo sempre visibile
        Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.yellow);
    }

    private void OnDrawGizmosSelected()
    {

        // Determina la direzione del raycast in base all'orientamento dell'oggetto (assumendo che la scala determini l'orientamento)
        Vector2 rayDirection = transform.right * (transform.localScale.x >= 0 ? -1 : 1);

        // Disegna il raycast nel Scene View per renderlo sempre visibile
        Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.yellow);
    }
}
