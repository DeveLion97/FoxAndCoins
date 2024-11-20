using UnityEngine;

public class PlayerWarningTrigger : MonoBehaviour
{
    public LayerMask enemiesLayer; // Layer da rilevare
    public float rayDistance = 5f; // Distanza dei raycast

    public bool isWarned;

    public delegate void OnWarnPlayer();
    public static OnWarnPlayer onWarnPlayer;

    public delegate void OnNoWarnPlayer();
    public static OnNoWarnPlayer onNoWarnPlayer;

    private void Update()
    {
        // Raycast a destra
        Vector2 rightDirection = transform.right;
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, rightDirection, rayDistance, enemiesLayer);

        // Raycast a sinistra
        Vector2 leftDirection = -transform.right;
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, leftDirection, rayDistance, enemiesLayer);

        // Verifica se uno dei due raycast colpisce un oggetto nel layer specificato
        if (hitRight.collider != null || hitLeft.collider != null)
        {
            if (!isWarned)
            {
                onWarnPlayer?.Invoke();
                isWarned = true;
            }
        }
        else
        {
            if (isWarned)
            {
                onNoWarnPlayer?.Invoke();
                isWarned = false;
            }
        }

        // Disegna i raycast nel Scene View per renderli sempre visibili
        Debug.DrawRay(transform.position, rightDirection * rayDistance, Color.red);
        Debug.DrawRay(transform.position, leftDirection * rayDistance, Color.red);
    }

    private void OnDrawGizmosSelected()
    {

        // Disegna i raycast nel Scene View per renderli sempre visibili
        Debug.DrawRay(transform.position, Vector3.right * rayDistance, Color.red);
        Debug.DrawRay(transform.position, Vector3.left * rayDistance, Color.red);
    }
}
