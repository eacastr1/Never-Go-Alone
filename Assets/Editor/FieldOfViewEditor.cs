using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.right, 360, fov.ViewRadius);

        Vector2 viewAngleA = fov.DirectionFromAngle(-fov.ViewAngle / 2, false);
        Vector2 viewAngleB = fov.DirectionFromAngle(fov.ViewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)(viewAngleA * fov.ViewRadius));
        Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)(viewAngleB * fov.ViewRadius));

        Handles.color = Color.green;
        foreach (Transform target in fov.VisibleTargets)
        {
            Handles.DrawLine(fov.transform.position, target.position);
        }

    }
}
