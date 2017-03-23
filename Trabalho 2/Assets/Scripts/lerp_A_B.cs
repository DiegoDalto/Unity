﻿using UnityEngine;
using System.Collections;

public class lerp_A_B : MonoBehaviour
{
	public Transform posicaoInicial;
	// é onde queremos que nosso objeto começe o movimento
	public Transform posA;
	public Transform posB;

	public float speed;
	//velocidade

	private float startTime;
	// hora de inicio do movimento
	private float comprimentoDaJornada;
	public Transform Objeto;
	// objeto a ser movido

	private int idMovimento;

	void Start ()
	{
		Objeto.position = posicaoInicial.position;
		Movimento1 ();
	}

	void FixedUpdate ()
	{
		float dist = (Time.time - startTime) * speed;
		float jornada = dist / comprimentoDaJornada;
		if (idMovimento == 1) {
			Objeto.position = Vector3.Lerp (posA.position, posB.position, jornada);
			if (Objeto.position == posB.position) {
				Movimento2 ();
			}
		} else if (idMovimento == 2) {
			Objeto.position = Vector3.Lerp (posB.position, posA.position, jornada);
			if (Objeto.position == posA.position) {
				Movimento1 ();
			}
		}
	}

	void Movimento1 ()
	{
		idMovimento = 1;
		startTime = Time.time; 
		comprimentoDaJornada = Vector3.Distance (posA.position, posB.position);	
	}

	void Movimento2 ()
	{
		idMovimento = 2;
		startTime = Time.time; 
		comprimentoDaJornada = Vector3.Distance (posB.position, posA.position);	
	}

}
