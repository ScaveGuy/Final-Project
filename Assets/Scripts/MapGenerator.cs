using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TileType
{
    Grass,
    Water,
    Dirt,
    Mountain,
    Forest,
    Building
}

[System.Serializable]
public struct TilePrefabEntry
{
    public TileType Type;
    public GameObject Prefab;
}

public class MapGenerator : MonoBehaviour
{
    public int MapWidth;
    public int MapHeight;
    public List<TilePrefabEntry> PrefabList;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMap()
    {
        float MinX = -(MapWidth / 2);
        float MaxZ = MapHeight / 2;
        float MaxX = MapWidth / 2;
        float MinZ = -(MapHeight / 2);

        float XPos = MinX;
        int Column = 0;
        int Row = 0;
        float ZPos = MaxZ;

        while(ZPos >= MinZ)
        {
            while( XPos <= MaxX)
            {
                GameObject newTileObj = CreateRandomTileAt(XPos, ZPos);
                MapTile newTile = newTileObj.GetComponent<MapTile>();
                newTile.Column = Column;
                newTile.Row = Row;

                XPos++;
                Column++;
            }

            ZPos--;
            Row++;
            XPos = MinX;
            Column = 0;
        }

    }

    private GameObject CreateRandomTileAt(float x, float z)
    {
        // Generate a random index to decide which prefab to create
        int tileIndex = Random.Range(0, PrefabList.Count);

        //Grab that prefab from the list
        GameObject prefab = PrefabList[tileIndex].Prefab;

        // Return the new instance object
        GameObject newTileObj = Instantiate(prefab);

        // Place the new object at the desired position
        newTileObj.transform.position = new Vector3(x, 0, z);


        return newTileObj;
    }
}
