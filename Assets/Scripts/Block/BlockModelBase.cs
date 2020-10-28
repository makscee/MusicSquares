using System;
using UnityEngine;

public class BlockModelBase : MonoBehaviour
{
    public Block block;
    BlockModelObject[] _models;

    public bool isDirty = true;
    int _currentModel = -1;
    public Func<int> pickModel = () => 0;

    void Update()
    {
        if (isDirty) Refresh();
    }

    void Refresh()
    {
        var modelInd = pickModel();
        if (modelInd != _currentModel) ChangeModel(modelInd);
        transform.position = block.logic.WorldPosition;
    }

    void ChangeModel(int modelInd)
    {
        if (_currentModel > 0 && _currentModel < _models.Length)
            _models[_currentModel]?.Show(false);
        _models[modelInd].Show(true);
        _currentModel = modelInd;
    }

    public static BlockModelBase Create(Block block, GameObject[] modelPrefabs)
    {
        var go = new GameObject($"{block.GetType()} visual model");
        var bmb = go.AddComponent<BlockModelBase>();
        bmb.block = block;
        bmb._models = new BlockModelObject[modelPrefabs.Length];
        for (var i = 0; i < modelPrefabs.Length; i++)
        {
            bmb._models[i] = Instantiate(modelPrefabs[i], bmb.transform).GetComponent<BlockModelObject>();
            bmb._models[i].Show(false);
        }

        return bmb;
    }
}