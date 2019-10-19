using System.Collections.Generic;
using UnityEngine;
using Network = AgentBehavior.NeuralNetwork.Network;

namespace AgentBehavior {
    public class NNBasicBehavior : AgentBehavior {

        public override Vector2 calculateGoalPosition(BoidAgent agent) {
            var position = agent.transform.position;
            List<float> inputs = new List<float>{position.x, position.y, position.z};
            return agent.genome.Evaluate(inputs).normalized;
        }

    }
}
