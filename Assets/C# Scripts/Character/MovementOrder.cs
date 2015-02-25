using UnityEngine;
using System.Collections;

public class MovementOrder {

	public Vector3 destination;

	public MovementOrder (Vector3 click)
	{
		destination = click;
	}
}
