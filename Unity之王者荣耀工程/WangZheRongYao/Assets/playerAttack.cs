using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

	[SerializeField]
	private ParticleSystem fire1;

	[SerializeField]
	private ParticleSystem fire2;


	private Animator ani;

	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator> ();
	}
	 
	public void Atk1()
	{
		ani.SetInteger ("state", AnimState.ATTACK1);
		print("Atk1方法");

	}

	public void Atk2()
	{
		ani.SetInteger ("state", AnimState.ATTACK2);
		print("Atk2方法");

	}

	public void Dance()
	{
		ani.SetInteger ("state", AnimState.DANCE);
		print("Dance方法");
	}

	public void EffectPlayer1()
	{
		fire1.Play();
		print("Play1方法");


	}

	public void EffectPlayer2()
	{
		fire1.Play ();
		print("Play2方法");

	}
}
