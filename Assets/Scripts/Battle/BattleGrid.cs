using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticFunctions;

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
    public static BattleGrid Instance;

    private BattleSetup Setup;
    public LayerMask FloorOnlyLayerMask;
    public LayerMask StaticObstaclesLayerMask;
    public int Width = 10;
    public int Depth = 10;
    public BattleNode[,] CustomGrid = new BattleNode[100, 100];
    public float GridSize = 1;

    private float MaxAlignmentDistance = Mathf.Infinity;
    private float RaycastHeight = 50;

    private BattleNode HoveredGrid;
    public List<BattleNode> SelectedCharactersMoveableArea = new List<BattleNode>();

    private void Awake()
    {
        Instance = this;
    }


    public void Activate(BattleSetup battleSetup, float radius)
    {
        Setup = battleSetup;
        Width = Depth = Mathf.FloorToInt(radius);
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
                Vector3 rayCastOrigin = new Vector3(flatPosition.x, this.transform.position.y + RaycastHeight, flatPosition.y);
                RaycastHit floorHit;
                bool hit = Physics.Raycast(rayCastOrigin, Vector3.down, out floorHit, 100, FloorOnlyLayerMask);
                bool walkable = hit && floorHit.collider.gameObject.tag == Constants.FloorTag && !Physics.CheckSphere(floorHit.point + new Vector3(0, GridSize, 0), GridSize, StaticObstaclesLayerMask);
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
            Gizmos.DrawSphere(HoveredGrid.CentrePosition + Vector3.up, 0.4f);

        foreach (var item in SelectedCharactersMoveableArea)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(item.CentrePosition + Vector3.up, Vector3.one * 0.4f);
        }
    }

    public void GetAvailableMovementNodes(BattleOrder playerCharcater)
    {
        int maxRange = playerCharcater.Character.GetCharacterData.MovementRange;
        SelectedCharactersMoveableArea = new List<BattleNode>();
        for (int i = -maxRange; i <= maxRange; i++)
        {
            for (int j = -maxRange; j <= maxRange; j++)
            {
                int x = (int)playerCharcater.Character.GridArrayPosition.x + i;
                int z = (int)playerCharcater.Character.GridArrayPosition.y + j;
                if ((x >= 0 && x < Width) && (z >= 0 && z < Depth))
                {
                    //float d = Vector3.Distance(CustomGrid[(int)playerCharcater.Character.GridArrayPosition.x, (int)playerCharcater.Character.GridArrayPosition.y].CentrePosition, CustomGrid[x, z].CentrePosition);
                    if (CustomGrid[x, z].Walkable && CustomGrid[x, z].Vacant)
                    {
                        SelectedCharactersMoveableArea.Add(CustomGrid[x, z]);
                    }
                }
            }
        }
    }

    public bool CanMoveCharcaterOnGrid(BaseCharacter character, Vector3 target)
    {
        BattleNode node = GetNodeAtWorldPosition(target);
        return SelectedCharactersMoveableArea.Contains(node);

    }

    public void MoveCharacterOnGrid(BaseCharacter character, Vector3 target)
    {
        CustomGrid[(int)character.GridArrayPosition.x, (int)character.GridArrayPosition.y].Vacant = true;

        Vector2 arrraypos = GetArraryPositionAtWorldPosition(target);
        BattleCharacterHandler.MoveCharacterTo(character, CustomGrid[(int)arrraypos.x, (int)arrraypos.y].CentrePosition, arrraypos);
        CustomGrid[(int)arrraypos.x, (int)arrraypos.y].Vacant = false;
    }

    public BattleNode GetNodeAtWorldPosition(Vector3 worldposition)
    {
        Vector2 arrraypos = GetArraryPositionAtWorldPosition(worldposition);
        return CustomGrid[(int)arrraypos.x, (int)arrraypos.y];
    }


    private Vector2 GetArraryPositionAtWorldPosition(Vector3 worldposition)
    {
        Vector2 minMPosition = new Vector2(this.transform.position.x - ((Width / 2) * GridSize), this.transform.position.z - ((Depth / 2) * GridSize));
        Vector2 maxPosition = new Vector2(minMPosition.x + (Width * GridSize), minMPosition.y + (Depth * GridSize));

        float xPercent = (worldposition.x - minMPosition.x) / (maxPosition.x - minMPosition.x);
        float zPercent = (worldposition.z - minMPosition.y) / (maxPosition.y - minMPosition.y);

        int x = Mathf.Clamp(Mathf.RoundToInt(xPercent * Width), 0, Width - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt(zPercent * Depth), 0, Depth - 1);

        return new Vector2(x, y);
    }

    public void UpdateHoverGridPosition(Vector3 hoverPosition)
    {
        Vector2 arrraypos = GetArraryPositionAtWorldPosition(hoverPosition);
        HoveredGrid = CustomGrid[(int)arrraypos.x, (int)arrraypos.y];
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

    public void AlignCharacter(BaseCharacter baseCharacter)
    {
        Vector2 arrarypos = GetCharactersClosestValidArrayPosition(baseCharacter.transform.position);
        CustomGrid[(int)arrarypos.x, (int)arrarypos.y].Vacant = false;
        BattleCharacterHandler.MoveCharacterTo(baseCharacter, CustomGrid[(int)arrarypos.x, (int)arrarypos.y].CentrePosition, arrarypos);
    }

    private void AlignObjects()
    {
        foreach (var item in Setup.TurnOrders)
        {
            AlignCharacter(item.Character);
        }
    }
}
