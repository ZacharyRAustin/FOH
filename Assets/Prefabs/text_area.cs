using UnityEngine;
using System.Collections;

public class text_area : MonoBehaviour{

	public string input_string = "HEY";

void OnGUI(){
		GUI.BeginGroup (new Rect ((Screen.width / 3), (Screen.height / 2), 200 * 2, 1000));
		input_string = GUI.TextArea(new Rect ((Screen.width / 3), (Screen.height / 2), 200 * 2, 1000), input_string,200);
		                GUI.EndGroup();

		}
	public void set_string(string b){
		input_string = b;
		}
}
