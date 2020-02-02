using UnityEngine;

public class Puzzle_viles : MonoBehaviour
{
    Puzzle puzzle;
    player_movement pm;
    Animator animator;

    int selectedButtonIndex = -1;
    int[] values = new int[3];
    int boostPerPush = 5;
    int epsilon = 5;

    // Start is called before the first frame update
    void Start()
    {
        puzzle = GetComponent<Puzzle>();
        pm = GameObject.Find("InputManager").GetComponent<player_movement>();
        animator = GetComponent<Animator>();

        values[0] = Random.Range(0, 90);
        values[1] = Random.Range(0, 100 - values[0] - 10);
        values[2] = 100 - values[1] - values[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzle.p1_locked)
        {
            if (pm.p1.B)
            {
                puzzle.p1_locked = false;
                pm.p1.is_solving = false;
            }
            if (pm.p1.A)
            {
                RaiseVial();
            }
            if (pm.p1.right) {
                selectedButtonIndex = (selectedButtonIndex + 1) % 3;
            }
            if (pm.p1.left) {
                selectedButtonIndex--;
                if (selectedButtonIndex < 0) {
                    selectedButtonIndex = 2;
                }
            }
        }
        // P2
        else if (puzzle.p2_locked)
        {
            if (pm.p2.B)
            {
                puzzle.p2_locked = false;
                pm.p2.is_solving = false;
            }
            if (pm.p2.A)
            {
                RaiseVial();
            }
            if (pm.p2.right) {
                selectedButtonIndex = (selectedButtonIndex + 1) % 3;
            }
            if (pm.p2.left) {
                selectedButtonIndex--;
                if (selectedButtonIndex < 0) {
                    selectedButtonIndex = 2;
                }
            }
        } else {
            selectedButtonIndex = 0;
        }
    }

    void RaiseVial()
    {
        var i = selectedButtonIndex;
        var actualBoost = Mathf.Min(boostPerPush, 100 - values[selectedButtonIndex]);
        if (actualBoost > 0) {
            values[i] = actualBoost;
            var quoto = actualBoost % 2;
            values[(i + 1) % 3] = actualBoost / 2;
            values[(i + 2) % 3] = (actualBoost / 2) + quoto;
        }

        UpdateMesh();

        if (Mathf.Abs(values[0] - values[1]) < epsilon &&
            Mathf.Abs(values[0] - values[2]) < epsilon) {
            puzzle.Solve();
        }
    }

    void UpdateMesh()
    {
        var vial0 = GameObject.FindGameObjectWithTag("Vial0");
        var vial1 = GameObject.FindGameObjectWithTag("Vial1");
        var vial2 = GameObject.FindGameObjectWithTag("Vial2");

        vial0.transform.localScale = new Vector3(vial0.transform.localScale.x, vial0.transform.localScale.y, 20 + (values[0] * 0.5f));
        vial1.transform.localScale = new Vector3(vial1.transform.localScale.x, vial1.transform.localScale.y, 20 + (values[1] * 0.5f));
        vial2.transform.localScale = new Vector3(vial2.transform.localScale.x, vial2.transform.localScale.y, 20 + (values[2] * 0.5f));
    }
}
