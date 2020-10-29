using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FieldCell : MonoBehaviour
{
    public int x, y;
    public Block block;
    SpriteRenderer _sr;
    public FieldMatrix matrix;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void Destroy()
    {
        DestroyImmediate(gameObject);
    }
    void SetCoords(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.localPosition = new Vector3(x, y);
    }
    
    public static FieldCell Create(FieldMatrix matrix, int x, int y)
    {
        var go = Instantiate(Prefabs.Instance.fieldCell, matrix.transform);
        var fc = go.GetComponent<FieldCell>();
        fc.matrix = matrix;
        fc.SetCoords(x, y);
        return fc;
    }

    public void OnClick()
    {
        Animator.Interpolate(1f, _sr.color.a, 0.3f)
            .PassValue(v => _sr.color = _sr.color.ChangeAlpha(v));
        Clear();
        AddBlock(new NodeBlock(matrix, x, y));
    }

    public void AddBlock(Block b)
    {
        if (block != null) throw new Exception($"Block already exists on {x} {y}");
        block = b;
    }

    public void Clear()
    {
        if (block != null)
        {
            block.Destroy();
            block = null;
        }
        matrix.binds.ClearCellBinds(x, y);
    }
}