using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartSoldier : MonoBehaviour {

	//寻路系统
	private NavMeshAgent nav;
	//跑步动画
	private Animation ani;

	//行走目标
	public Transform target;

	public Transform []towers;


	// Use this for initialization
	void Start () {
		//获取
		nav = GetComponent<NavMeshAgent> ();

		ani = GetComponent<Animation> ();

		target = GetTarget ();

	}
	
	// Update is called once per frame
	void Update () {
		print ("SmartSoldier Update");

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
				print ("塔还在");

				return towers[i];

			}

		}
		return null;
	}







}
