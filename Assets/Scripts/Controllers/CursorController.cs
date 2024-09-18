using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int mask = (1 << (int)Define.ELayer.Ground) | (1 << (int)Define.ELayer.NPC);
    Texture2D npcIcon;
    Texture2D handIcon;
    
    void Start()
    {
        npcIcon = Managers.Resource.Load<Texture2D>("loot");
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
            if (hit.collider.gameObject.layer == (int)Define.ELayer.NPC)
                Cursor.SetCursor(npcIcon, new Vector2(npcIcon.width / 5, 0), CursorMode.Auto);
            else
                Cursor.SetCursor(handIcon, new Vector2(handIcon.width / 3, 0), CursorMode.Auto);
        }
    }
}
