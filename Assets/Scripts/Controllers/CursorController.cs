using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
    Texture2D attackIcon;
    Texture2D handIcon;

    void Start()
    {
        attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                Cursor.SetCursor(attackIcon, new Vector2(attackIcon.width / 5, 0), CursorMode.Auto);
            else
                Cursor.SetCursor(handIcon, new Vector2(handIcon.width / 3, 0), CursorMode.Auto);
        }
    }
}
