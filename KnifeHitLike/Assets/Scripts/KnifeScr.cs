using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KnifeHitLikeLib;

public class KnifeScr : MonoBehaviour
{
    public KnifeStatus Status { get; private set; }

    private float flyingSpeed;
    private Vector2 flyingWay;
	private Rigidbody2D RB2D;
	private GameManager gameManager;

    void Awake()
    {
		gameManager = GameManager.GM;
		Status = KnifeStatus.waiting;
		RB2D = GetComponent<Rigidbody2D>();
		flyingSpeed = gameManager.KnifeSpeed;
    }

    public void ThrowKnife()
    {
        Status = KnifeStatus.flying;
		flyingWay = -1 * RB2D.position;
    }

    public void Flying()
    {
		RB2D.MovePosition(RB2D.position + flyingWay * Time.deltaTime * flyingSpeed);
    }

	/*public void StuckInTarget(int _placeOnTarget)
	{
		Status = KnifeStatus.stucking;
	}*/

	void OnTriggerEnter2D(Collider2D _other) 
	{
		switch(_other.gameObject.tag)
		{
			case "Target":
				Status = KnifeStatus.stucking;
				this.transform.SetParent(_other.transform);
				break;
			case "Knife":
				if(Status == KnifeStatus.stucking)
				{
					gameManager.HitKnife();
				}
				break;
			case "Apple":
				break;
		}
	}
}
