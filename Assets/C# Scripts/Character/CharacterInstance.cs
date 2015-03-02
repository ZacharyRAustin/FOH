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
		if (anim == null) {
			animation.Play ("walk");
		} else {
			anim.SetBool ("run", false);
			anim.SetBool ("walk", true);
			anim.SetBool ("idle", false);
			anim.SetBool ("attack", false);
		}
	}
	
	public void run ()
	{
		if (anim == null) {
			animation.Play ("run");
		} else {
			anim.SetBool ("run", true);
			anim.SetBool ("walk", false);
			anim.SetBool ("idle", false);
			anim.SetBool ("attack", false);
		}
	}
	
	public void idle ()
	{
		if (anim == null) {
			animation.Play ("idle");
		} else {
			anim.SetBool ("run", false);
			anim.SetBool ("walk", false);
			anim.SetBool ("attack", false);
			anim.SetBool ("idle", true);
		}
	}
	
	public void attack () {
		if (anim == null) {
			animation.Play ("attack");
		} else {
			anim.SetBool ("run", false);
			anim.SetBool ("walk", false);
			anim.SetBool ("idle", false);
			anim.SetBool ("attack", true);
		}
	}
}
