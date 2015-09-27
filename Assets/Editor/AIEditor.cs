using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof (AI<MonoBehaviour>), true)]
public class AIEditor : Editor {
	public override void OnInspectorGUI() {
		AI<MonoBehaviour> ai = (AI<MonoBehaviour>)target;

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Behaviours:");
		foreach(AIBehaviour<MonoBehaviour> b in ai.Behaviours) {
			EditorGUILayout.LabelField("\t" + b.ToString());
		}
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Action Queue:");
		IEnumerator<Action<MonoBehaviour>> en = ai.ActionQueue.GetEnumerator();
		do {
			Action<MonoBehaviour> action = en.Current;
			EditorGUILayout.LabelField("\t" + action.ToString());
		} while(en.MoveNext());
	}
}