using UnityEngine;

public class AttackTriggerAI : MonoBehaviour
{

    [SerializeField] private EnemyAI enemyAI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Player = collision.GetComponent<Player>();
        if (Player == null)
            return;

        enemyAI.Attack();
    }
}
