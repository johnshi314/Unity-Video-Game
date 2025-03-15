using UnityEngine;
using System.Collections.Generic;

public class CraftingGrid : MonoBehaviour
{
    public GameObject cellPrefab; // Prefab for grid cells
    public Transform gridParent; // Parent object for grid cells
    public LineRenderer lineRenderer; // Line Renderer for visualizing the line
    private GameObject[,] grid; // 2D array of grid cells
    public float gridSpacing = 2.0f; // Distance between grid points
    private List<Vector2Int> selectedCells = new List<Vector2Int>(); // Track selected cells

    // Had to make this one a list so that the player can start on any point as long as they make a circle
    private List<Vector2Int> outerPerimeterPattern = new List<Vector2Int> {
        new Vector2Int(0, 0), // Top-left corner
        new Vector2Int(0, 1), // Top-middle
        new Vector2Int(0, 2), // Top-right corner
        new Vector2Int(1, 2), // Middle-right
        new Vector2Int(2, 2), // Bottom-right corner
        new Vector2Int(2, 1), // Bottom-middle
        new Vector2Int(2, 0), // Bottom-left corner
        new Vector2Int(1, 0)  // Middle-left
    };

    void Start()
    {
        // Initialize the grid
        grid = new GameObject[3, 3];
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                // Instantiate cell at specified position
                GameObject cell = Instantiate(cellPrefab, gridParent);

                float xPos = col * gridSpacing;
                float yPos = row * gridSpacing;

                // Set the position of the cell
                cell.transform.position = new Vector2(xPos, yPos);

                grid[row, col] = cell;
            }
        }

        // Initialize line renderer
        lineRenderer.positionCount = 0; // Start with no points
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        // Handle mouse dragging
        if (Input.GetMouseButtonDown(0))
        {
            // Reset previous selections and line renderer
            selectedCells.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            // Get the mouse position and convert to world position
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = GetGridPosition(worldPos);

            if (IsWithinGrid(gridPos) && !selectedCells.Contains(gridPos))
            {
                // Add grid position to selected cells
                selectedCells.Add(gridPos);
                // Update the line renderer
                UpdateLineRenderer();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Check patterns and trigger outcomes
            CheckPatternsAndTriggerOutcomes();
        }
    }

    Vector2Int GetGridPosition(Vector2 worldPos)
    {
        // Convert world position to grid coordinates
        int col = Mathf.RoundToInt(worldPos.x);
        int row = Mathf.RoundToInt(worldPos.y);
        return new Vector2Int(col, row);
    }

    bool IsWithinGrid(Vector2Int pos)
    {
        // Check if position is within the bounds of the grid
        return pos.x >= 0 && pos.x < 3 && pos.y >= 0 && pos.y < 3;
    }

    void UpdateLineRenderer()
    {
        // Update the line renderer with the selected cells
        lineRenderer.positionCount = selectedCells.Count;
        for (int i = 0; i < selectedCells.Count; i++)
        {
            Vector2Int cellPos = selectedCells[i];
            // Calculate the position with spacing
            // Multiply the cell coordinates by gridSpacing to account for the increased spacing
            float xPos = cellPos.x * gridSpacing;
            float yPos = cellPos.y * gridSpacing;

            // Set the line renderer position at the calculated (x, y) position
            lineRenderer.SetPosition(i, new Vector3(xPos, yPos, -9));
        }
    }

    void CheckPatternsAndTriggerOutcomes()
    {
        // Define patterns and check them against selected cells
        // If a pattern matches, trigger the desired outcome
        // Example: Check for a diagonal pattern
        // Define pattern
        // Define patterns
        Vector2Int[] diagonalPattern = {
            new Vector2Int(0, 0),
            new Vector2Int(1, 1),
            new Vector2Int(2, 2)
        };

        Vector2Int[] outerGridsPattern = {
            new Vector2Int(0, 0), // Top-left corner
            new Vector2Int(0, 1), // Top-middle
            new Vector2Int(0, 2), // Top-right corner
            new Vector2Int(1, 2), // Middle-right
            new Vector2Int(2, 2), // Bottom-right corner
            new Vector2Int(2, 1), // Bottom-middle
            new Vector2Int(2, 0), // Bottom-left corner
            new Vector2Int(1, 0)  // Middle-left
        };

        Vector2Int[] verticalPattern = {
            new Vector2Int(1, 0),
            new Vector2Int(1, 1),
            new Vector2Int(1, 2)
        };

        Vector2Int[] horizontalPattern = {
            new Vector2Int(0, 1),
            new Vector2Int(1, 1),
            new Vector2Int(2, 1)
        };

        // Check if any pattern matches
        if (MatchPattern(diagonalPattern))
        {
            TriggerOutcome();
            Debug.Log("Diagonal Pattern Received");
        }
        else if (MatchOuterPerimeterPattern())
        {
            TriggerOutcome();
            Debug.Log("Circle Pattern Received");
        }
        else if (MatchPattern(verticalPattern))
        {
            TriggerOutcome();
            Debug.Log("Vertical Pattern Received");
        }
        else if (MatchPattern(horizontalPattern))
        {
            TriggerOutcome();
            Debug.Log("Horizontal Pattern Received");
        }
    }

    bool MatchPattern(Vector2Int[] pattern)
    {
        // Check if all positions in the pattern are in the selectedCells list
        foreach (Vector2Int pos in pattern)
        {
            if (!selectedCells.Contains(pos))
            {
                return false;
            }
        }
        return true;
    }

    bool MatchOuterPerimeterPattern()
    {
        int patternLength = outerPerimeterPattern.Count;
        for (int start = 0; start < patternLength; start++)
        {
            bool patternMatches = true;

            // Check the pattern from the starting point
            for (int i = 0; i < patternLength; i++)
            {
                // Calculate the current index in the pattern
                int currentIndex = (start + i) % patternLength;

                // Calculate the expected position in the grid
                Vector2Int expectedPosition = outerPerimeterPattern[currentIndex];

                // Check if the expected position is in the selected cells
                if (!selectedCells.Contains(expectedPosition))
                {
                    patternMatches = false;
                    break;
                }
            }

            // If the pattern matches, return true
            if (patternMatches)
            {
                return true;
            }
        }

        // If no match is found, return false
        return false;
    }


    void TriggerOutcome()
    {
        // Handle the outcome (e.g., crafting item, displaying message, playing sound)
        Debug.Log("Pattern matched! Outcome triggered.");
        // Perform the desired action here
    }
}
