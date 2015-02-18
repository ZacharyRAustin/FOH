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
        SpawnCharacteristics.setNearDoor(true, doorPosition);
    }

    void OnTriggerExit() {
        SpawnCharacteristics.setNearDoor(false, -1);
    }

    public void setDoorPosition(int pos) {
        doorPosition = pos;
    }

    public int getDoorPosition() {
        return doorPosition;
    }
}
