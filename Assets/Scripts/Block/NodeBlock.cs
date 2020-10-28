public class NodeBlock : Block
{
    public NodeBlock(BlockFieldMatrix matrix, int x, int y) : base(matrix, x, y)
    {
        visuals.modelBase = BlockModelBase.Create(this,
            new [] {Prefabs.Instance.nodePipeModel, Prefabs.Instance.nodeEndModel});
    }
}