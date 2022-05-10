using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScr : MonoBehaviour
{
	private GameManager gameManager;

	void Awake()
	{
		gameManager = GameManager.GM;
	}

	void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Knife")
		{
            this.gameObject.SetActive(false);
			gameManager.AppleHit();
		}
	}
}
