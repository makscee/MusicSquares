using System;
using UnityEngine;

public class Block
{
    public BlockLogic logic;
    public BlockVisuals visuals;
    public Action onBind, onUnbind;
    bool _destroyed;

    protected Block(FieldMatrix matrix, int x, int y)
    {
        logic = new BlockLogic(matrix, x, y);
        visuals = new BlockVisuals();
        onBind += Refresh;
        onUnbind += Refresh;
    }
    
    protected Block(FieldCell cell) : this(cell.matrix, cell.x, cell.y)
    {
    }
    public void Destroy()
    {
        if (_destroyed) return;
        _destroyed = true;
        visuals.Destroy();
        logic.Destroy();
    }

    public void Refresh()
    {
        visuals.Refresh();
        logic.Refresh();
    }

    public override int GetHashCode()
    {
        if (_destroyed) return 0;
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (_destroyed && obj == null) return true;
        return base.Equals(obj);
    }
}