using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    [Range(0,10)] public float viewRadius = 2;
    [Range(0,360)] public float viewAngle = 270;

//    public LayerMask boidMask;
//    public LayerMask foodMask;
//    public LayerMask dangerMask;
    public LayerMask obstacleMask;
    
    public List<Transform> objectsInView;
    private Collider2D thisCollider;

    private void Start() {
        thisCollider = gameObject.GetComponent<Collider2D>();
    }

    public void FindVisibleObjects() {
        objectsInView.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);
        for(int i=0; i < targetsInViewRadius.Length; i++) {
            if (targetsInViewRadius[i] != thisCollider) {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 targetDirection = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.up, targetDirection) < (viewAngle / 2)) {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics2D.Raycast(transform.position, targetDirection, distanceToTarget, obstacleMask)) {
                        // if we don't collide with an obstacle on the way to the target
                        objectsInView.Add(target);
                    }
                }
            }
        }
    }

    public Vector2 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
