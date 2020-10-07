	using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
  
  public class StateSearchNode : IComparable<StateSearchNode>, IEquatable<StateSearchNode> {
		public StateSearchNode parent;
		public float runningCost;
		public Dictionary<string, bool> state;
		public Action action;

		public int CompareTo(StateSearchNode other)
    {
      if (this.runningCost < other.runningCost) return -1;
      else if (this.runningCost > other.runningCost) return 1;
      else return 0;
    }

		public bool Equals(StateSearchNode other) {
			if(other == null) {
				return false;
			}
			// TODO: replace this with containsKey
			bool statesEqual = this.state.Count == other.state.Count && 
				this.state.All(k => other.state.ContainsKey(k.Key) && other.state[k.Key] == k.Value);
			if(statesEqual) {
				return true;
			}
			return false;
		}

		public override bool Equals (object other) {
			if (other == null)
         return false;

      StateSearchNode nodeObj = other as StateSearchNode;
      if (nodeObj == null)
         return false;
      else
         return Equals(nodeObj);
		}

		public override int GetHashCode()
		{
				return this.state.GetHashCode();
		}

		public StateSearchNode(StateSearchNode parent, float runningCost, Dictionary<string, bool> state, Action action) {
			this.parent = parent;
			this.runningCost = runningCost;
			this.state = state;
			this.action = action;
		}
	}