using UnityEngine;
using System.Collections;

public class BasicAttackProjectile : MonoBehaviour {
	
	public float velocity = 15f;
	public Vector3 targetLocation;
	private Vector3 velocityVector;
	
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
		if (velocityVector.magnitude < 1f)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

}
