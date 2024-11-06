using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoxIndicator : Indicator
{
    public override void UpdatePosition(Vector3 inputPos)
    {
        transform.position = new Vector3(inputPos.x, transform.position.y, inputPos.z);
    }

    public override void UpdateRotate(Vector3 direction)
    {
        // X축을 고정하면서 Z축이 방향을 가리키게 하기 위해 회전을 설정
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.down);
        // Vector3.down으로 설정하여 Y축을 -Y로 유지

        // X축 90도 회전을 추가하여 최종 회전 설정
        transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);
    }
}
