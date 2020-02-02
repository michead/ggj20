using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class Director : MonoBehaviour
{
    public StringUnityEvent SpawnEvent;
    public GameObject Camera;
    public GameObject PostProcessing;

    [ReadOnly]
    public GameObject[] Puzzles;

    // Start is called before the first frame update
    void Start()
    {
        CollectAllPuzzles();
        Invoke("SpawnPuzzle", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Just for testing.
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     OnDestruction();
        // }
    }

    public void SpawnPuzzle()
    {
        var puzzleName = PickRandPuzzle();
        SpawnEvent.Invoke(puzzleName);
    }

    public void OnPuzzleSolved()
    {
        Invoke("SpawnPuzzle", 1.0f);
    }

    private void CollectAllPuzzles()
    {
        string[] guids = AssetDatabase.FindAssets(null, new[] { "Assets/Resources/Prefabs/Puzzles"});
        var puzzles = new List<GameObject>();

        var i = 0;

        foreach (string guid in guids)
        {
            var puzzle = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
            if (puzzle.tag == "Puzzle") {
                puzzles.Add(puzzle);
            }
            i++;
        }

        Puzzles = puzzles.ToArray();
    }

    private string PickRandPuzzle()
    {
        return Puzzles[Random.Range(0, Puzzles.Length)].name;
    }

    private void OnDestruction() {
        Camera.GetComponent<CameraShake>().Shake(1f, 0.1f);
        PostProcessing.GetComponent<PostProcessing>().ChromaticAberration();
    }
}
