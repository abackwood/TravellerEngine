using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof (Follower))]
public class FollowerEditor : Editor {
	public override void OnInspectorGUI() {
		Follower ai = (Follower)target;

		DrawDefaultInspector();

		EditorGUILayout.LabelField("Behaviours:");
		foreach(AIBehaviour<Agent> b in ai.Behaviours) {
			EditorGUILayout.LabelField("\t" + b.ToString());
		}

		EditorGUILayout.LabelField("Action Queue:");
		IEnumerator<Action<Agent>> en = ai.ActionQueue.GetEnumerator();
		while(en.MoveNext()) {
			Action<Agent> action = en.Current;
			EditorGUILayout.LabelField("\t" + action.ToString());
		}
	}
}