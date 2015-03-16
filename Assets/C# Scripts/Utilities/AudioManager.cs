using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip mainClip;
	public AudioClip fightClip;
	public AudioClip bossClip;
	public AudioClip gameOverClip;
	public AudioClip softClip;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {

	}

	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BossSong()
	{
		audioSource.Stop ();
		audioSource.clip = bossClip;
		audioSource.Play ();
	}

	public void FightSong()
	{
		audioSource.Stop ();
		audioSource.clip = fightClip;
		audioSource.Play ();
	}

	public void GameOverSong()
	{
		audioSource.clip = gameOverClip;
		audioSource.Play ();
	}

	public void MainSong()
	{
		audioSource.Stop ();
		audioSource.clip = mainClip;
		audioSource.Play ();
	}

	public void SoftSong()
	{
		audioSource.Stop ();
		audioSource.clip = softClip;
		audioSource.Play ();
	}

	public void RandSong()
	{
		int x = Random.Range (0, 3);

		if (x == 0)
		{
			MainSong();
		}
		else if (x == 1)
		{
			FightSong();
		}
		else
		{
			SoftSong();
		}
	}
}
