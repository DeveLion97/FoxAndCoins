using UnityEngine;

public class EnemyShootAnimation : MonoBehaviour
{

    public EnemyAI enemyAI;

    public void Shoot()
    {
        if (enemyAI == null) { return; }

        enemyAI.FireProjectile();

    }

}
