using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]
    float move_speed;
    Rigidbody rb1, rb2;
    Vector3 vel1, vel2;

    public struct player1 { public bool A, B, X, Y, left, right, up, down, is_solving; }
    public struct player2 { public bool A, B, X, Y, left, right, up, down, is_solving; }
    public player1 p1;
    public player2 p2;

    //public bool is_p1_solving, is_p2_solving = false;
    // Start is called before the first frame update
    void Start()
    {
        rb1 = GameObject.Find("Player1").GetComponent<Rigidbody>();
        rb2 = GameObject.Find("Player2").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player 1
        // Movement
        if (!p1.is_solving)
        {
            vel1 = new Vector3(0.0f, 0.0f, 0.0f);
            vel1 += new Vector3(1.0f, 0.0f, 0.0f) * Input.GetAxis("Horizontal_p1") * move_speed;
            vel1 += new Vector3(0.0f, 0.0f, 1.0f) * Input.GetAxis("Vertical_p1") * move_speed;
            rb1.velocity = vel1;
        }
        // Puzzles
        else
        {
            p1.left = Input.GetAxis("Horizontal_p1") < 0;
            p1.right = Input.GetAxis("Horizontal_p1") > 0;
            p1.up = Input.GetAxis("Horizontal_p1") > 0;
            p1.down = Input.GetAxis("Horizontal_p1") < 0;
            p1.A = Input.GetAxis("A_p1") > 0;
            p1.B = Input.GetAxis("B_p1") > 0;
            p1.X = Input.GetAxis("X_p1") > 0;
            p1.Y = Input.GetAxis("Y_p1") > 0;
        }
        // Player 2
        // Movement
        if (!p2.is_solving)
        {
            vel2 = new Vector3(0.0f, 0.0f, 0.0f);
            vel2 += new Vector3(1.0f, 0.0f, 0.0f) * Input.GetAxis("Horizontal_p2") * move_speed;
            vel2 += new Vector3(0.0f, 0.0f, 1.0f) * Input.GetAxis("Vertical_p2") * move_speed;
            rb2.velocity = vel2;
        }
        // Puzzles
        else
        {
            p2.left = Input.GetAxis("Horizontal_p1") < 0;
            p2.right = Input.GetAxis("Horizontal_p1") > 0;
            p2.up = Input.GetAxis("Horizontal_p1") > 0;
            p2.down = Input.GetAxis("Horizontal_p1") < 0;
            p2.A = Input.GetAxis("A_p1") > 0;
            p2.B = Input.GetAxis("B_p1") > 0;
            p2.X = Input.GetAxis("X_p1") > 0;
            p2.Y = Input.GetAxis("Y_p1") > 0;
        }
    }
}
