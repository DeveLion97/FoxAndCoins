using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPoint : MonoBehaviour {

	[SerializeField] private List<IPlayerRespawnListener> listListener = new List<IPlayerRespawnListener>();

	// Use this for initialization
	void Awake () {
		listListener = new List<IPlayerRespawnListener> ();
	}

	public void PlayerHitCheckPoint(){}

	private IEnumerator PlayerHitCheckPointCo(int bonus){
		yield break;
	}

	public void PlayerLeftCheckPoint(){}

	public void SpawnPlayer(Player Player){
		Player.RespawnAt (transform.position);

		foreach (var listener in listListener) {
			listener.OnPlayerRespawnInThisCheckPoint (this, Player);
		}
	}

	public void AssignOnjectToCheckPoint(IPlayerRespawnListener listener){
		listListener.Add (listener);
	}
}
