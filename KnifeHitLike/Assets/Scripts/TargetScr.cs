using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScr : MonoBehaviour
{
    private GameManager gameManager;
    private int targetLifes;

    void Awake()
    {
		gameManager = GameManager.GM;
		targetLifes = gameManager.TargetLifes;
		Vibration.Init();
    }

    public void SpinTheTarget()
    {
		transform.Rotate(0, 0, -1 * Time.deltaTime * gameManager.TargetRotationSpeed);
    }

	void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.gameObject.tag == "Knife")
		{
			Vibration.Vibrate();
			targetLifes--;
			if (targetLifes + gameManager.StuckedKnifesCount <= 0)
			{
				BlowUp();
				gameManager.TargetBlowingUp();
			}
		}
	}

	void BlowUp()
	{
		this.gameObject.SetActive(false);
	}
}