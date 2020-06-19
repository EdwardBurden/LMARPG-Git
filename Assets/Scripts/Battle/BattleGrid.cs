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
    private float RaycastHeight = 10;

    private List<PlayerCharcater> PCharacter;

    private BattleNode HoveredGrid;

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
        Gizmos.color = Color.blue;
        if (HoveredGrid.Walkable)
            Gizmos.DrawCube(HoveredGrid.CentrePosition + Vector3.up, Vector3.one);
    }

    public BattleNode GetNodeAtWorldPosition(Vector3 worldposition)
    {
        Vector2 minMPosition = new Vector2(this.transform.position.x - ((Width / 2) * GridSize), this.transform.position.z - ((Depth / 2) * GridSize));
        Vector2 maxPosition = new Vector2(minMPosition.x + (Width * GridSize), minMPosition.y + (Depth * GridSize));

        float xPercent = (worldposition.x - minMPosition.x) / (maxPosition.x - minMPosition.x);
        float zPercent = (worldposition.z - minMPosition.y) / (maxPosition.y - minMPosition.y);

        int x = Mathf.Clamp(Mathf.RoundToInt(xPercent * Width), 0, Width - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt(zPercent * Depth), 0, Depth - 1);

        return CustomGrid[x, y];
    }

    public void UpdateHoverGridPosition(Vector3 hoverPosition)
    {
        HoveredGrid = GetNodeAtWorldPosition(hoverPosition);
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
