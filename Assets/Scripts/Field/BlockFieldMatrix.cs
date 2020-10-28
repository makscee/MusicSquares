using System;
using UnityEngine;

[ExecuteInEditMode]
public class BlockFieldMatrix : MonoBehaviour
{
    [Range(1, 10)] public int sizeX;
    [Range(1, 10)] public int sizeY;
    FieldCell[,] _cells;

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
    }
}