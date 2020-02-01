using UnityEngine;
using UnityEngine.Assertions;

public class Tile : MonoBehaviour
{
    public int[] Dimensions = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        Assert.IsTrue(Dimensions.Length == 2);
    }
}
