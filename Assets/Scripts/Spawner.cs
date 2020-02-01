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
    private IList<GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject> {
            GameObject.FindGameObjectWithTag("player1"),
            GameObject.FindGameObjectWithTag("player2"),
        };

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

                if (GetTilesOccupiedByPlayers().Contains(t)) {
                    // Try again another position as the one computed is occupied.
                    // FIXME: This leads potentially to a stack overflow.
                    Spawn(tileId);
                    return;
                }
            }
        }

        puzzlePosition /= (puzzleComp.Dimensions[0] * puzzleComp.Dimensions[1]);
        puzzlePosition.y = -2.0f;
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

    public IList<GameObject> GetTilesOccupiedByPlayers()
    {
        var occupiedTiles = new List<GameObject>();

        foreach (var p in players) {
            foreach (var t in Tiles) {
                const float halfSideSize = 0.5f;
                Rect rect = new Rect(
                    t.transform.position.x - halfSideSize,
                    -t.transform.position.y - halfSideSize,
                    halfSideSize * 2,
                    halfSideSize * 2);

                if (rect.Contains(new Vector2(p.transform.position.x, p.transform.position.y))) {
                    occupiedTiles.Add(t);
                }
            }
        }

        return occupiedTiles;
    }
}
