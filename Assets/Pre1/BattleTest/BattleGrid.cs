using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inputmanager;

public struct BattleNode
{
    public Vector2 Position;
    public bool Walkable;
    public bool Vacant;

    public BattleNode(Vector2 position, bool walkable, bool vacant)
    {
        Position = position;
        Walkable = walkable;
        Vacant = vacant;
    }
}

public class BattleGrid : MonoBehaviour
{
    public int Width = 10;
    public int Depth = 10;
    public BattleNode[,] CustomGrid = new BattleNode[100, 100];
    public float GridSize = 1;

    public float MaxAlignmentDistance = 100;
    private float RaycastHeight = 3;

    private List<PlayerCharcater> PCharacter;

    public void Activate(List<PlayerCharcater> playerCharcater)
    {
        PCharacter = playerCharcater;
        SetupGrid();
        AlignObjects();
    }

    private void SetupGrid()
    {
        CustomGrid = new BattleNode[Width, Depth];
        Vector2 offset = new Vector2(this.transform.position.x - ((Width / 2) * GridSize), this.transform.position.z - ((Depth / 2) * GridSize));
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Depth; j++)
            {
                Vector2 flatPosition = new Vector2(offset.x + (i * GridSize), offset.y + (j * GridSize));
                Vector3 rayCastOrigin = new Vector3(flatPosition.x, RaycastHeight, flatPosition.y);
                RaycastHit floorHit;
                bool hit = Physics.Raycast(rayCastOrigin, Vector3.down, out floorHit);
                bool walkable = hit && floorHit.collider.gameObject.tag == "Floor";
                CustomGrid[i, j] = new BattleNode(flatPosition, walkable, true);
            }
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Depth; j++)
            {
                Gizmos.color = Color.red;
                if (CustomGrid[i, j].Walkable)
                    Gizmos.DrawCube(new Vector3(CustomGrid[i, j].Position.x, 0, CustomGrid[i, j].Position.y), Vector3.one * GridSize);
            }
        }
    }


    private Vector2 GetCharactersClosestValidArrayPosition(Vector3 worldPos)
    {
        float distMax = MaxAlignmentDistance;
        int x = 0, y = 0;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Depth; j++)
            {
                float dist = Vector2.Distance(new Vector2(worldPos.x, worldPos.z), CustomGrid[i, j].Position);
                if (dist < distMax && CustomGrid[i, j].Vacant && CustomGrid[i, j].Walkable)
                {
                    distMax = dist;
                    x = i;
                    y = j;
                }
            }
        }
        return new Vector2(x, y);
    }

    public void AlignObjects()
    {
        foreach (var item in PCharacter)
        {
            Vector2 arrarypos = GetCharactersClosestValidArrayPosition(item.transform.position);
            CustomGrid[(int)arrarypos.x, (int)arrarypos.y].Vacant = false;
            Vector2 pos = new Vector2(CustomGrid[(int)arrarypos.x, (int)arrarypos.y].Position.x, CustomGrid[(int)arrarypos.x, (int)arrarypos.y].Position.y);
            item.MoveTo(new Vector3(pos.x, 0, pos.y));
        }
    }
}
