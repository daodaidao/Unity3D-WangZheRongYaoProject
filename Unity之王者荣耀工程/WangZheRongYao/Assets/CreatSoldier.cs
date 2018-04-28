using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSoldier : MonoBehaviour {

	[SerializeField]
	private GameObject soldier;
	[SerializeField]
	private Transform starTran;

	[SerializeField]
	private Transform soldierParent;

	bool isCreatSoldier = true;

	public int soldierCount = 2;



	void Start(){
	
		StartCoroutine (Creat(0,1,5));
	}


	void CreatSmartSoldier(){

		GameObject obj =  Instantiate (soldier, starTran.position, Quaternion.identity)as GameObject ;
		obj.transform.parent = soldierParent;

		
	}

	private IEnumerator Creat (float time, float delyTime, float spwanTime){

		yield return new WaitForSeconds (time);

		while (isCreatSoldier) {
		
			for (int i = 0; i < soldierCount; i++) {
			
				CreatSmartSoldier ();
				yield return new WaitForSeconds (delyTime);
			
			}

			yield return new WaitForSeconds (spwanTime);




		}


	}


}
