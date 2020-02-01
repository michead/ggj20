using UnityEngine;

public class Camera : MonoBehaviour
{
    public float boomMultiplier = 1.0f;

    private GameObject player1;
    private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("player1");
        player2 = GameObject.FindGameObjectWithTag("player2");

        UpdateTransform();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        var target = (player1.transform.position + player2.transform.position) / 2;
        var direction = (target - transform.position).normalized;
        var lookAtRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * boomMultiplier);
    }
}
