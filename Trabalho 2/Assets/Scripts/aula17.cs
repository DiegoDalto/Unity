using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class aula17 : MonoBehaviour
{

	public int up;
	public int fix;

	public TextMesh uptxt;
	public TextMesh fixtxt;

	public float tempTime;
	public float tempTime2;



	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{ 
		tempTime += Time.deltaTime;
		up++;
		uptxt.text = tempTime.ToString ();
	
	}
	//ele e executado apos o termino do update
	void LateUpdate ()
	{
		tempTime2 += Time.deltaTime;
		fix++;
		fixtxt.text = tempTime2.ToString ();
	}
	//ela trabalha com framerate fixo
	void FixedUpdate ()
	{
		
	}


}
