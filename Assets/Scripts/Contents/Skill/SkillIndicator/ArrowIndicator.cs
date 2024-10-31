using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class ArrowIndicator : Indicator
{
    
    public override void UpdatePosition(Vector3 inputPos)
    {
        base.UpdatePosition(inputPos);

        Vector3 playerPos = Managers.Game.GetPlayer().transform.position;
        Vector3 direction = (inputPos - Managers.Game.GetPlayer().transform.position).normalized;
        Vector3 length = direction * (info.maxLength / 2);
        transform.position = new Vector3(playerPos.x + length.x, 1f, playerPos.z + length.z);

        Quaternion rotate = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Euler(90f, rotate.eulerAngles.y, rotate.eulerAngles.z);     
    }
}
