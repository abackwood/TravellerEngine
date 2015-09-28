using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Action<T> where T : MonoBehaviour {
	bool IsAllowed(T target);
	void Execute(T target);
}
