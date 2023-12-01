using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetUpdate(true);
        }
        if (scale)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, duration).SetEase(Ease.InOutSine).SetUpdate(true);
        }
        if (fall)
        {
            //Vector3 pos = transform.position;   //Get where I want it to end
            //transform.position = new Vector3(transform.position.x, transform.position.y + fallHeight, transform.position.z);    //Go where I want it to start
            //transform.DOMove(pos, duration);    //Make it move

            RectTransform rect = GetComponent<RectTransform>();
            Vector2 pos = rect.anchoredPosition;
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + fallHeight);
            DOTween.To(() => rect.anchoredPosition, x => rect.anchoredPosition = x, pos, duration).SetEase(Ease.OutBounce).SetUpdate(true);

        }
    }

}
