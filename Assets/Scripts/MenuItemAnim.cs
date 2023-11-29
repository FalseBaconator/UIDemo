using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemAnim : MonoBehaviour
{
    public float duration;
    public bool rotation;
    public bool scale;
    public bool fall;
    public float fallHeight;

    private void OnEnable()
    {
        if (rotation)
        {
            transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360);
        }
        if (scale)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, duration);
        }
        if (fall)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y + fallHeight, transform.position.z);
            transform.DOMove(pos, duration);
        }
    }

}
