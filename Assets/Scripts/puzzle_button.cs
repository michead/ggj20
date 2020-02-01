using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle_button : MonoBehaviour
{
    player_movement pm;
    Puzzle puzzle;

    float pressure;
    bool force_tapping1, force_tapping2 = false;

    [SerializeField]
    float decrease_pressure_by;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player_controller").GetComponent<player_movement>();
        puzzle = GetComponent<Puzzle>();
        pressure = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase pressure (1.0 max) by 0.3 per sec
        pressure = Mathf.Clamp01(pressure + Time.deltaTime * 0.3f);

        // P1
        if (puzzle.p1_locked)
        {
            if (pm.p1.B)
            {
                puzzle.p1_locked = false;
                pm.p1.is_solving = false;
            }
            if (pm.p1.A && !force_tapping1)
            {
                force_tapping1 = true;
                pressure = Mathf.Clamp01(pressure - decrease_pressure_by);
            }
            else
            {
                force_tapping1 = false;
            }
        }
        // P2
        if (puzzle.p2_locked)
        {
            if (pm.p2.B)
            {
                puzzle.p2_locked = false;
                pm.p2.is_solving = false;
            }
            if (pm.p2.A && !force_tapping2)
            {
                force_tapping2 = true;
                pressure = Mathf.Clamp01(pressure - decrease_pressure_by);
            }
            else
            {
                force_tapping2 = false;
            }
        }

        if (pressure <= 0.0f)
        {
            puzzle.Solve();
        }
    }
}
