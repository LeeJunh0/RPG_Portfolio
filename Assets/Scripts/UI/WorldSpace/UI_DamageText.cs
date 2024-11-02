using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_DamageText : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void SetDamage(int damage) { GetComponent<TextMeshPro>().text = string.Format($"{damage}"); }

    public void DestroyEvent() { Managers.Resource.Destroy(gameObject); }
}
