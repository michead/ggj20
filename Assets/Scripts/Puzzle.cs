using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public UnityEvent PuzzleSolvedEvent;
    public int[] Dimensions = new int[2];
    public float ShowTimeout = 0.5f;
    public float HideTimeout = 1.0f;

    private Animator animator;
    private Director director;
    private Spawner spawner;
    private bool isSolved;

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
}
