using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed; // 배경 스크롤 속도
    private new Renderer renderer;
    private float currentOffset;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        currentOffset = 0f;
    }

    private void Update()
    {
        currentOffset += Time.deltaTime * speed;
        float normalizedOffset = currentOffset % 1.0f; // Get the fractional part
        renderer.material.mainTextureOffset = new Vector2(normalizedOffset, 0);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}