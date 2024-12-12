using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private MeshRenderer render;
    private float offset;
    public float speed;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset, 0);
    }

    // 배경 스크롤 속도를 변경하는 메서드
    public void SetSpeedMultiplier(float multiplier)
    {
        speed = multiplier;
    }
}
