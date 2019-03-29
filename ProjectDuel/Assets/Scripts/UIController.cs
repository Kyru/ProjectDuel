using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController: MonoBehaviour
{
	
	[SerializeField] private GameObject blue_hearts;
	[SerializeField] private GameObject yellow_hearts;
	[SerializeField] private Sprite empty_heart;
	private List <GameObject> blue_hearts_list;
	private List <GameObject> yellow_hearts_list;

	
	void Start()
	{
		Messenger<int>.AddListener(GameEvent.BLUE_HURT, blue_remove_heart);
		Messenger<int>.AddListener(GameEvent.YELLOW_HURT, yellow_remove_heart);

		blue_hearts_list = new List<GameObject>();
		foreach(Transform child in blue_hearts.transform)
		{
			blue_hearts_list.Add(child.gameObject);
		}

		yellow_hearts_list = new List<GameObject>();
		foreach(Transform child in yellow_hearts.transform)
		{
			yellow_hearts_list.Add(child.gameObject);
		}
	}

	public void blue_remove_heart(int health)
	{
		blue_hearts_list[health].GetComponent<Image>().sprite=empty_heart;
	}
	public void yellow_remove_heart(int health)
	{
		yellow_hearts_list[health].GetComponent<Image>().sprite=empty_heart;
	}

	
}
