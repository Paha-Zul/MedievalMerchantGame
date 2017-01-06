using UnityEngine;
using System.Collections;

public class EditorGizmo : MonoBehaviour {

    public float explosionRadius = 1.0F;
    public bool Solid = true;
    public Color gizmoColor = Color.blue;

    void OnDrawGizmos() {
        Gizmos.color = gizmoColor;
        if(!Solid) Gizmos.DrawWireSphere(transform.position, explosionRadius);
        else Gizmos.DrawSphere(transform.position, explosionRadius);
    }

    
}
