using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bricks : MonoBehaviour
{
    public Tilemap tile;
    // Start is called before the first frame update
    void Start()
    {
        tile = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    public void MakeDot(Vector3 pos)
    {
        Vector3Int cellpos = tile.WorldToCell(pos);

        tile.SetTile(cellpos, null);
    }
}
