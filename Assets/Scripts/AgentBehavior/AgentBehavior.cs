using UnityEngine;

namespace AgentBehavior {
    public abstract class AgentBehavior : ScriptableObject {
        /// <summary>
        /// Should return a normalized Vector2 indicating the direction
        /// in which the boid should move this step.
        /// </summary>
        /// <param name="boidAgent"></param>
        /// <returns></returns>
        public abstract Vector2 calculateGoalPosition(BoidAgent boidAgent);
    }
}
