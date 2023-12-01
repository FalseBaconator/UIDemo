using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCamera : MonoBehaviour
{
    Camera cam;

    public bool bob;
    public float bobHeight;
    public float bobDuration;

    private void Awake()
    {
        cam = Camera.main;
        if (bob)
        {
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + bobHeight, transform.position.z), bobDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            //transform.DOJump(transform.position, bobHeight, 1, bobDuration).SetLoops(-1, LoopType.Restart);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
