using System.Collections.Generic;
using System.Collections;
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

    IList<GameObject> vials;
    IList<GameObject> vialContainers;
    public Material PlasticShiny;
    bool _left, _right;
    float lockTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        puzzle = GetComponent<Puzzle>();
        pm = GameObject.Find("InputManager").GetComponent<player_movement>();
        animator = GetComponent<Animator>();

        values = RandomizeValues();
        while (CanWin(values)) {
            values = RandomizeValues();
        }

        vials = new List<GameObject>{
            GameObject.FindGameObjectWithTag("Vial0"),
            GameObject.FindGameObjectWithTag("Vial1"),
            GameObject.FindGameObjectWithTag("Vial2"),
        };

        vialContainers = new List<GameObject>{
            GameObject.FindGameObjectWithTag("VialContainer0"),
            GameObject.FindGameObjectWithTag("VialContainer1"),
            GameObject.FindGameObjectWithTag("VialContainer2"),
        };

        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzle.p1_locked)
        {
            if (lockTime < 0.001f) {
                lockTime = Time.time;
            }

            if (pm.p1.B)
            {
                puzzle.p1_locked = false;
                pm.p1.is_solving = false;
            }
            if (pm.p1.A && (Time.time - lockTime) > 0.1f)
            {
                RaiseVial();
            }
            if (pm.p1.right) {
                if (!_right) {
                    _right = true;
                    selectedButtonIndex = (selectedButtonIndex + 1) % 3;
                }
            }
            if (pm.p1.left) {
                if (!_left) {
                    _left = true;
                    selectedButtonIndex--;
                    if (selectedButtonIndex < 0) {
                        selectedButtonIndex = 2;
                    }
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
            if (pm.p2.A && (Time.time - lockTime) > 0.1f)
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
            lockTime = 0f;
        }

        if (puzzle.p1_locked || puzzle.p2_locked) {
            for (var  i = 0; i < 3; i++) {
                if (i == selectedButtonIndex) {
                    continue;
                }

                vialContainers[i].tag = vialContainers[i].tag
                    .Replace("SupportsOutline", string.Empty)
                    .Trim();

                var materials = vialContainers[i].GetComponent<MeshRenderer>().materials;
                materials[0] = PlasticShiny;
                vialContainers[i].GetComponent<MeshRenderer>().materials = materials;
            }

            if (!vialContainers[selectedButtonIndex].tag.Contains("SupportsOutline")) {
                vialContainers[selectedButtonIndex].tag = vialContainers[selectedButtonIndex].tag + " SupportsOutline"; 
            }
        }

        if (!pm.p1.left) {
            _left = false;
        }

        if (!pm.p1.right) {
            _right = false;
        }
    }

    void RaiseVial()
    {
        var i = selectedButtonIndex;
        var actualBoost = Mathf.Min(boostPerPush, 100 - values[selectedButtonIndex]);
        if (actualBoost > 0) {
            values[i] = values[selectedButtonIndex] + actualBoost;
            var quoto = actualBoost % 2;
            values[(i + 1) % 3] = actualBoost / 2;
            values[(i + 2) % 3] = (actualBoost / 2) + quoto;
        }

        UpdateMesh();

        if (CanWin(values)) {
            puzzle.p1_locked = false;
            puzzle.p2_locked = false;
            pm.p1.is_solving = false;
            pm.p2.is_solving = false;
            puzzle.Solve();
        }
    }

    void UpdateMesh()
    {
        StartCoroutine(_UpdateMesh());
    }

    private  IEnumerator _UpdateMesh()
    {
        var elapsed = 0f;
        var original0 = vials[0].transform.localScale.z;
        var original1 = vials[1].transform.localScale.z;
        var original2 = vials[2].transform.localScale.z;
        var startTime = Time.time;

        while (elapsed < 0.5f)
        {
            var scale0 = 20 + (values[0] * 0.5f);
            var scale1 = 20 + (values[1] * 0.5f);
            var scale2 = 20 + (values[2] * 0.5f);

            float t = Time.time - startTime;
            var target0 = Mathf.SmoothStep(original0, scale0, t);
            var target1 = Mathf.SmoothStep(original1, scale1, t);
            var target2 = Mathf.SmoothStep(original2, scale2, t);

            vials[0].transform.localScale = new Vector3(vials[0].transform.localScale.x, vials[0].transform.localScale.y, target0);
            vials[1].transform.localScale = new Vector3(vials[1].transform.localScale.x, vials[1].transform.localScale.y, target1);
            vials[2].transform.localScale = new Vector3(vials[2].transform.localScale.x, vials[2].transform.localScale.y, target2);

            yield return null;
        }
    }

    private int[] RandomizeValues() {
        var a = Random.Range(0, 90);
        var b = Random.Range(0, 100 - a - 10);
        var c = 100 - b - a;
        return new int[] {a,b,c};
    }

    private bool CanWin(int[] values) {
        return (
            Mathf.Abs(values[0] - values[1]) < epsilon &&
            Mathf.Abs(values[0] - values[2]) < epsilon
        );
    }
}
