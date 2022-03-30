using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f;
    public Tilemap tilemap;

    public Tile wallTile;
    public Tile destructibleTile;

    public GameObject explosionPrefab;


    // Update is called once per frame
   
     public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        ExplodeCell(originCell);

        if (ExplodeCell(originCell + new Vector3Int(1, 0, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(2, 0, 0));
        }

        if (ExplodeCell(originCell + new Vector3Int(0, 1, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(0, 2, 0));
        }

        if (ExplodeCell(originCell + new Vector3Int(-1, 0, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(-2, 0, 0));
        }

        if (ExplodeCell(originCell + new Vector3Int(0, -1, 0)))
        {
            ExplodeCell(originCell + new Vector3Int(0, -2, 0));
        }
    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == wallTile)
        {
            return false;
        }
        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);
        return true;
    }
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            Debug.Log("Boom");
            //FindObjectOfType<MapDestroyer>().Explode(transform.position);
            Explode(transform.position);
            Destroy(gameObject);
        }
    }
}
