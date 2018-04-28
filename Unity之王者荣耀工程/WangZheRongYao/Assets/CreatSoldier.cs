using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSoldier : MonoBehaviour {

	[SerializeField]
	private GameObject soldierPrefab;
	[SerializeField]
	private Transform starTran;

	[SerializeField]
	private Transform soldierParent;

	[SerializeField]
	Transform[] towers;



	bool isCreatSoldier = true;

	public int soldierCount = 2;



	void Start(){
	
		StartCoroutine (Creat(0,1,5));
	}


	void CreatSmartSoldier(Transform starTRan,Transform[] towers){

		GameObject obj =  Instantiate (soldierPrefab, starTran.position, Quaternion.identity)as GameObject ;
		obj.transform.parent = soldierParent;

		SmartSoldier soldier = obj.GetComponent<SmartSoldier> ();
		soldier.towers = towers;
		
	}

	private IEnumerator Creat (float time, float delyTime, float spwanTime){

		yield return new WaitForSeconds (time);

		while (isCreatSoldier) {
		
			for (int i = 0; i < soldierCount; i++) {
			
				CreatSmartSoldier (starTran,towers);
				yield return new WaitForSeconds (delyTime);
			
			}

			yield return new WaitForSeconds (spwanTime);




		}


	}


}
