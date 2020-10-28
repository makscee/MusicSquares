using UnityEngine;

public class BlockModelObject : MonoBehaviour
{
    public void Show(bool value)
    {
        gameObject.SetActive(value);
    }
}