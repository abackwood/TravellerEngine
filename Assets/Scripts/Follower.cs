using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Follower : AI<Agent> {
	public Agent followedAgent;
	public int state;

	protected override void Setup () {
		state = 0;
		AddBehaviour<Follow>(followedAgent);
		AddBehaviour<Avoid>();
	}

	class Follow : AIBehaviour<Agent> {
		Agent followedAgent;

		public override void Start () {}
		public override void Update () {
			Follower follower = ai as Follower;
			if(follower.state == 0) {
				Vector3 destination = followedAgent.Position;
				ai.Do<MoveTowards>(destination);
			}
		}
		public override bool Done () {
			return false;
		}

		public Follow(Agent followedAgent) {

			this.followedAgent = followedAgent;
		}
	}

	class Avoid : AIBehaviour<Agent> {
		public override void Start () {}
		public override void Update() {
			Follower follower = ai as Follower;
			ICollection<Agent> list = target.GetAgentsWithinRadius(2);
			if(list.Count > 0) {
				follower.state = 1;
				Vector3 direction = Vector3.zero;
				foreach(Agent a in list) {
					direction += target.Position - a.Position;
				}
				ai.Do<MoveTowards>(target.Position + direction);
			}
			else {
				follower.state = 0;
			}
		}
		public override bool Done () {
			return false;
		}
	}
}
