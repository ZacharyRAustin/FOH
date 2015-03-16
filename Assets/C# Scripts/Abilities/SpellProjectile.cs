using UnityEngine;
using System.Collections;

public class SpellProjectile : MonoBehaviour {

	public Color spellColor;
	public float velocity;
	public Vector3 targetLocation;
	private Vector3 velocityVector;
	public RandomAbility spell;

	void Start ()
	{

	}

	void Update ()
	{
		velocityVector = targetLocation - transform.localPosition;
		//velocityVector.z = 0f;
		velocityVector.Normalize();
		transform.localPosition += velocityVector * velocity * Time.deltaTime;

		ArrivedAtTargetCheck ();
	}

	void ArrivedAtTargetCheck()
	{
		velocityVector = targetLocation - transform.localPosition;
		if (velocityVector.magnitude < 1)
		{
			spell.TargetPositionResolve();
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		Character hitChar = collision.gameObject.GetComponentInParent<Character>();
		Debug.Log (spell.name + " hit " + hitChar.name);
		spell.CollisionResolve (hitChar);
	}
}
