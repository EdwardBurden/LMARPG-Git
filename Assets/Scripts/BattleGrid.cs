using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inputmanager;

public struct BattleNode
{
    public Vector3 CentrePosition;
    public bool Walkable;
    public bool Vacant;

    public BattleNode(Vector3 position, bool walkable, bool vacant)
    {
        CentrePosition = position;
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
                int layerMask = 1 << 8;
                layerMask = ~layerMask;//TODO use better raycast technique
                bool hit = Physics.Raycast(rayCastOrigin, Vector3.down, out floorHit, 100, layerMask);
                bool walkable = hit && floorHit.collider.gameObject.tag == "Floor";
                Vector3 heightPos;
                if (walkable)
                {
                    heightPos = floorHit.point;
                }
                else
                {
                    heightPos = new Vector3(flatPosition.x, 0, flatPosition.y);
                }
                CustomGrid[i, j] = new BattleNode(heightPos, walkable, true);
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
                    Gizmos.DrawCube(CustomGrid[i, j].CentrePosition, Vector3.one * 0.4f);
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
                float dist = Vector3.Distance(worldPos, CustomGrid[i, j].CentrePosition);
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
            item.MoveTo(CustomGrid[(int)arrarypos.x, (int)arrarypos.y].CentrePosition);
        }
    }
}
