using UnityEngine;

public class Block
{
    public BlockLogic logic;
    public BlockVisuals visuals;

    protected Block(BlockFieldMatrix matrix, int x, int y)
    {
        logic = new BlockLogic(matrix, x, y);
        visuals = new BlockVisuals();
        
    }
}