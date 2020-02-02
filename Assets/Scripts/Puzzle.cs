﻿using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public UnityEvent PuzzleSolvedEvent;
    public int[] Dimensions = new int[2];
    public float ShowTimeout = 0.5f;
    public float HideTimeout = 1.0f;
    public Material OutlineMaterial;

    private Animator animator;
    private Director director;
    private Spawner spawner;
    public bool isSolved;

    // Are players locked in with teh puzzle
    public bool p1_locked = false;
    public bool p2_locked = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<Director>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        PuzzleSolvedEvent.AddListener(OnPuzzleSolved);
        Invoke("Show", ShowTimeout);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOutline();
    }

    void OnValidate()
    {
        Assert.IsTrue(Dimensions.Length == 2);
    }

    public void Solve()
    {
        if (isSolved) {
            return;
        }

        Hide();
        PuzzleSolvedEvent.Invoke();
        isSolved = true;
    }

    public void Show() 
    {
        animator.Play("Show");
    }

    public void Hide()
    {
        animator.Play("Hide");
        Destroy(gameObject, HideTimeout);
    }

    public void OnPuzzleSolved()
    {
        director.OnPuzzleSolved();
        spawner.OnPuzzleSolved();
    }

    private void UpdateOutline()
    {
        var meshRenderers = gameObject
            .GetComponentsInChildren<MeshRenderer>()
            .Where(c => c.gameObject.tag == "SupportsOutline");

        foreach (var meshRenderer in meshRenderers) {
            var materials = meshRenderer.materials;
            if (materials.Length < 2) {
                meshRenderer.materials = new [] { materials[0], null };
            }

            if (p1_locked || p2_locked)
            {
                // Outline puzzle if it's being interacted with.
                if (!materials.Any(m => m.name == OutlineMaterial.name)) {
                    materials[1] = OutlineMaterial;
                    meshRenderer.materials = materials;
                }
            }
            else
            {
                // Remove outline if puzzle is not locked anymore.
                // This assumes the materials array has been preset to contain 2 elements.
                meshRenderer.materials[1] = null;
            }
        }
    }
}
