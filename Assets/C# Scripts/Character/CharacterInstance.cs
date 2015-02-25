using UnityEngine;
using System.Collections;

public class CharacterInstance : MonoBehaviour {

	public Character parentChar;

	Animator anim;

	public void SetParentChar (Character c)
	{
		parentChar = c;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void walk ()
	{
		anim.SetBool ("walk", true);
		anim.SetBool ("idle", false);
		anim.SetBool ("attack", false);
	}

	public void idle ()
	{
		anim.SetBool ("walk", false);
		anim.SetBool ("attack", false);
		anim.SetBool ("idle", true);
	}

	public void attack () {
		anim.SetBool ("walk", false);
		anim.SetBool ("idle", false);
		anim.SetBool ("attack", true);
	}
}
