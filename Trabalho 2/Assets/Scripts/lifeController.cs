using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class lifeController : MonoBehaviour
{

	public float maxLife = 130;
	public float life;
	public Scrollbar lifeBar;

	// Use this for initialization
	void Start ()
	{
		life = maxLife;
		lifeBar.size = (life / maxLife);
	}
	
	// Update is called once per frame
	public void hit (float hit)
	{
		life -= hit;
		if (life < 0) {
			life = 0;
		}
		lifeControl ();
	}

	public void fullHealing ()
	{
		life = maxLife;
		lifeControl ();
	}

	private void lifeControl ()
	{
		lifeBar.size = (life / maxLife);
	}
}

 