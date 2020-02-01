using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    // Start is called before the first frame update
    void Awake() {
        //pause the game
        Time.timeScale = 0;
    }

    public void UnPause() {
        //Time.timeScale = 1;
        Destroy(gameObject);
    }
    private void OnDestroy() {
        Time.timeScale = 1;
    }
}