using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_switches : MonoBehaviour
{
    player_movement pm;
    Puzzle puzzle;

    bool[] switches;    // Which switches have been switched
    int selected;       // Which switch is selected

    Animator[] animators;
    Transform bar;

    bool left_toggle, right_toggle;

    // Start is called before the first frame update
    void Start()
    {
        animators = new Animator[3];
        switches = new bool[3];
        for (int i = 0; i < 3; i++)
            switches[i] = false;
        selected = 1;

        foreach (Transform t in transform)
        {
            foreach (Transform tt in t)
            {
                if (tt.gameObject.name == "Lever1")
                {
                    animators[0] = tt.gameObject.GetComponent<Animator>();
                }
                if (tt.gameObject.name == "Lever2")
                {
                    animators[1] = tt.gameObject.GetComponent<Animator>();
                }
                if (tt.gameObject.name == "Lever3")
                {
                    animators[2] = tt.gameObject.GetComponent<Animator>();
                }
                if (tt.gameObject.name == "Bar")
                {
                    bar = tt.gameObject.GetComponent<Transform>();
                }
            }
        }
        bar.localScale = new Vector3(0.0f, 1.0f, 1.0f);

        pm = GameObject.Find("InputManager").GetComponent<player_movement>();
        puzzle = GetComponent<Puzzle>();
        selected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // P1
        if (puzzle.p1_locked)
        {
            if (pm.p1.B)
            {
                puzzle.p1_locked = false;
                pm.p1.is_solving = false;
            }
            if (pm.p1.A)
            {
                if (selected == 0 && !switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    animators[0].Play("Flip0");
                }
                else if (selected == 1 && !switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    animators[1].Play("Flip1");
                }
                else if (selected == 2 && switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    animators[2].Play("Flip2");
                }
            }

            if (pm.p1.left && !left_toggle)
            {
                if (selected > 0)
                    selected--;
                else
                    selected = 2;
                left_toggle = true;
            }
            else
                left_toggle = false;
            if (pm.p1.right && !right_toggle)
            {
                if (selected < 2)
                    selected++;
                else
                    selected = 0;
                right_toggle = true;
            }
            right_toggle = false;
        }

        // P2
        if (puzzle.p2_locked)
        {
            if (pm.p2.B)
            {
                puzzle.p2_locked = false;
                pm.p2.is_solving = false;
            }
            if (pm.p2.A)
            {
                if (selected == 0 && !switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    animators[0].Play("Flip0");
                }
                else if (selected == 1 && !switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    animators[1].Play("Flip1");
                }
                else if (selected == 2 && switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    animators[2].Play("Flip2");
                }
            }

            if (pm.p2.left && !left_toggle)
            {
                if (selected > 0)
                    selected--;
                else
                    selected = 2;
                left_toggle = true;
            }
            else
                left_toggle = false;
            if (pm.p2.right && !right_toggle)
            {
                if (selected < 2)
                    selected++;
                else
                    selected = 0;
                right_toggle = true;
            }
            else
                right_toggle = false;
        }

        if (switches[0] && switches[1] && switches[2])
            puzzle.Solve();
    }
}
