using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class Director : MonoBehaviour
{
    public StringUnityEvent SpawnEvent;
    public GameObject Camera;
    public GameObject PostProcessing;

    public GameObject[] Puzzles;

    // Start is called before the first frame update
    void Start()
    {
        //CollectAllPuzzles();
        //Invoke("SpawnPuzzle", 1.0f);
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
        //Invoke("SpawnPuzzle", 1.0f);
    }

    private string PickRandPuzzle()
    {
        return Puzzles[Random.Range(0, Puzzles.Length)].name;
    }

    public void OnDestruction() {
        Camera.GetComponent<CameraShake>().Shake(1f, 0.1f);
        PostProcessing.GetComponent<PostProcessing>().ChromaticAberration();
    }
}
