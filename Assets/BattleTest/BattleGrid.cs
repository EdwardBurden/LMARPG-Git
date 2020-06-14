using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid : MonoBehaviour
{
    public int Width = 10;
    public int Depth = 10;
    public Vector2[,] CustomGrid;
    public float GridSize = 1;

    public float MaxAlignmentDistance = 100;

    List<Vector3> Cubes = new List<Vector3>();

    public BattleCharacter BattleCharacter;
    public Vector2 CharPos;

    private void Awake()
    {
        SetupGrid();
    }

    private void SetupGrid()
    {
        Cubes = new List<Vector3>();
        CustomGrid = new Vector2[Width, Depth];
        Vector2 offset = new Vector2(this.transform.position.x - ((Width / 2) * GridSize), this.transform.position.z - ((Depth / 2) * GridSize));
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Depth; j++)
            {
                CustomGrid[i, j] = new Vector2(offset.x + (i * GridSize), offset.y + (j * GridSize));
            }
        }
    }

    void OnDrawGizmos()
    {
     /*   for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Depth; j++)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawCube(new Vector3(CustomGrid[i, j].x, 0, CustomGrid[i, j].y), Vector3.one * GridSize);
            }
        }
        foreach (var item in Cubes)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(item, Vector3.one * GridSize);
        }
        foreach (var item in BattleCharacter.GetCharacterRange((int)CharPos.x, (int)CharPos.y, CustomGrid, Width, Depth))
        {
            Gizmos.color = Color.green;
            Vector2 pos = new Vector2(CustomGrid[(int)item.x, (int)item.y].x, CustomGrid[(int)item.x, (int)item.y].y);
            Vector3 posvector3 = new Vector3(pos.x, 0, pos.y);
            Gizmos.DrawCube(posvector3, Vector3.one * GridSize);
        }
        8888888888888*/
    }
    internal void AlignObjects(List<BattleCharacter> characters)
    {
        BattleCharacter = characters[0];
        SetupGrid();
        List<Vector2> taken = new List<Vector2>();
        foreach (var item in characters)
        {
            float distMax = MaxAlignmentDistance;
            Vector2 arrayPos = Vector2.zero;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Depth; j++)
                {
                    float dist = Vector2.Distance(new Vector2(item.transform.position.x, item.transform.position.z), CustomGrid[i, j]);
                    if (dist < distMax && !taken.Exists(x => x.x == i && x.y == j))
                    {
                        distMax = dist;
                        arrayPos = new Vector2(i, j);
                    }
                }
            }
            taken.Add(arrayPos);
            Vector2 pos = new Vector2(CustomGrid[(int)arrayPos.x, (int)arrayPos.y].x, CustomGrid[(int)arrayPos.x, (int)arrayPos.y].y);
            Cubes.Add(new Vector3(pos.x, 0, pos.y));
        }
        CharPos = taken[0];
    }
}
