using UnityEngine;
using System.Collections;

public abstract class AIBehaviour<T> where T : MonoBehaviour {
	public AI<T> ai;
	public T target;

	public abstract void Start();
	public abstract void Update();
	public abstract bool Done();

	protected void Do<V>(params object[] args) where V : Action<T> {
		ai.Do<V>(args);
	}

	public override int GetHashCode () {
		return this.GetType().Name.GetHashCode();
	}
	public override bool Equals (object obj) {
		return this.GetType().Equals(obj.GetType());
	}
}
