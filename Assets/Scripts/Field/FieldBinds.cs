using System.Collections.Generic;

public class FieldBinds
{
    readonly FieldMatrix _matrix;
    int[,] _binds;

    public FieldBinds(FieldMatrix matrix)
    {
        _matrix = matrix;
        _binds = new int[matrix.sizeX, matrix.sizeY];
    }

    public int GetBindMask(int x, int y)
    {
        return !IndCheck(x, y) ? 0 : _binds[x, y];
    }

    public void AddBind(int x1, int y1, int x2, int y2)
    {
        var dir = Dir(x1, y1, x2, y2);
        var revDir = (dir + 2) % 4;
        UpdateCell(x1, y1, _binds[x1, y1] | (1 << dir));
        UpdateCell(x2, y2, _binds[x2, y2] & ~(1 << revDir));
    }

    public void RemoveBind(int x1, int y1, int x2, int y2)
    {
        var dir = Dir(x1, y1, x2, y2);
        var revDir = (dir + 2) % 4;
        UpdateCell(x1, y1, _binds[x1, y1] & ~(1 << dir));
        UpdateCell(x2, y2, _binds[x2, y2] & ~(1 << revDir));
    }

    void UpdateCell(int x, int y, int value)
    {
        var oldValue = _binds[x, y];
        if (!_matrix.GetBlock(x, y, out var block)) return;
        _binds[x, y] = value;
        if (oldValue < value) block?.onBind();
        if (oldValue > value) block?.onUnbind();

    }

    public void ClearCellBinds(int x, int y)
    {
        for (var i = 0; i < 4; i++)
        {
            Utils.CoordsFromDir(i, out var x2, out var y2);
            x2 += x;
            y2 += y;
            if (!IndCheck(x2, y2)) continue;
            RemoveBind(x, y, x2, y2);
        }
    }

    public bool IsBound(int x1, int y1, int x2, int y2)
    {
        return (_binds[x1, y1] & (1 << Dir(x1, y1, x2, y2))) > 0;
    }

    public bool HaveBinds(int x, int y)
    {
        return _binds[x, y] > 0;
    }

    public List<Block> GetBoundBlocks(Block block)
    {
        var result = new List<Block>(4);
        var x = block.logic.x;
        var y = block.logic.y;
        for (var i = 0; i < 4; i++)
        {
            Utils.CoordsFromDir(i, out var x2, out var y2);
            x2 += x;
            y2 += y;
            if (IndCheck(x2, y2) &&
                IsBound(block.logic.x, block.logic.y, x2, y2) && _matrix.GetBlock(x2, y2, out var b)) 
                result.Add(b);
        }

        return result;
    }

    public bool IndCheck(int x, int y)
    {
        return _matrix.IndCheck(x, y);
    }

    static int Dir(int x1, int y1, int x2, int y2)
    {
        return Utils.DirFromCoords(x2 - x1, y2 - y1);
    }
}