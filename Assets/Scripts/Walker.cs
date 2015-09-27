using UnityEngine;
using System.Collections;

public class Walker : AI<Agent> {
	protected override void Setup() {
		AddBehaviour<RandomWalk>();
	}

	class RandomWalk : AIBehaviour<Agent> {
		Vector3 destination;

		public override void Start() {
			GenerateDestination();
		}
		public override void Update() {
			if(Vector3.Distance(target.Position, destination) < 0.5f) {
				GenerateDestination();
			}
			ai.Do<MoveTowards>(destination);
		}
		void GenerateDestination() {
			float x = UnityEngine.Random.Range(0.0f,30.0f);
			float z = UnityEngine.Random.Range(0.0f,30.0f);
			destination = new Vector3(x,0.5f,z);
		}

		public override bool Done() {
			return false;
		}
	}
}
