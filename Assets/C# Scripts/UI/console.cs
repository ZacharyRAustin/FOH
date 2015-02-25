using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MyConsole
{
	private static List< string > messages = new List<string>();
	private static Vector2 viewPoint;
	
	public static void DrawConsole (Rect area)
	{
		GUILayout.BeginArea (area);
		viewPoint = GUILayout.BeginScrollView (viewPoint);
		GUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();
		foreach (string s in messages)
			GUILayout.Label(s);
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
