using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScr : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private float rotationSpeed;
    private int targetLifes;

    void Awake()
    {
        
    }

    public void Init(GameManager _gameManager, float _rotationSpeed)
    {
        gameManager = _gameManager;
		rotationSpeed = _rotationSpeed;
    }
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}
