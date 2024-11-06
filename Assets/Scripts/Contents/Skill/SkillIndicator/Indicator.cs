using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorInfo
{
    public Define.EIndicator type = Define.EIndicator.RangeIndicator;
    public float maxLength = 5f;
    public float maxRadius = 5f;
}

public class Indicator : MonoBehaviour
{
    public IndicatorInfo info;

    public void SetInfo(Define.EIndicator type, float maxLength, float maxRadius)
    {
        info = new IndicatorInfo();

        info.type = type;
        info.maxLength = maxLength;
        info.maxRadius = maxRadius;

        switch (type)
        {
            case Define.EIndicator.ArrowIndicator:
                {
                    transform.localScale = new Vector3(1f, maxLength, 1f);
                }
                break;
            case Define.EIndicator.CircleIndicator:
                {
                    transform.localScale = new Vector3(maxLength, maxLength, maxLength);
                }
                break;
            case Define.EIndicator.RangeIndicator:
                {
                    transform.localScale = new Vector3(maxRadius, maxRadius, maxRadius);
                }
                break;
        }
    }

    public virtual void UpdatePosition(Vector3 inputPos) 
    {
        if (info.type != Define.EIndicator.CircleIndicator)
            return;
    }
    public virtual void UpdateRotate(Vector3 direction)
    {

    }
}
