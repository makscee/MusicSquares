using UnityEngine;

public class BlockVisuals
{
    public BlockModelBase modelBase;

    public BlockVisuals()
    {
        
    }

    public void Destroy()
    {
        Object.Destroy(modelBase.gameObject);
    }

    public void Refresh()
    {
        if (modelBase != null)
        {
            modelBase.isDirty = true;
        }
    }
}