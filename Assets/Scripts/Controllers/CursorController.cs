using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int mask = (1 << (int)Define.ELayer.Ground) | (1 << (int)Define.ELayer.Monster) | (1 << (int)Define.ELayer.NPC);
    Texture2D attackIcon;
    Texture2D handIcon;
    
    void Start()
    {
        attackIcon = Managers.Resource.Load<Texture2D>("Attack");
        handIcon = Managers.Resource.Load<Texture2D>("Hand");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.ELayer.Monster)
                Cursor.SetCursor(attackIcon, new Vector2(attackIcon.width / 5, 0), CursorMode.Auto);
            else
                Cursor.SetCursor(handIcon, new Vector2(handIcon.width / 3, 0), CursorMode.Auto);
        }
    }
}
