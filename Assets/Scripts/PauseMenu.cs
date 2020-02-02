using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    // Start is called before the first frame update

    float timer = 0.5f;
    void Awake() {
        //pause the game
        Time.timeScale = 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetAxis("Pause") > 0 && timer <= 0.0f)
        {
            UnPause();
        }
    }

    public void UnPause() {
        //Time.timeScale = 1;
        Destroy(gameObject);
    }
    private void OnDestroy() {
        Time.timeScale = 1;
    }
}