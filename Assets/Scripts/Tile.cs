using UnityEngine;

public class Tile : MonoBehaviour
{
    private Animator animator;
    private const float slideAnimationDuration = 1.0f;

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
        GetComponent<BoxCollider>().enabled = true;
        animator.Play("Open");
    }

    public void Close()
    {
        animator.Play("Close");

        // Account for animation to be over.
        Invoke("DisableCollider", slideAnimationDuration);
    }

    public void DisableCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
