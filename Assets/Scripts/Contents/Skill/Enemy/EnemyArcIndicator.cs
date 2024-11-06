using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcIndicator : Indicator
{
    public override void UpdatePosition(Vector3 inputPos)
    {
        transform.position = new Vector3(inputPos.x, transform.position.y, inputPos.z);
    }

    public override void UpdateRotate(Vector3 direction)
    {
        
    }
}
