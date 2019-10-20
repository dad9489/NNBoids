using System;
using AgentBehavior;
using AgentBehavior.NeuralNetwork;
using UnityEngine;
using Network = AgentBehavior.NeuralNetwork.Network;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(FieldOfView))]
public class BoidAgent : MonoBehaviour {
    private Collider2D agentCollider;
    public FieldOfView fov;
    private Vector2 currentVelocity;
    private float speed = 5f;
    private AgentBehavior.AgentBehavior agentBehavior;
    public Genome genome;
    
    // Start is called before the first frame update
    void Start() {
        agentCollider = gameObject.GetComponent<Collider2D>();
        fov = gameObject.GetComponent<FieldOfView>();
//        innovationIndexer = gameObject.GetComponent<InnovationIndexer>();
        agentBehavior = ScriptableObject.CreateInstance<NNBasicBehavior>();
    }

    private void OnMouseDown() {
        GlobalManager.Instance.selectedAgent = this;
    }

    // Update is called once per frame
    void Update() {
//        Vector2 goalPosition = new Vector2(0.5f, 0.5f);
        fov.FindVisibleObjects();
        Vector2 goalPosition = CalculateGoalPosition();
//        goalPosition = Vector2.SmoothDamp(transform.up, goalPosition, ref currentVelocity, 0.5f).normalized;
        move(goalPosition * speed);
    }

    void move(Vector2 goalPosition) {
        var currentTransform = transform;
        var currentRotation = currentTransform.rotation;
        currentTransform.up += (Vector3)goalPosition;
        currentTransform.position += (Vector3)goalPosition * Time.deltaTime;
    }

    Vector2 CalculateGoalPosition() {
        return agentBehavior.calculateGoalPosition(this);
//        return transform.up;
    }
}
