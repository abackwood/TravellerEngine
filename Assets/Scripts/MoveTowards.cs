using UnityEngine;
using System.Collections;

public class MoveTowards : Action<Agent> {
	Vector3 goal;

	public bool IsAllowed(Agent target) {
		return true;
	}

	public void Execute(Agent target) {
		Vector3 direction = goal - target.Position;
		float maxDistance = target.maxSpeed * Time.deltaTime;
		float distance = Mathf.Min (maxDistance, direction.magnitude);
		target.transform.position += direction.normalized * distance;
	}

	public MoveTowards(Vector3 direction) {
		this.goal = direction;
	}

	public override string ToString () {
		return string.Format ("MoveTowards " + goal);
	}
}
