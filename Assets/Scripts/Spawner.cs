using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [ReadOnly]
    public GameObject[] Tiles;

    public UnityEvent SpawnEvent;

    private int sideSize;
    private IList<GameObject> slidingTiles;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTiles();
        StartCoroutine("TestDriver");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeTiles()
    {
        Tiles = GameObject.FindGameObjectsWithTag("Tile");

        // Check number of tiles is a power of 2.
        var fSideSize = Mathf.Sqrt(Tiles.Length);
        var iSideSize = Mathf.RoundToInt(fSideSize);
        Assert.AreApproximatelyEqual(iSideSize, fSideSize, 0.1f);

        sideSize = iSideSize;

        // Order tiles far -> near, left -> right.
        Tiles = Tiles.OrderBy(t => t.transform.position.z * 10 + t.transform.position.x).ToArray();
    }

    void Spawn(int widthX, int widthZ)
    {
        // var randX = Random.Range(0, sideSize - tile.Dimensions[0]);
        // var randZ = Random.Range(0, sideSize - tile.Dimensions[1]);

        var randX = Random.Range(0, sideSize - widthX);
        var randZ = Random.Range(0, sideSize - widthZ);

        slidingTiles = new List<GameObject>();
        for (var x = 0; x < widthX; x++)
        {
            for (var z = 0; z < widthZ; z++)
            {
                var t = GetTileAt(randX + x, randZ + z);
                slidingTiles.Add(t);
            }
        }

        OpenSliders();
        Invoke("CloseSliders", 3.0f);
    }

    private void OpenSliders()
    {
        foreach (var st in slidingTiles)
        {
            st.SetActive(false);
        }
    }

    private void CloseSliders()
    {
        foreach (var st in slidingTiles)
        {
            st.SetActive(true);
        }
    }

    private GameObject GetTileAt(int x, int z)
    {
        return Tiles[x + sideSize * z];
    }

    public IEnumerator TestDriver()
    {
        var i = 0;
        while (true)
        {
            if (i % 2 == 0) {
                Spawn(1, 1);
            } else {
                Spawn(2, 2);
            }

            i++;

            yield return new WaitForSeconds(5.5f);
        }
    }
}
