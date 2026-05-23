using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public LayerMask obstacleMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    private PathNode[,] grid;
    private float nodeDiameter;
    private int gridSizeX;
    private int gridSizeY;

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new PathNode[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, obstacleMask));
                grid[x, y] = new PathNode(walkable, worldPoint, x, y);
            }
        }
    }

    public PathNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null)
        {
            foreach (PathNode node in grid)
            {
                Gizmos.color = node.isWalkable ? Color.green : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
    public List<PathNode> GetNeighbors(PathNode node)
    {
        List<PathNode> neighbors = new List<PathNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    if (Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1)
                    {
                        if (!grid[node.gridX + x, node.gridY].isWalkable || !grid[node.gridX, node.gridY + y].isWalkable)
                        {
                            continue;
                        }
                    }
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }
}