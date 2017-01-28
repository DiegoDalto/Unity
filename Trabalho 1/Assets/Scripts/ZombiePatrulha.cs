using UnityEngine;
using System.Collections;

public class ZombiePatrulha : MonoBehaviour
{
	public Transform[] pontosPatrulha;
	int currentPonto;
	public float speed = 0.5f;
	public float tempo = 2f;
	public float sight = 3f;
	public float force;

	Animator anime;
	// Use this for initialization
	void Start ()
	{
		anime = GetComponent<Animator> ();
		StartCoroutine ("Patrulha");
		anime.SetBool ("walking", true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.localScale.x * Vector2.right, sight);
		if (hit.collider != null && hit.collider.tag == "Player") {
			GetComponent<Rigidbody2D> ().AddForce (Vector3.up * force + (hit.collider.transform.position - transform.position) * force);
		}
	}

	IEnumerator Patrulha ()
	{
		while (true) {
			if (transform.position.x == pontosPatrulha [currentPonto].position.x) {
				currentPonto++;
				anime.SetBool ("walking", false);
				yield return new WaitForSeconds (tempo);
				anime.SetBool ("walking", true);
			}
			if (currentPonto >= pontosPatrulha.Length) {
				currentPonto = 0;
			}
			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (pontosPatrulha [currentPonto].position.x, transform.position.y), speed);

			/*if (transform.position.x > pontosPatrulha [currentPonto].position.x) {
				transform.localScale = new Vector3 (-1, 1, 1);
			} else if (transform.position.x < pontosPatrulha [currentPonto].position.x) {
		
				transform.localScale = Vector3.one;
			}*/

			yield return null;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "inimigo") {
			anime.SetBool ("death", true);
			Destroy (this.gameObject, 2f);
		}
	}

}
