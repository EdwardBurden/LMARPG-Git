using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    List<Vector3> Cubes = new List<Vector3>();

    public List<BattleCharacter> BattleCharacter = new List<BattleCharacter>();

    public void Activate()
    {
        SetupGrid();
        AlignObjects();
    }

    private void SetupGrid()
    {
        Cubes = new List<Vector3>();
        CustomGrid = new BattleNode[Width, Depth];
        Vector2 offset = new Vector2(this.transform.position.x - ((Width / 2) * GridSize), this.transform.position.z - ((Depth / 2) * GridSize));
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Depth; j++)
            {
                CustomGrid[i, j] = new BattleNode(new Vector2(offset.x + (i * GridSize), offset.y + (j * GridSize)), true, true);
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

                Gizmos.DrawCube(new Vector3(CustomGrid[i, j].Position.x, 0, CustomGrid[i, j].Position.y), Vector3.one * GridSize);
            }
        }
        foreach (var item in Cubes)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(item, Vector3.one * GridSize);
        }
    }


    private Vector2 GetArrayPositionFromWorldPosition(Vector3 worldPos)
    {
        float distMax = MaxAlignmentDistance;
        int x = 0, y = 0;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Depth; j++)
            {
                float dist = Vector2.Distance(new Vector2(worldPos.x, worldPos.z), CustomGrid[i, j].Position);
                if (dist < distMax && CustomGrid[i, j].Vacant)
                {
                    distMax = dist;
                    x = i;
                    y = j;
                }
            }
        }
        CustomGrid[x, y].Vacant = false;
        return new Vector2(x, y);
    }

    public void AlignObjects()
    {
        Cubes = new List<Vector3>();
        foreach (var item in BattleCharacter)
        {
            CustomGrid[(int)item.ArrayPosition.x, (int)item.ArrayPosition.y].Vacant = true;
            Vector2 arrarypos = GetArrayPositionFromWorldPosition(item.transform.position);
            CustomGrid[(int)arrarypos.x, (int)arrarypos.y].Vacant = false;
            Vector2 pos = new Vector2(CustomGrid[(int)arrarypos.x, (int)arrarypos.y].Position.x, CustomGrid[(int)arrarypos.x, (int)arrarypos.y].Position.y);
            Cubes.Add(new Vector3(pos.x, 0, pos.y));
            item.MoveTo(new Vector3(pos.x, 0, pos.y));
        }
    }
}
