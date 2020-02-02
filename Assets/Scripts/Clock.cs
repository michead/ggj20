using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float time = 20.0f;
    [SerializeField]
    TextMesh tm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = Mathf.Clamp(time - Time.deltaTime, 0.0f, 9999.0f);

        string text = "";
        if (time >= 10.0f)
            text = time.ToString("F2");
        else
            text = "0" + time.ToString("F2");
        text = text.Replace('.', ':');
        tm.text = text;
    }

    void Add_time(float t)
    {
        time += t;
    }
}
