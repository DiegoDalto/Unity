using UnityEngine;
using System.Collections;

public class carrinho : MonoBehaviour
{

	private Player player;
	public float forca;
	// Use this for initialization
	void Start ()
	{
		player = FindObjectOfType (typeof(Player)) as Player;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	void interacao ()
	{
		if (player.facingRight == true && forca < 0) {
			forca *= -1;
			
		} else if (player.facingRight == false && forca > 0) {
			forca *= -1;
		}
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (forca, 0));
	}
}
