using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode Mode = Define.CameraMode.QuaterView;

    [SerializeField]
    Vector3 Delta = new Vector3(0.0f, 6.0f, -5.0f);

    [SerializeField]
    GameObject Player = null;

    public void SetPlayer(GameObject player) { Player = player; }

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if(Mode == Define.CameraMode.QuaterView)
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

    public void SetQuaterView(Vector3 delta)
    {
        Mode = Define.CameraMode.QuaterView;
        Delta = delta;
    }
}
