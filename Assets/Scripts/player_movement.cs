using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]
    float move_speed;
    Rigidbody rb1, rb2;
    Vector3 vel1, vel2;
    // Start is called before the first frame update
    void Start()
    {
        rb1 = GameObject.Find("Player1").GetComponent<Rigidbody>();
        rb2 = GameObject.Find("Player2").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vel1 = new Vector3(0.0f, 0.0f, 0.0f);
        vel2 = new Vector3(0.0f, 0.0f, 0.0f);
        vel1 += new Vector3(1.0f, 0.0f, 0.0f) * Input.GetAxis("Horizontal_p1") * move_speed;
        vel2 += new Vector3(1.0f, 0.0f, 0.0f) * Input.GetAxis("Horizontal_p2") * move_speed;
        vel1 += new Vector3(0.0f, 0.0f, 1.0f) * Input.GetAxis("Vertical_p1") * move_speed;
        vel2 += new Vector3(0.0f, 0.0f, 1.0f) * Input.GetAxis("Vertical_p2") * move_speed;
        rb1.velocity = vel1;
        rb2.velocity = vel2;
    }
}
