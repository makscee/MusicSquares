using System;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class FieldMatrix : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Range(1, 10)] public int sizeX;
    [Range(1, 10)] public int sizeY;
    public FieldBinds binds;
    FieldCell[,] _cells;

    [SerializeField] BoxCollider2D box;

    public bool GetBlock(int x, int y, out Block block)
    {
        block = null;
        if (!IndCheck(x, y)) return false;
        block = _cells[x, y]?.block;
        return block != null;
    }

    public bool GetCell(int x, int y, out FieldCell cell)
    {
        cell = null;
        if (!IndCheck(x, y)) return false;
        cell = _cells[x, y];
        return true;
    }

    public bool IndCheck(int x, int y)
    {
        return x >= 0 && x < _cells.GetLength(0) && y >= 0 && y < _cells.GetLength(1) && _cells[x, y] != null;
    }

    void OnValidate()
    {
        if (_cells == null || sizeX != _cells.GetLength(0) || sizeY != _cells.GetLength(1))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += CreateField;
#else 
            CreateField();
#endif
        }
    }

    void OnEnable()
    {
        CreateField();
    }

    void CreateField()
    {
        if (this == null) return;
        foreach (var cell in GetComponentsInChildren<FieldCell>())
                cell.Destroy();
    
        _cells = new FieldCell[sizeX, sizeY];
        for (var x = 0; x < sizeX; x++)
        {
            for (var y = 0; y < sizeY; y++)
            {
                _cells[x, y] = FieldCell.Create(this, x, y);
            }
        }
        binds = new FieldBinds(this);
        box.size = new Vector2(sizeX, sizeY);
        box.offset = (box.size - Vector2.one) / 2;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_dragging) return;
        var pos = SharedObjects.mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _cells[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)].OnClick();
    }

    bool _dragging;
    public void OnDrag(PointerEventData eventData)
    {
        GetCoordsFromScreen(this, out var x, out var y);
        BlockEditor.OnBlockDrag(x, y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
        GetCoordsFromScreen(this, out var x, out var y);
        if (GetBlock(x, y, out var block)) 
        {
            BlockEditor.OnBlockDragStart(block);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;
        BlockEditor.OnBlockDragEnd();
    }

    static void GetCoordsFromScreen(FieldMatrix matrix, out int x, out int y)
    {
        var pos = SharedObjects.mainCamera.ScreenToWorldPoint(Input.mousePosition) - matrix.transform.position;
        x = Mathf.RoundToInt(pos.x);
        y = Mathf.RoundToInt(pos.y);
    }
}