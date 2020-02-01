using UnityEngine;
using UnityEngine.Events;

public class Director : MonoBehaviour
{
    public StringUnityEvent SpawnEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("SpawnPuzzle", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPuzzle()
    {
        SpawnEvent.Invoke("Cube");
    }

    public void OnPuzzleSolved()
    {
        Invoke("SpawnPuzzle", 1.0f);
    }
}
