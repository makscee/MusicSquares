using UnityEngine;

public class BlockLogic
{
    public BlockFieldMatrix matrix;
    public int x, y;
    public Vector2 WorldPosition => matrix.transform.position + new Vector3(x, y);

    public BlockLogic(BlockFieldMatrix matrix, int x, int y)
    {
        this.matrix = matrix;
        this.x = x;
        this.y = y;
    }
}