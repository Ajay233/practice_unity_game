using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{
   public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

     void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = tilemap.WorldToCell(worldPoint);
        TileBase tile = tilemap.GetTile(position);
        Debug.Log(tile);
    }

}
