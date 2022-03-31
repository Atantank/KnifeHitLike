using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KnifeHitLikeLib;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas mainScreen;
    [SerializeField] Canvas gameScreen;
    [SerializeField] Canvas gameMenu;
    [SerializeField] GameObject gameObjects;

    [SerializeField] private TargetScr targetPref;
	[SerializeField] private float targetRotationSpeed;
    public float TargetRotationSpeed { get => targetRotationSpeed; }
	[SerializeField] private int targetLifes;
    public int TargetLifes { get => targetLifes; }
    private TargetScr target;

    [SerializeField] private GameObject knifeSpawner;
    [SerializeField] private KnifeScr knifePref;
    [SerializeField] private float knifeSpeed;
    public float KnifeSpeed { get => knifeSpeed; }
    [SerializeField] private int stuckedKnifesCountMin;
    [SerializeField] private int stuckedKnifesCountMax;
	[SerializeField] private int stuckedKnifesCount;
	public int StuckedKnifesCount { get => stuckedKnifesCount; }
    private KnifeScr currentKnife;
    private List<KnifeScr> allKnifes;

    [SerializeField] private AppleScr applePref;
	private AppleScr apple;
	[Range(0, 100)] [SerializeField] private int appleChance;

	public static GameManager GM { get; private set; }
    private InputController controller;
    private List<int> freePlaces;
    private Quaternion zeroQuaternion = new Quaternion(0, 0, 0, 0);

    void Awake()
    {
        GM = this;
		controller = new InputController();
		controller.Clicker.Tap.started += context => Throw();
		GoMainScreen();
    }

    void OnEnable() 
    {
        controller.Enable();
    }

	void OnDisable()
	{
		controller.Disable();
	}

    public void GoMainScreen()
    {
		Time.timeScale = 0;
		//controller.Clicker.Tap.started -= context => Throw();
		//controller.Disable();
		mainScreen.gameObject.SetActive(true);
        gameScreen.gameObject.SetActive(false);
		gameObjects.gameObject.SetActive(false);
        if (target)
        {
            GameObject.Destroy(target.gameObject);
            foreach(KnifeScr k in allKnifes)
            {
                GameObject.Destroy(k.gameObject);
            }
        }
    }

    public void GoPlayGame()
    {
		mainScreen.gameObject.SetActive(false);
		gameScreen.gameObject.SetActive(true);
        gameMenu.gameObject.SetActive(false);
		gameObjects.gameObject.SetActive(true);
		LoadLevel();
    }

    public void PressMenuButton()
    {
        if(Time.timeScale == 1)
        {
			controller.Disable();
			Time.timeScale = 0;
            gameMenu.gameObject.SetActive(true);
        }
        else
        {
			gameMenu.gameObject.SetActive(false);
			Time.timeScale = 1;
			controller.Enable();
        }
    }

    public void PressResratr()
    {
        GoMainScreen();
        GoPlayGame();
    }

	void LoadLevel()
	{
		//controller.Clicker.Tap.started += context => Throw();
		//controller.Enable();
		allKnifes = new List<KnifeScr>();
		freePlaces = new List<int> { -135, -90, -45, 0, 45, 90, 135, 180 };
		target = Instantiate(targetPref, Vector3.zero, zeroQuaternion, gameObjects.transform);

		SpawnApple();
		SpawnStuckedKnifes();
		SpawnKnife();
		Time.timeScale = 1;
	}

    void SpawnApple()
    {
		int tmpRandom = Random.Range(0, 101);
		bool isAppleNeed = tmpRandom <= appleChance;
		if(isAppleNeed)
        {
            int angle = GetRandomPlaceOnTarget();
			Vector3 tmpPos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
			Quaternion tmpQ = Quaternion.Euler(0, 0, angle + 90);
            apple = Instantiate(applePref, tmpPos * 0.6f, tmpQ, target.transform);
        }
	}

    void SpawnStuckedKnifes()
	{
        stuckedKnifesCount = Random.Range(stuckedKnifesCountMin, stuckedKnifesCountMax + 1);
        for (int i = 0; i < stuckedKnifesCount; i++)
        {
			int angle = GetRandomPlaceOnTarget();
			Vector3 tmpPos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
			Quaternion tmpQ = Quaternion.Euler(0, 0, angle + 90);
			KnifeScr tmpKnife = Instantiate(knifePref, tmpPos * 0.85f, tmpQ, target.transform);
            allKnifes.Add(tmpKnife);
		}
	}

    void SpawnKnife()
    {
		currentKnife = Instantiate(knifePref, knifeSpawner.transform.position, zeroQuaternion, knifeSpawner.transform);
		allKnifes.Add(currentKnife);
    }

    int GetRandomPlaceOnTarget()
    {
        int i = Random.Range(0, freePlaces.Count);
        int placeNum = freePlaces[i];
        freePlaces.RemoveAt(i);
        return placeNum;
    }

    void Throw()
    {
		if (Time.timeScale == 1)
        {
            currentKnife.ThrowKnife();
            SpawnKnife();
        }
    }

    public void TargetBlowingUp()
    {
        print("Win!");
		Time.timeScale = 0;
    }

	public void HitKnife()
	{
		print("Lose!");
		Vibration.Vibrate();
        Time.timeScale = 0;
	}

    void FixedUpdate()
    {
        if(Time.timeScale == 1)
        {
			target.SpinTheTarget();

			foreach (KnifeScr k in allKnifes)
			{
				if (k.Status == KnifeStatus.flying)
				{
					k.Flying();
				}
			}
        }
    }
}