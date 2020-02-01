﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    Puzzle par;
    player_movement pm;

    void Start()
    {
        par = gameObject.GetComponentInParent<Puzzle>();
        pm = GameObject.Find("Player_controller").GetComponent<player_movement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player1")
        {
            if (pm.p1.A)
            {
                par.p1_locked = true;
            }
        }
        if (other.gameObject.name == "Player2")
        {
            if (pm.p2.A)
            {
                par.p2_locked = true;
            }
        }
    }
}