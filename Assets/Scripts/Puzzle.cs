using System.Collections.Generic;
using System.Linq;
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
    public Material PlasticShiny;

    private Animator animator;
    private Director director;
    private Spawner spawner;
    public bool isSolved;

    // Are players locked in with teh puzzle
    public bool p1_locked = false;
    public bool p2_locked = false;
    
    Game_progess gp;

    // Start is called before the first frame update
    void Start()
    {
        gp = GameObject.Find("InputManager").GetComponent<Game_progess>();
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

        gp.On_solved();

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
            .Where(c => c.gameObject.tag.Contains("SupportsOutline"))
            .ToList();

        foreach (var meshRenderer in meshRenderers) {
            meshRenderer.materials = new List<Material>(meshRenderer.materials).ToArray();
            var materials = new Material[2];

            for (var i = 0; i < meshRenderer.materials.Length; i++) {
                materials[i] = meshRenderer.materials[i];
            }

            if (p1_locked || p2_locked)
            {
                // Outline puzzle if it's being interacted with.
                if (!materials.Any(m => m?.name == OutlineMaterial.name)) {
                    
                    if (meshRenderer.tag.Contains("VialContainer")) {
                        materials[0] = new Material(OutlineMaterial);
                        meshRenderer.materials = materials;
                    } else {
                        materials[1] = new Material(OutlineMaterial);
                        meshRenderer.materials = materials;
                    } 
                }
            }
            else
            {
                // Remove outline if puzzle is not locked anymore.
                // This assumes the materials array has been preset to contain 2 elements.
                if (meshRenderer.gameObject.tag.Contains("VialContainer")) {
                    meshRenderer.materials[0] = PlasticShiny;
                } else {
                    if (meshRenderer.materials.Length > 1) {
                        meshRenderer.materials[1] = null;
                    }
                }
            }
        }
    }
}
