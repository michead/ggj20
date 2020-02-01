using System.Collections;
using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChromaticAberration()
    {
        GetComponent<Animator>().Play("ChromaticAberration");
    }
}
