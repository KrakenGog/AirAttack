using System.Collections.Generic;
using UnityEngine;

public class PoolsBase : MonoBehaviour
{
    [SerializeField] private List<ListHolder<Destructible>> _destructibles;

    private List<Pool<VFXRecycler>> _destructiblePools;

    public void Initialize()
    {
        foreach (var items in _destructibles)
        {
            /*Pool<VFXRecycler> pool = new Pool<VFXRecycler>(
                items.list[0].GetVfxInstance,
                (vfx) => vfx.Recycle(),
                (vfx) => vfx.gameObject.SetActive(false),
                Mathf.RoundToInt((float)items.list.Count / 2)
            );*/

            foreach (var item in items.list)
            {
                //item.Initialize(pool);
            }
        }
    }
}

[System.Serializable]
public struct ListHolder<T>
{
    public List<T> list;
}
