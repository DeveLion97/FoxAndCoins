using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterScrollingEffect : MonoBehaviour {

	public float speed;

	public List<GameObject> waterBlocks;
	int index;
	public float x;
	public float xOffset = 1.25f;

	// Use this for initialization

	void Start () {

		while(x <= 160.0f)
		{
			if (index == waterBlocks.Count - 1) { index = 0; }

			x += xOffset;
			GameObject go = Instantiate(waterBlocks[index], new Vector3(x, 0, 0), Quaternion.identity);
			go.transform.SetParent(transform);

			index++;
		}
	}
	
	
}
