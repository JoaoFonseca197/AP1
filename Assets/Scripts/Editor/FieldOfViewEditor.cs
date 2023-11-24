using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyScript))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        
        EnemyScript enemy = (EnemyScript)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy._maxLookRange);

        Vector3 viewAngle01 = DirectionFromAngle(enemy.transform.eulerAngles.y, -enemy._viewAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(enemy.transform.eulerAngles.y, enemy._viewAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngle01 * enemy._maxLookRange);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngle02 * enemy._maxLookRange);

        //if (enemy.canSeePlayer)
        //{
        //    Handles.color = Color.green;
        //    Handles.DrawLine(enemy.transform.position, enemy.playerRef.transform.position);
        //}
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
