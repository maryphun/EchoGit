using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool pickedUp = false;
    [SerializeField] private Transform player;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector2.Distance(new Vector2(player.position.x, player.position.z), new Vector2(transform.position.x, transform.position.z)) < 1.5f)
            {
                //get the key
                this.pickedUp = true;
            }
        }
    }
}
