using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [ReadOnly]
    public GameObject[] Tiles;

    private const int xSideSize = 9;
    private const int zSideSize = 5;
    private IList<GameObject> slidingTiles;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeTiles()
    {
        Tiles = GameObject.FindGameObjectsWithTag("Tile");

        // Order tiles far -> near, left -> right.
        Tiles = Tiles.OrderBy(t => -t.transform.position.z * 10 + t.transform.position.x).ToArray();
    }

    public void Spawn(string tileId)
    {
        var puzzleType = Resources.Load($"Prefabs/{tileId}") as GameObject;
        var puzzleComp = puzzleType.GetComponent<Puzzle>();

        var randX = Random.Range(0, xSideSize - puzzleComp.Dimensions[0]);
        var randZ = Random.Range(0, zSideSize - puzzleComp.Dimensions[1] - 1);

        var puzzlePosition = Vector3.zero;
        slidingTiles = new List<GameObject>();
        for (var x = 0; x < puzzleComp.Dimensions[0]; x++)
        {
            for (var z = 0; z < puzzleComp.Dimensions[1]; z++)
            {
                var t = GetTileAt(randX + x, randZ + z);
                slidingTiles.Add(t);
                puzzlePosition += t.transform.position;
            }
        }

        puzzlePosition /= (puzzleComp.Dimensions[0] * puzzleComp.Dimensions[1]);
        var puzzle = Instantiate(puzzleType, puzzlePosition, Quaternion.identity);

        OpenSliders();
    }

    private void OpenSliders()
    {
        // TODO: Open animation
        foreach (var st in slidingTiles)
        {
            st.GetComponent<Tile>().Open();
        }
    }

    private void CloseSliders()
    {
        // TODO: Close animation
        foreach (var st in slidingTiles)
        {
            st.GetComponent<Tile>().Close();
        }
    }

    private GameObject GetTileAt(int x, int z)
    {
        return Tiles[5 + x + xSideSize * z];
    }

    public void OnPuzzleSolved()
    {
        CloseSliders();
    }
}
