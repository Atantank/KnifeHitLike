using System;
using UnityEngine;
using UnityEngine.Audio;

public class ProgressScr : MonoBehaviour
{
	private static ProgressData progress;
	private static AudioMixerGroup mixer;
	[SerializeField] private AudioMixerGroup masterVolume;

	public static int GameScore
	{
		get => progress.GameScore;
		set
		{
			progress.GameScore = value;
			SaveData();
		}
	}

	public static int Level
	{
		get => progress.Level;
		set
		{
			progress.Level = value;
			SaveData();
		}
	}

	public static bool IsMute
	{
		get => progress.IsMute;
		set
		{
			progress.IsMute = value;
			mixer.audioMixer.SetFloat("MasterVolume", IsMute ? -80 : 0);
			SaveData();
		}
	}

	void Awake()
	{
		progress = new ProgressData(0, 1, false);
		mixer = masterVolume;
		mixer.audioMixer.SetFloat("MasterVolume", IsMute ? -80 : 0);

		CheckFirstLoad();
		LoadData();
	}

	public void CheckFirstLoad()
	{
		if (!PlayerPrefs.HasKey("ProgressKnifeHitLike"))
		{
			ResetGameData();
		}
	}

	public static void ResetGameData()
	{
		progress = new ProgressData(0, 1, false);
		SaveData();
		LoadData();
	}

	public static void LoadData()
	{
		progress = JsonUtility.FromJson<ProgressData>(PlayerPrefs.GetString("ProgressKnifeHitLike"));
	}

	public static void SaveData()
	{
		PlayerPrefs.SetString("ProgressKnifeHitLike", JsonUtility.ToJson(progress));
		PlayerPrefs.Save();
	}

	[Serializable]
	class ProgressData
	{
		public int GameScore;
		public int Level;
		public bool IsMute;

		public ProgressData(int _gameScore, int _level, bool _isMute)
		{
			GameScore = _gameScore;
			Level = _level;
			IsMute = _isMute;
		}
	}
}
