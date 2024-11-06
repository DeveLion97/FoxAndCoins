using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinTile : MonoBehaviour
{


    public Tilemap tilemap;

    public Vector2 cellOffset;

    public int coinToAdd = 1;
    public int pointToAdd = 25;
    public GameObject Effect;
    public AudioClip sound;
    [Range(0, 1)]
    public float soundVolume = 0.5f;


    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica che l'oggetto che ha colpito sia il Player
        var player = other.gameObject.GetComponent<Player>();
        if (player == null)
            return;


        Vector3 playerPosition = other.bounds.center;

        Vector3Int cellPosition = tilemap.WorldToCell(playerPosition);

        cellPosition = new Vector3Int(cellPosition.x, (int)(cellPosition.y + cellOffset.y), cellPosition.z);

        SoundManager.PlaySfx(sound, soundVolume);

        GameManager.Instance.AddCoin(coinToAdd);
        GameManager.Instance.AddPoint(pointToAdd);

        Vector3 worldPosition = tilemap.CellToWorld(cellPosition);
        Vector3 spawnPosition = new Vector3(worldPosition.x + 0.5f, worldPosition.y + 0.5f, worldPosition.z);

        if (Effect != null)
        {
            
            Instantiate(Effect, spawnPosition, transform.rotation);
        }
            

        if (pointToAdd != 0)
            GameManager.Instance.ShowFloatingText(pointToAdd.ToString(), spawnPosition, Color.white);


        Debug.Log($"Posizione della cella colpita: {cellPosition}");

        if (tilemap.HasTile(cellPosition))
        {

            tilemap.SetTile(cellPosition, null);
        }

    }
}
