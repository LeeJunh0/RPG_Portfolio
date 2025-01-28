using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcIndicator : Indicator
{ 
    public override void UpdatePosition(Vector3 inputPos)
    {
        transform.position = new Vector3(inputPos.x, transform.position.y, inputPos.z);
    }

    public override void UpdateRotate(Vector3 direction, float speed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation * Quaternion.Euler(90, 0, 90), speed * Time.deltaTime);
    }
}
