using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle_button : MonoBehaviour
{
    player_movement pm;
    Puzzle puzzle;

    Animator animator;

    float pressure;
    bool force_tapping1, force_tapping2 = false;

    [SerializeField]
    float decrease_pressure_by;

    // Animation stuff
    Transform needle;


    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("InputManager").GetComponent<player_movement>();
        puzzle = GetComponent<Puzzle>();
        //animator = GetComponent<Animator>();
        foreach (Transform t in transform)
        {
            if (t.gameObject.name == "Needle")
            {
                needle = t;
            }
            if (t.gameObject.name == "Cylinder.022")
            {
                animator = t.GetComponent<Animator>();
            }
        }
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
                animator.Play("Button_press");
            }
            else if (!pm.p1.A)
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
                animator.Play("Button_press");
            }
            else if (!pm.p1.A)
            {
                force_tapping2 = false;
            }
        }

        if (pressure <= 0.0f)
        {
            if (puzzle.p1_locked)
            {
                puzzle.p1_locked = false;
                pm.p1.is_solving = false;
            }
            if (puzzle.p2_locked)
            {
                puzzle.p2_locked = false;
                pm.p2.is_solving = false;
            }
            puzzle.Solve();
        }
    }

    private void LateUpdate()
    {
        // Needle jitter 
        // max 13 min 145
        needle.rotation = Quaternion.Euler(0.0f, 0.0f, 145.0f - (132.0f * pressure));
        // Hax
        if (transform.position.y > 0.0f)
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
    }
}
