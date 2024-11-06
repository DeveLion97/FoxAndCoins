using UnityEngine;
using System.Collections;

public class BirdDetectPlayerHelper : MonoBehaviour {
	public BirdAI bird;

	[SerializeField] private bool isDetected;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.GetComponent<Player> () == null)
			return;

		if (isDetected)
		{
			return;
		}

		if (!bird.isDead && bird.chasePlayer)
		{
			isDetected = true;
            bird.isChasing = true;
			bird.Attack();
        }
			
	}
}
