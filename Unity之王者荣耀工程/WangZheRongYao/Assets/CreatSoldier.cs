using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSoldier : MonoBehaviour {
	
	[SerializeField]
//	solderPrefab
	private GameObject solder;

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

	//1.小兵生成位置 2. 目标
	void CreatSmartSoldier(Transform starTRan,Transform[] towers){

		print ("CreatSmartSoldier");

		GameObject obj =  Instantiate (solder, starTran.position, Quaternion.identity)as GameObject ;
		obj.transform.parent = soldierParent;

		SmartSoldier soldier = obj.GetComponent<SmartSoldier> ();

		soldier.towers = towers;
		
	}

	//生成小兵的 时间， 延迟时间，
	private IEnumerator Creat (float time, float delyTime, float spwanTime){

		yield return new WaitForSeconds (time);

		while (isCreatSoldier) {
		
			for (int i = 0; i < soldierCount; i++) {
			
				CreatSmartSoldier (starTran,towers);


				yield return new WaitForSeconds (delyTime);
			
			}
			//下一个生成时间
			yield return new WaitForSeconds (spwanTime);




		}


	}


}
