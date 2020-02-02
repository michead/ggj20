using UnityEngine;

public class player_movement : MonoBehaviour {
    [SerializeField]
    float move_speed;
    Rigidbody rb1, rb2;
    Vector3 vel1, vel2;

    public struct player1 { public bool A, B, X, Y, left, right, up, down, is_solving; }
    public struct player2 { public bool A, B, X, Y, left, right, up, down, is_solving; }
    public player1 p1;
    public player2 p2;

    private GameObject pgo1;
    private GameObject pgo2;

    public PauseMenu pausePrefab;

    //public bool is_p1_solving, is_p2_solving = false;
    // Start is called before the first frame update
    void Start() {
        pgo1 = GameObject.Find("Player1");
        pgo2 = GameObject.Find("Player2");

        rb1 = pgo1.GetComponent<Rigidbody>();
        rb2 = pgo2.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Pause") > 0 && Time.timeScale != 0) {
            //pause the game
            GameObject.Instantiate(pausePrefab);
        }

        // Player 1
        // Movement
        if (!p1.is_solving) {
            vel1 = new Vector3(0.0f, 0.0f, 0.0f);
            vel1 += new Vector3(1.0f, 0.0f, 0.0f) * Input.GetAxis("Horizontal_p1") * move_speed;
            vel1 += new Vector3(0.0f, 0.0f, 1.0f) * Input.GetAxis("Vertical_p1") * move_speed;
            rb1.velocity = vel1;
            if (rb1.velocity.magnitude < 0.2f) {
                rb1.velocity = Vector3.zero;
            }
            p1.A = Input.GetAxis("A_p1") > 0;
        }
        // Puzzles
        else {
            rb1.velocity = new Vector3(0.0f, 0.0f, 0.0f);
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
        if (!p2.is_solving) {
            vel2 = new Vector3(0.0f, 0.0f, 0.0f);
            vel2 += new Vector3(1.0f, 0.0f, 0.0f) * Input.GetAxis("Horizontal_p2") * move_speed;
            vel2 += new Vector3(0.0f, 0.0f, 1.0f) * Input.GetAxis("Vertical_p2") * move_speed;
            rb2.velocity = vel2;
            if (rb2.velocity.magnitude < 0.2f) {
                rb2.velocity = Vector3.zero;
            }
            p2.A = Input.GetAxis("A_p2") > 0;
        }
        // Puzzles
        else {
            rb2.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            p2.left = Input.GetAxis("Horizontal_p1") < 0;
            p2.right = Input.GetAxis("Horizontal_p1") > 0;
            p2.up = Input.GetAxis("Horizontal_p1") > 0;
            p2.down = Input.GetAxis("Horizontal_p1") < 0;
            p2.A = Input.GetAxis("A_p1") > 0;
            p2.B = Input.GetAxis("B_p1") > 0;
            p2.X = Input.GetAxis("X_p1") > 0;
            p2.Y = Input.GetAxis("Y_p1") > 0;
        }

        if (rb1.velocity.magnitude > 0) {
            var rot1 = pgo1.transform.rotation;
            var y = Quaternion.LookRotation(rb1.velocity).y;
            pgo1.transform.rotation = new Quaternion(rot1.x, y, rot1.z, rot1.w);
        }

        pgo1.GetComponentInChildren<Animator>().SetFloat("Speed", Mathf.Min(1, Mathf.Abs(rb1.velocity.magnitude)));
        Debug.DrawLine(pgo1.transform.position, pgo1.transform.position + rb1.velocity.normalized);

        if (rb2.velocity.magnitude > 0) {
            var rot2 = pgo2.transform.rotation;
            var y = Quaternion.LookRotation(rb2.velocity).y;
            pgo2.transform.rotation = new Quaternion(rot2.x, y, rot2.z, rot2.w);
        }

        pgo2.GetComponentInChildren<Animator>().SetFloat("Speed", Mathf.Min(1, Mathf.Abs(rb2.velocity.magnitude)));
        Debug.DrawLine(pgo2.transform.position, pgo2.transform.position + rb2.velocity.normalized);
    }
}
