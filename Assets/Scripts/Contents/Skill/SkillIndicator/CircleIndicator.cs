using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIndicator : Indicator
{
    RangeIndicator rangeIndicator;

    public void SetRange(GameObject go) 
    {
        rangeIndicator = go.GetComponent<RangeIndicator>();
        rangeIndicator.SetInfo(Define.EIndicator.RangeIndicator, info.maxLength, info.maxRadius);

        Vector3 playerPos = Managers.Game.GetPlayer().transform.position;
        go.transform.position = new Vector3(playerPos.x, go.transform.position.y, playerPos.z);
        go.transform.parent = Managers.Game.GetPlayer().transform;
    }

    public override void UpdatePosition(Vector3 inputPos)
    {
        if (rangeIndicator == null)
            return;

        Vector3 playerPos = Managers.Game.GetPlayer().transform.position;
        float distance = Vector3.Distance(inputPos, playerPos);

        if(distance > (rangeIndicator.info.maxRadius / 2))
        {
            Vector3 direction = (inputPos - playerPos).normalized;
            Vector3 maxPos = direction * (info.maxRadius / 2);
            inputPos = new Vector3(playerPos.x + maxPos.x, transform.position.y, playerPos.z + maxPos.z);
        }

        transform.position = new Vector3(inputPos.x, transform.position.y, inputPos.z);
        Debug.Log(transform.position);
    }

    private void OnDestroy()
    {
        Managers.Resource.Destroy(rangeIndicator.gameObject);
    }
}
