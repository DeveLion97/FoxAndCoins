using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float scrollSpeed = 2f; // Velocità di scorrimento
    private Vector2 startPosition; // Posizione iniziale
    public float spriteWidth; // Larghezza dello sprite

    void Start()
    {
        // Memorizza la posizione iniziale
        startPosition = transform.position;

        // Calcola la larghezza dello sprite tenendo conto del fattore di scala dell'oggetto
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
    }

    void Update()
    {
        // Calcola il nuovo offset in base alla velocità di scorrimento
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, spriteWidth);

        // Aggiorna la posizione del GameObject per simulare lo scrolling
        transform.position = startPosition + Vector2.left * newPosition;
    }
}
