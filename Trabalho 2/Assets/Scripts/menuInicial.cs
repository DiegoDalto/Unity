using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuInicial : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void startGame ()
	{
		Application.LoadLevel ("fase 1");
	}

	public void GoToControl ()
	{
		//transição de fase pelo nome
		SceneManager.LoadScene ("Controllers");
	}

	public void GoToMenu ()
	{
		//transição de fase pelo nome
		SceneManager.LoadScene ("menu");
	}



}
