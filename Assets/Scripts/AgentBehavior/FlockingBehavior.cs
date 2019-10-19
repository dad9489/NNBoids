using System.Collections.Generic;
using UnityEngine;

namespace AgentBehavior {
    public class FlockingBehavior : AgentBehavior {
    
        private float cohesionWeight = 5f;
        private float alignmentWeight = 1f;
        private float avoidanceWeight = 2f;
        private float randomWeight = 1f;
        private float fearOfUnknownWeight = 0.01f;
        private float radius = 10f;
        private Vector2 currentVelocity;
    
        public override Vector2 calculateGoalPosition(BoidAgent agent) {
//            return Vector2.zero;
            return (Cohesion(agent) * cohesionWeight + Alignment(agent) * alignmentWeight +
                   Avoidance(agent) * avoidanceWeight + RandomMove(agent) * randomWeight +
                   FearOfUnknown(agent) * fearOfUnknownWeight).normalized;
        }

        private Vector2 Cohesion(BoidAgent agent) {
            List<Transform> neighbors = agent.fov.objectsInView;
            Vector2 cohesionMove = Vector2.zero;
            if (neighbors.Count == 0)
                return cohesionMove;
            foreach(var neighbor in neighbors) {
                cohesionMove += (Vector2)neighbor.position;
            }
            cohesionMove /= neighbors.Count;
            cohesionMove -= (Vector2)agent.transform.position;
            cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, 0.5f);
            return cohesionMove * 10;
        }
        
        private Vector2 Alignment(BoidAgent agent) {
            List<Transform> neighbors = agent.fov.objectsInView;
            Vector2 alignmentMove = Vector2.zero;
            if (neighbors.Count == 0)
                return agent.transform.up;
            foreach(var neighbor in neighbors) {
                alignmentMove += (Vector2)neighbor.up;
            }
            alignmentMove /= neighbors.Count;
            return alignmentMove;
        }
        
        private Vector2 Avoidance(BoidAgent agent) {
            List<Transform> neighbors = agent.fov.objectsInView;
            Vector2 avoidanceMove = Vector2.zero;
            if (neighbors.Count == 0)
                return avoidanceMove;
            foreach(var neighbor in neighbors) {
                avoidanceMove += (Vector2)(agent.transform.position - neighbor.position);
            }
            avoidanceMove /= neighbors.Count;
            return avoidanceMove;
        }

        private Vector2 RandomMove(BoidAgent agent) {
            return Random.insideUnitCircle;
//            return Vector2.zero;
        }

        private Vector2 FearOfUnknown(BoidAgent agent) {
            Vector2 centerOffset = Vector2.zero - (Vector2) agent.transform.position;
            float radiusCloseness = centerOffset.magnitude / radius;
            if (radiusCloseness > 0.9f) {
                return radiusCloseness * radiusCloseness * centerOffset;
            }
            return Vector2.zero;
        }
    }
}
