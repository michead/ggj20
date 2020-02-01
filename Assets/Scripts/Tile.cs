using UnityEngine;

public class Tile : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        animator.Play("Open");
    }

    public void Close()
    {
        animator.Play("Close");
    }
}
