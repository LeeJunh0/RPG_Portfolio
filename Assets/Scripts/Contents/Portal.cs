using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject target;
    public Portal exitPotal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            target = other.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(BindKey.Interact))
            OnPortal();
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }

    private void OnPortal()
    {
        GameObject player = Managers.Game.GetPlayer();
        player.transform.position = new Vector3(exitPotal.transform.position.x, 0, exitPotal.transform.position.z);
    }
}
