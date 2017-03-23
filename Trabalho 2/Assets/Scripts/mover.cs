using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour
{

	public float speed;
	public float x;
	public float timeToChange;
	private float tempTime;
	// Use this for initialization
	void Start ()
	{
		x = transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
	{

		tempTime += Time.deltaTime;
		if (tempTime >= timeToChange) {
			tempTime = 0;
			speed *= -1;

		}

		//Time.deltaTime calcula quanto tempo passou entre 1 frame e outro
		x -= speed * Time.deltaTime;

		transform.position = new Vector3 (x, transform.position.y, transform.position.z);
	}
}
