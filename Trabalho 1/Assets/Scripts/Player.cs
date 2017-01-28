using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Reflection.Emit;
using System;

public class Player : MonoBehaviour
{

	private cam cam;

	public Animator anime;
	public Rigidbody2D RigidBodyPlayer;
	public bool walk;

	//recebe o valor de entrada(esquerda ou direita)
	private float movimentoX;
	private float movimentoY;

	public float maxSpeed;
	public bool facingRight;
	public int JumpForce;
	private bool DoubleJump;

	public bool wallCheck;

	public Transform GroundCheck;
	public bool Grounded;
	public LayerMask WhatIsGround;

	public Text moedas;
	private int scoreMoedas;

	public Text time;

	public Text vidas;
	private int qtdVidas;

	public float addSpeed;
	private float walkSpeed;


	public GameObject ObjetoInteracao;

	public bool escada;

	private bool acao;
	[SerializeField]
	private bool isKey;

	[SerializeField]
	private GameObject posicaoProjetil;
	[SerializeField]
	private GameObject projetil;

	void Start ()
	{
		isKey = false;
		cam = FindObjectOfType (typeof(cam)) as cam;
		scoreMoedas = 0;
		qtdVidas = 3;
		walkSpeed = maxSpeed;
	}


	void Update ()
	{   


		Resetar ();
		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			acao = true;
			anime.SetTrigger ("atirar");

		}
		int valor = (int)Time.time;
			
		time.text = valor + "";
		if (Input.GetButtonDown ("Fire1") && ObjetoInteracao != null) {
			ObjetoInteracao.SendMessage ("interacao", SendMessageOptions.DontRequireReceiver);
		}
		if (Input.GetButtonDown ("Fire1")) {
			anime.SetBool ("correndo", true);
			if (anime.GetBool ("walk") == true) {
				anime.SetBool ("correndo", true);
			}
			walkSpeed = maxSpeed + addSpeed;
		}
		if (Input.GetButtonUp ("Fire1")) {
			anime.SetBool ("correndo", false);
			if (anime.GetBool ("walk") == true) {
				anime.SetBool ("correndo", false);
			} 
			walkSpeed = maxSpeed;
		}	


		movimentoX = Input.GetAxis ("Horizontal");

		if (movimentoX != 0) {
			transform.SetParent (null);
		}

		if (escada == true) {

			if (movimentoX != 0) {
				anime.SetBool ("escada", false);
				anime.SetBool ("upDown", false);
				RigidBodyPlayer.gravityScale = 1;
				escada = false;
			}

			movimentoY = Input.GetAxis ("Vertical");
		

			if (movimentoY != 0) {
				RigidBodyPlayer.velocity = new Vector3 (RigidBodyPlayer.velocity.x, movimentoY * maxSpeed);
				anime.SetBool ("escada", true);
				anime.SetBool ("upDown", true);
				RigidBodyPlayer.gravityScale = 0;
			} else {
				anime.SetBool ("upDown", false);
			}
		}
	
		Grounded = Physics2D.OverlapCircle (GroundCheck.position, 0.1f, WhatIsGround);


		if (Grounded) {
			DoubleJump = false;
		}

		if (Input.GetButtonDown ("Jump") && (Grounded || !DoubleJump) && !wallCheck) {

			anime.SetBool ("escada", false);
			anime.SetBool ("upDown", false);
			RigidBodyPlayer.gravityScale = 1;
			escada = false;

			soundController.playSound (soundFx.JUMP);
			RigidBodyPlayer.velocity = new Vector2 (0, 0);
			RigidBodyPlayer.AddForce (new Vector2 (0, JumpForce));

			if (!Grounded && !DoubleJump) {
				DoubleJump = true;
			}
		}

		if (movimentoX > 0 && !facingRight) {
			Flip ();
		} else if (movimentoX < 0 && facingRight) {
			Flip ();
		}

		if (!anime.GetCurrentAnimatorStateInfo (0).IsTag ("atirar")) {
			if (!wallCheck) {
				RigidBodyPlayer.velocity = new Vector2 (movimentoX * walkSpeed, RigidBodyPlayer.velocity.y);
			}
		}
		if (movimentoX != 0) {
			walk = true;
		} else {
			walk = false;

		}

		anime.SetBool ("walk", walk);

		anime.SetBool ("Grounded", Grounded);
		anime.SetFloat ("speedY", RigidBodyPlayer.velocity.y);
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		switch (col.tag) {
		case "ajusteCamera":
			cam.ajusteY = 0;
			cam.comLerp = true;
			break;
		case "interacao":
			ObjetoInteracao = col.gameObject;
			break;
		case "coin":
			soundController.playSound (soundFx.COIN);
			Destroy (col.gameObject);
			scoreMoedas = scoreMoedas + 1;
			moedas.text = scoreMoedas.ToString ();
			break;
		case "chaveBau":
			Destroy (col.gameObject);
			isKey = true;
			break;
		case"bau":
			if (isKey) {
				ObjetoInteracao = col.gameObject;
			}
			break;
		case "death":
			if (qtdVidas > 0) {
				soundController.playSound (soundFx.DEATH);
				qtdVidas = qtdVidas - 1;
				vidas.text = qtdVidas.ToString ();
				transform.position = new Vector3 (0, 0, 0);
			} else {
				Application.LoadLevel (2);

			}
			break;
		
		}
	}

	void FixedUpdate ()
	{
		Acao ();
	}

	void Resetar ()
	{
		acao = false;
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//método para disparar gatilho ao entrar em um colisor (ao entrar em colisao com um trigger)
	//	void OnTriggerEnter2D (Collider2D col)
	//	{
	//disparando evento utilizando tags
	//if (col.tag == "bloco") {
	//}

	//desaparece o objeto ao colidir
	//col.gameObject.SetActive (false);

	//destroi o objeto ao colidir
	//Destroy (col.gameObject);
	//	}
	//método para disparar gatilho ao sair em um colisor (ao sair da colisao com um trigger)
	void OnTriggerExit2D (Collider2D col)
	{
		wallCheck = false;
		if (col.tag == "interacao") {
			ObjetoInteracao = null;
		}

		if (col.tag == "escada") {
			escada = false;
			anime.SetBool ("escada", false);
			anime.SetBool ("upDown", false);
			RigidBodyPlayer.gravityScale = 1;
		}

	}
	// vai ficar executando enquanto estiver colidindo
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag != "gatilhos" && col.tag != "objeto" && col.tag != "ajusteCamera" && col.tag != "interacao" && col.tag != "escada") {
			wallCheck = true;
		}
		if (col.tag == "escada") {
			escada = true;
		}
	}



	void OnCollisionExit2D (Collision2D col)
	{
		
		if (col.gameObject.tag == "plataformaMovel") {
			transform.SetParent (null);
		}
	}

	void OnCollisionStay2D (Collision2D col)
	{
		if (col.gameObject.tag == "death") {
			if (qtdVidas > 0) {
				soundController.playSound (soundFx.DEATH);
				qtdVidas = qtdVidas - 1;
				vidas.text = qtdVidas.ToString ();
				transform.position = new Vector3 (0, 0, 0);
			} else {
				//gameover
			}
		}
		if (col.gameObject.tag == "plataformaMovel" && movimentoX == 0) {
			transform.SetParent (col.gameObject.transform);
		}
	}

	void Acao ()
	{
		if (acao && !anime.GetCurrentAnimatorStateInfo (0).IsTag ("atirar")) {
			anime.SetTrigger ("atirar");
			//RigidBodyPlayer.velocity = Vector2.zero;
			AcaoAtirar ();
		}
	}

	private void AcaoAtirar ()
	{
		if (anime.GetBool ("correndo") == false) {
		
			GameObject tempProjetil = (GameObject)(Instantiate (projetil, posicaoProjetil.transform.position, Quaternion.identity));
			if (facingRight) {
				tempProjetil.GetComponent<shuriken> ().Inicializar (Vector2.right); 
			} else {
				tempProjetil.GetComponent<shuriken> ().Inicializar (Vector2.left);
			}
		}
	}

	//método para disparar gatilho ao entrar em um colisor (ao entrar em colisao com um componente solido)
	//	void OnCollisionEnter2D (Collision2D col)
	//	{
	//colidindo objetos solidos utilizando tags
	//if (col.gameObject.tag == "bloco") {
			
	//}
	//	}
	//método para disparar gatilho ao sair em um colisor (ao sair da colisao com um componente solido)
	//	void OnCollisionExit2D (Collision2D col)
	//	{
	//	}
	// vai ficar executando enquanto estiver colidindo
	//	void OnCollisionStay (Collision2D col)
	//	{
	//	}
}
