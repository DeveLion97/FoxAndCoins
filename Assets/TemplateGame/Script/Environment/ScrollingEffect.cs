using UnityEngine;

public class ScrollingEffect : MonoBehaviour
{
    public float scrollSpeed = 2f; // Velocità di scorrimento
    private Vector2 startPosition; // Posizione iniziale
    public Vector2 endPosition; // Posizione finale
    private Vector2 targetPosition; // Destinazione attuale

    void Start()
    {
        // Memorizza la posizione iniziale e imposta la destinazione iniziale
        startPosition = transform.position;
        targetPosition = endPosition;
    }

    void Update()
    {
        // Muove l'oggetto verso la destinazione
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, scrollSpeed * Time.deltaTime);

        // Se ha raggiunto la destinazione, inverte il movimento
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            targetPosition = (targetPosition == endPosition) ? startPosition : endPosition;
        }
    }
}
