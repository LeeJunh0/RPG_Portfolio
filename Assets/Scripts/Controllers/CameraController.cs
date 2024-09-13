using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.ECameraMode Mode = Define.ECameraMode.QuaterView;

    [SerializeField]
    Define.ECameraType type = Define.ECameraType.MainCamera;

    [SerializeField]
    Vector3 Delta;

    [SerializeField]
    GameObject Player = null;

    public void SetPlayer(GameObject player) { Player = player; }

    void Start()
    {
        switch (type)
        {
            case Define.ECameraType.MainCamera:
                Delta = new Vector3(0f, 13f, -9f);
                break;
            case Define.ECameraType.MiniMapCamera:
                Delta = new Vector3(0f, 9f, 0f);
                break;
        }        
    }

    void LateUpdate()
    {
        if(gameObject.tag == "MainCamera")
        {
            if (Mode == Define.ECameraMode.QuaterView)
            {
                if (Player.IsValid() == false)
                    return;

                RaycastHit hit;
                if (Physics.Raycast(Player.transform.position, Delta, out hit, Delta.magnitude, LayerMask.GetMask("Block")))
                {
                    float dis = (hit.point - Player.transform.position).magnitude * 0.8f;
                    transform.position = Player.transform.position + Delta.normalized * dis;
                }
                else
                {
                    transform.position = Player.transform.position + Delta;
                    transform.LookAt(Player.transform);
                }
            }
        }
        else
        {
            if (Player.IsValid() == false)
                return;

            transform.position = Player.transform.position + Delta;
            transform.LookAt(Player.transform);
        }
                
    }

    public void SetQuaterView(Vector3 delta)
    {
        Mode = Define.ECameraMode.QuaterView;
        Delta = delta;
    }
}
