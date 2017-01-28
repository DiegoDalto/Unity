using UnityEngine;
using System.Collections;

public class bau : MonoBehaviour
{

	public GameObject bauAberto;
	public GameObject bauFechado;

	private bool aberto;


	// Use this for initialization
	void Start ()
	{
		bauAberto.SetActive (false);
		bauFechado.SetActive (true);
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void interacao ()
	{
		//os comandos que quero quando interagir com esse objeto
		if (!aberto) {
			soundController.playSound (soundFx.OPEN);
			bauAberto.SetActive (true);
			bauFechado.SetActive (false);
			aberto = true;
		} 
	}

}
