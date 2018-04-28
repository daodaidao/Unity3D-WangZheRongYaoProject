using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartSoldier : MonoBehaviour {
	
	private NavMeshAgent nav;

	private Animation ani;

	public Transform target;

	public Transform []towers;


	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();

		ani = GetComponent<Animation> ();


	}
	
	// Update is called once per frame
	void Update () {
		SoldierMove ();
	}

	void SoldierMove()
	{
		if(target == null)
		{
			target = GetTarget(); 

			return;
		}
		ani.CrossFade("Run");
		nav.SetDestination(target.position);


	}

	Transform GetTarget()
	{
		for (int i = 0; i < towers.Length; i++)
		{
			if (towers[i] != null)
			{
				return towers[i];
			}

		}
		return null;
	}







}
