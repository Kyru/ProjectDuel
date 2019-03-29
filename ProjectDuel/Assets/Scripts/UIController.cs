using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class UIController: MonoBehaviour
{
	[SerializeField] private Text HP_Label_Blue;
	[SerializeField] private GameObject blue_crab;
	
	void Update() 
	{
		HP_Label_Blue.text= blue_crab.GetComponent<PlayerCharacter>().get_health().ToString();
	}
}
