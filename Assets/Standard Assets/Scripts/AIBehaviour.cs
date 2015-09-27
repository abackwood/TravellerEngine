using UnityEngine;
using System.Collections;

public abstract class AIBehaviour<T> where T : MonoBehaviour {
	public AI<T> ai;
	public T target;

	public abstract void Start();
	public abstract void Update();
	public abstract bool Done();

	public override int GetHashCode () {
		return this.GetType().Name.GetHashCode();
	}
}
