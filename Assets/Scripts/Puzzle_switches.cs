using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_switches : MonoBehaviour
{
    player_movement pm;
    Puzzle puzzle;

    bool[] switches;    // Which switches have been switched
    int selected;       // Which switch is selected

    //Animator[] animators;
    Transform [] s_t;
    float[] switch_timers;
    bool[] switch_done;
    Transform bar;

    bool left_toggle, right_toggle;

    // Start is called before the first frame update
    void Start()
    {
        //animators = new Animator[3];
        s_t = new Transform[3];
        switch_timers = new float[3];
        for (int i = 0; i < 3; i++)
            switch_timers[i] = 0.0f;
        switches = new bool[3];
        for (int i = 0; i < 3; i++)
            switches[i] = false;
        switch_done = new bool[3];
        for (int i = 0; i < 3; i++)
            switch_done[i] = false;
        selected = 1;

        foreach (Transform t in transform)
        {
            foreach (Transform tt in t)
            {
                if (tt.gameObject.name == "Lever1")
                {
                    //animators[0] = tt.gameObject.GetComponent<Animator>();
                    s_t[0] = tt;
                }
                if (tt.gameObject.name == "Lever2")
                {
                    //animators[1] = tt.gameObject.GetComponent<Animator>();
                    s_t[1] = tt;
                }
                if (tt.gameObject.name == "Lever3")
                {
                    //animators[2] = tt.gameObject.GetComponent<Animator>();
                    s_t[2] = tt;
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
                    //animators[0].Play("Flip0");
                }
                else if (selected == 1 && !switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    //animators[1].Play("Flip1");
                }
                else if (selected == 2 && !switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    //animators[2].Play("Flip2");
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
            else if (!pm.p1.left)
                left_toggle = false;
            if (pm.p1.right && !right_toggle)
            {
                if (selected < 2)
                    selected++;
                else
                    selected = 0;
                right_toggle = true;
            }
            else if (!pm.p1.right)
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
                    //animators[0].Play("Flip0");
                }
                else if (selected == 1 && !switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    //animators[1].Play("Flip1");
                }
                else if (selected == 2 && !switches[selected])
                {
                    switches[selected] = true;
                    bar.localScale = new Vector3(bar.localScale.x + 1.0f / 3.0f, 1.0f, 1.0f);
                    //animators[2].Play("Flip2");
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
            else if (!pm.p2.left)
                left_toggle = false;
            if (pm.p2.right && !right_toggle)
            {
                if (selected < 2)
                    selected++;
                else
                    selected = 0;
                right_toggle = true;
            }
            else if (!pm.p2.right)
                right_toggle = false;
        }

        if (switches[0] && !switch_done[0])
        {
            s_t[0].rotation = Quaternion.Slerp(Quaternion.Euler(-115.0f, 0.0f, 0.0f), Quaternion.Euler(60.0f, 0.0f, 0.0f), switch_timers[0]);
            switch_timers[0] = Mathf.Clamp01(switch_timers[0] + Time.deltaTime);
            switch_done[0] = switch_timers[0] >= 1.0f;
        }
        if (switches[1] && !switch_done[1])
        {
            s_t[1].rotation = Quaternion.Slerp(Quaternion.Euler(-115.0f, 0.0f, 0.0f), Quaternion.Euler(60.0f, 0.0f, 0.0f), switch_timers[1]);
            switch_timers[1] = Mathf.Clamp01(switch_timers[1] + Time.deltaTime);
            switch_done[1] = switch_timers[1] >= 1.0f;
        }
        if (switches[2] && !switch_done[2])
        {
            s_t[2].rotation = Quaternion.Slerp(Quaternion.Euler(-115.0f, 0.0f, 0.0f), Quaternion.Euler(60.0f, 0.0f, 0.0f), switch_timers[2]);
            switch_timers[2] = Mathf.Clamp01(switch_timers[2] + Time.deltaTime);
            switch_done[2] = switch_timers[2] >= 1.0f;
        }

        if (switch_done[0] && switch_done[1] && switch_done[2])
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
}
