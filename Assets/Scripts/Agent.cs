using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour {
	static List<Agent> agents = new List<Agent>();
	public static ICollection<Agent> Agents {
		get { return new List<Agent>(agents); }
	}

	public float maxSpeed;

	public Vector3 Position {
		get { return transform.position; }
	}

	public ICollection<Agent> GetAgentsWithinRadius(float radius) {
		List<Agent> list = new List<Agent>();
		foreach(Agent agent in agents) {
			if(!agent.Equals (this) && Vector3.Distance(Position,agent.Position) < radius) {
				list.Add (agent);
			}
		}
		return list;
	}

	void Start () {
		agents.Add (this);
	}
	
	void Update () {
		
	}
}
