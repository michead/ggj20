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
    Director dir;
    [SerializeField]
    Spawner sp;

    float time_till_brake = 25.0f;
    float time_between_brakes = 2.5f; // + random 0 -> 3
    float time_till_spawn = 3.5f;
    float time_between_spawns = 0.5f; // + random 0 -> 3
    float time_between_spawns_slow = 3.0f; // + random 0 -> 4
    float time_to_add_on_solve = 3.0f;
    List<int> unbroken;

    bool all_broken = false;

    public void On_solved()
    {
        clock.Add_time(time_to_add_on_solve);
    }

    // Start is called before the first frame update
    void Start()
    {
        unbroken = new List<int>();
        for (int i = 0; i < mesh_pairs.Count / 2; i++)
        {
            unbroken.Add(i);
        }
    }

    float wait = 2.0f; // wait for scene change
    
    // Update is called once per frame
    void Update()
    {

        // Time over
        if (clock.time <= 0.0f && unbroken.Count >= 23)
        {
            wait -= Time.deltaTime;
            if (wait <= 0.0f)
                Application.LoadLevel(2);
        }
        else
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


            // Puzzle spawning
            time_till_spawn = Mathf.Clamp(time_till_spawn - Time.deltaTime, 0.0f, 9999.0f);
            if (time_till_spawn <= 0.0f)
            {
                if (sp.currently_spawned > 5)
                {
                    // don't spawn
                }
                else if (sp.currently_spawned > 2)
                {
                    dir.SpawnPuzzle();
                    time_till_spawn = time_between_spawns_slow + Random.value * 4.0f;
                }
                else
                {
                    dir.SpawnPuzzle();
                    time_till_spawn = time_between_spawns + Random.value * 3.0f;
                }
            }
        }
    }
}
