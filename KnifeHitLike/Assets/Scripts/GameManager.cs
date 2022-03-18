using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TargetScr targetPref;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private int targetLifes;

    private TargetScr target;

    void Start()
    {
        LoadLevel();
    }

    void LoadLevel()
    {
		//target = Instatiate();
    }
}
