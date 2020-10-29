using UnityEngine;

public class BlockLogic
{
    public readonly FieldMatrix matrix;
    public int x, y;
    public Vector2 WorldPosition => matrix.transform.position + new Vector3(x, y);

    public BlockLogic(FieldMatrix matrix, int x, int y)
    {
        this.matrix = matrix;
        this.x = x;
        this.y = y;
    }

    public void Destroy()
    {
        if (matrix.GetCell(x, y, out var cell) && cell.block != null) cell.Clear();
    }

    public void Refresh()
    {
        
    }
}