using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public UnityEvent PuzzleSolvedEvent;
    public int[] Dimensions = new int[2];

    private Director director;
    private Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.FindGameObjectWithTag("Director").GetComponent<Director>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        PuzzleSolvedEvent.AddListener(OnPuzzleSolved);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Solve();
        }
    }

    void OnValidate()
    {
        Assert.IsTrue(Dimensions.Length == 2);
    }

    void Solve()
    {
        Disappear();
        PuzzleSolvedEvent.Invoke();
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }

    public void OnPuzzleSolved()
    {
        director.OnPuzzleSolved();
        spawner.OnPuzzleSolved();
    }
}
