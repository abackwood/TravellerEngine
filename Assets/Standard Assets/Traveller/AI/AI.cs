using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AI<T> : MonoBehaviour where T : MonoBehaviour {
	public T target { get; private set; }

	List<AIBehaviour<T>> behaviours;
	public ICollection<AIBehaviour<T>> Behaviours {
		get { return new List<AIBehaviour<T>>(behaviours); }
	}

	Queue<Action<T>> actionQueue;
	public Queue<Action<T>> ActionQueue {
		get { return new Queue<Action<T>>(actionQueue); }
	}

	Dictionary<string,object> variables;

	//Behaviours
	public AIBehaviour<T> AddBehaviour<V>(params object[] args) where V : AIBehaviour<T> {
		AIBehaviour<T> b = System.Activator.CreateInstance(typeof(V),args) as AIBehaviour<T>;
		b.ai = this;
		b.target = target;
		b.Start();
		behaviours.Add (b);
		return b;
	}
	public void RemoveBehaviour(AIBehaviour<T> b) {
		behaviours.Remove (b);
	}

	//Actions
	public bool Do<V>(params object[] args) where V : Action<T> {
		Action<T> action = System.Activator.CreateInstance(typeof(V),args) as Action<T>;
		if(action.IsAllowed(target)) {
			actionQueue.Enqueue(action);
			return true;
		}
		else {
			return false;
		}
	}

	//Variables
	public object GetVariable(string id) {
		return variables.ContainsKey(id) ? variables[id] : null;
	}
	public object SetVariable(string id, object value) {
		object oldValue = GetVariable(id);
		variables[id] = value;
		return oldValue;
	}

	void Start() {
		target = GetComponent<T>();
		behaviours = new List<AIBehaviour<T>>();
		actionQueue = new Queue<Action<T>>();
		variables = new Dictionary<string, object>();
		Setup();
	}
	protected abstract void Setup();

	void Update() {
		UpdateBehaviours();
		if(actionQueue.Count > 0) {
			DoNextAction();
		}
	}
	void UpdateBehaviours() {
		foreach(AIBehaviour<T> b in behaviours) {
			if(b.Done()) {
				RemoveBehaviour(b);
			}
			else {
				b.Update();
			}
		}
	}
	void DoNextAction() {
		Action<T> action = actionQueue.Dequeue();
		if(action.IsAllowed(target)) {
			action.Execute(target);
		}
	}
}
