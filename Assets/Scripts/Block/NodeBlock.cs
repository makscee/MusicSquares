using UnityEngine;

public class NodeBlock : Block
{
    public NodeBlock(FieldMatrix matrix, int x, int y) : base(matrix, x, y)
    {
        visuals.modelBase = BlockModelBase.Create(this,
            new [] {Prefabs.Instance.nodePipeModel, Prefabs.Instance.nodeEndModel});
        visuals.modelBase.pickModel = () => matrix.binds.HaveBinds(x, y) ? 0 : 1;
    }

    public NodeBlock(FieldCell cell) : this(cell.matrix, cell.x, cell.y)
    {
    }
}