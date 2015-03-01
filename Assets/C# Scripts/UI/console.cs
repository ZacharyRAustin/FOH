using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MyConsole
{
	private static List< string > messages = new List<string>();
	private static Vector2 viewPoint;
	private static GUIStyle style;
	private static GUIStyle style1;
	private static Color flashColour;
	private static Texture2D texture;

	public static void DrawConsole()
		{
		flashColour = new Color(1f, 0f, 0f, 0.1f);
		style = new GUIStyle ();
		style1 = new GUIStyle ();
		texture = new Texture2D (1, 1);
		texture.SetPixel (1, 1, flashColour);
	

		GUI.DrawTexture (new Rect (Screen.width - 200, Screen.height - 500, 200, 500),texture);
		GUILayout.BeginArea (new Rect (Screen.width - 200, Screen.height - 500, 200, 500));
		style.fontSize = 10;
		style.fontStyle = FontStyle.Normal;
		viewPoint = GUILayout.BeginScrollView (viewPoint);
		GUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();
		foreach (string s in messages)
			GUILayout.Label(s,style);
		GUILayout.EndVertical();
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}
	
	public static void NewMessage (string message)
	{
		messages.Add (message);
		viewPoint = new Vector2(0f,float.MaxValue);
	}
}
