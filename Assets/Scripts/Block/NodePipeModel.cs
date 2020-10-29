using System;
using UnityEngine;

public class NodePipeModel : MonoBehaviour
{
    [SerializeField] GameObject[] bindVisuals;
    BlockModelBase _bmb;

    void Awake()
    {
        _bmb = GetComponentInParent<BlockModelBase>();
        _bmb.block.onBind += RefreshBindVisuals;
        _bmb.block.onUnbind += RefreshBindVisuals;
    }

    void RefreshBindVisuals()
    {
        var mask = _bmb.block.logic.matrix.binds.GetBindMask(_bmb.block.logic.x, _bmb.block.logic.y);
        Debug.Log($"{mask}");
        for (var i = 0; i < 4; i++)
            bindVisuals[i].SetActive((mask & (1 << i)) > 0);
    }
}