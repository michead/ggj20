using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_progess : MonoBehaviour
{
    [SerializeField]
    List<GameObject> mesh_pairs;
    [SerializeField]
    Clock clock;
    [SerializeField]
    Spawner sp;

    [SerializeField]
    Director dir;

    float time_till_brake = 22.0f;
    float time_between_brakes = 2.5f; // + random 0 -> 3
    List<int> unbroken;

    bool all_broken = false;

    // Start is called before the first frame update
    void Start()
    {
        unbroken = new List<int>();
        for (int i = 0; i < mesh_pairs.Count / 2; i++)
        {
            unbroken.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time_till_brake = Mathf.Clamp(time_till_brake - Time.deltaTime, 0.0f, 9999.0f);

        if (time_till_brake <= 0.0f && !all_broken)
        {
            int to_brake = unbroken[Random.Range(0, unbroken.Count)];
            mesh_pairs[to_brake * 2].SetActive(false);
            mesh_pairs[to_brake * 2 + 1].SetActive(true);
            // Add particles or sth
            dir.OnDestruction();

            unbroken.Remove(to_brake);

            time_till_brake = time_between_brakes + Random.value * 3.0f;

            if (unbroken.Count == 0)
                all_broken = true;
        }
    }
}
