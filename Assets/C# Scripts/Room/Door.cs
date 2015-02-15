using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private int doorPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter() {
        Debug.Log("Entered Collider for " + doorPosition);
    }

    public void setDoorPosition(int pos) {
        doorPosition = pos;
    }

    public int getDoorPosition() {
        return doorPosition;
    }
}
