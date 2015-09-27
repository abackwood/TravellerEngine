using UnityEngine;
using System.Collections;

public class Follower : AI<Agent> {
	public Agent followedAgent;

	protected override void Setup () {
		AddBehaviour(new Follow(followedAgent));
	}

	class Follow : AIBehaviour<Agent> {
		Agent followedAgent;

		public override void Start () {}
		public override void Update () {
			Vector3 destination = followedAgent.Position;
			ai.Do<MoveTowards>(destination);
		}
		public override bool Done () {
			return false;
		}

		public Follow(Agent followedAgent) {
			this.followedAgent = followedAgent;
		}
	}
}
