using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class shuriken : MonoBehaviour
{

	[SerializeField]
	private float velocidade;


	private Vector2 direcao;
	private float tempo = 0.08f;
	private Rigidbody2D rb;


	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		rb.velocity = direcao * velocidade;
	}

	public void Inicializar (Vector2 _direcao)
	{
		direcao = _direcao;
	}

	void OnBecameInvisible ()
	{
		Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "inimigo") {
			StartCoroutine ("Destruir");
		}
	}

	IEnumerator Destruir ()
	{
		yield return new WaitForSeconds (tempo);
		Destroy (gameObject);
	}
}
