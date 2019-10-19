using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView) target;
        Vector2 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector2 viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
        Handles.DrawWireArc(fov.transform.position,
            Vector3.forward,
            (Vector3)viewAngleB * fov.viewRadius,
            fov.viewAngle,
            fov.viewRadius);
        Vector3 line1 = ((Vector3) viewAngleA * fov.viewRadius).normalized;
        Vector3 line2 = ((Vector3) viewAngleB * fov.viewRadius).normalized;
        if (line1 != line2) {
            Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)viewAngleA * fov.viewRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)viewAngleB * fov.viewRadius);
        }

//        Handles.color = Color.red;
//        foreach (Transform visibleTarget in fov.objectsInView)
//        {
//            Vector3 targetDirection = (visibleTarget.position - fov.transform.position).normalized;
//            var reference = fov.transform.up;
//            float angle = Vector3.Angle(targetDirection, reference);
//            Handles.DrawLine(fov.transform.position, visibleTarget.position);
//        }
    }
}
