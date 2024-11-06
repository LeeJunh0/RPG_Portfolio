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
        // X���� �����ϸ鼭 Z���� ������ ����Ű�� �ϱ� ���� ȸ���� ����
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.down);
        // Vector3.down���� �����Ͽ� Y���� -Y�� ����

        // X�� 90�� ȸ���� �߰��Ͽ� ���� ȸ�� ����
        transform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, 0);
    }
}
