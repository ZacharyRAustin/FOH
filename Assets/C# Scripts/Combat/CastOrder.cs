using UnityEngine;
using System.Collections;

public class CastOrder {

	public RandomAbility spell;
	public Character targetCharacter;
	public Vector3 targetLocation;

	public CastOrder (RandomAbility s, Character c, Vector3 l)
	{
		spell = s;
		targetCharacter = c;
		targetLocation = l;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
