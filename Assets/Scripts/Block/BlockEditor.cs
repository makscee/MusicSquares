using UnityEngine;

public static class BlockEditor
{
    static FieldMatrix _matrix;
    static int _lastX, _lastY;
    static bool _blockDragged;
    public static void OnBlockDragStart(Block block)
    {
        _lastX = block.logic.x;
        _lastY = block.logic.y;
        _blockDragged = true;
        _matrix = block.logic.matrix;
    }

    public static void OnBlockDrag(int x, int y)
    {
        if (!_blockDragged || _lastX == x && _lastY == y) return;
        CreatePath(_lastX, _lastY, x, y);
        _lastX = x;
        _lastY = y;
    }

    public static void OnBlockDragEnd()
    {
        _blockDragged = false;
    }

    static void DragFromTo(int fromX, int fromY, int toX, int toY)
    {
        if (!_matrix.GetBlock(fromX, fromY, out var fromBlock)) return;
        if (_matrix.GetBlock(toX, toY, out var toBlock))
        {
            if (_matrix.binds.IsBound(toX, toY, fromX, fromY) && !_matrix.binds.HaveBinds(fromX, fromY))
            {
                fromBlock.Destroy();
                return;
            }
            _matrix.binds.AddBind(fromX, fromY, toX, toY);
            return;
        }

        if (_matrix.GetCell(toX, toY, out var cell))
        {
            cell.AddBlock(new NodeBlock(cell));
            _matrix.binds.AddBind(fromX, fromY, toX, toY);
        }
    }

    static void CreatePath(int fromX, int fromY, int x, int y)
    {
        var xTotal = Mathf.Abs(x - fromX);
        var yTotal = Mathf.Abs(y - fromY);
        var xDelta = xTotal > 0 ? (x - fromX) / xTotal : 0;
        var yDelta = yTotal > 0 ? (y - fromY) / yTotal : 0;
        int xCur = fromX, yCur = fromY;
        var xPerc = xTotal != 0 ? Mathf.Abs((float) xCur - fromX) / xTotal : 1f;
        var yPerc = yTotal != 0 ? Mathf.Abs((float) yCur - fromY) / yTotal : 1f;
        for (var i = 0; i < xTotal + yTotal; i++)
        {
            int xFrom = xCur, yFrom = yCur;
            if (xPerc > yPerc)
            {
                yCur += yDelta;
                yPerc = Mathf.Abs((float) yCur - fromY) / yTotal;
            }
            else
            {
                xCur += xDelta;
                xPerc = Mathf.Abs((float) xCur - fromX) / xTotal;
            }
            DragFromTo(xFrom, yFrom, xCur, yCur);
        }
    }
}