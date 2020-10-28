using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldCell : MonoBehaviour, IPointerClickHandler
{
    public int x, y;
    public Block block;
    SpriteRenderer _sr;
    BlockFieldMatrix _matrix;

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
    
    public static FieldCell Create(BlockFieldMatrix matrix, int x, int y)
    {
        var go = Instantiate(Prefabs.Instance.fieldCell, matrix.transform);
        var fc = go.GetComponent<FieldCell>();
        fc._matrix = matrix;
        fc.SetCoords(x, y);
        return fc;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Animator.Interpolate(1f, _sr.color.a, 0.3f)
            .PassValue(v => _sr.color = _sr.color.ChangeAlpha(v));
        block = new NodeBlock(_matrix, x, y);
    }
}