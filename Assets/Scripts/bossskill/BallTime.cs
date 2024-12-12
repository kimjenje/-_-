using UnityEngine;
using System.Collections;

public class BallTime : MonoBehaviour
{
    public Vector2 _point;
    public Transform[] _enemys;
    private SpriteRenderer _spriteRenderer;

    float _startTime;
    bool _hasFired = false;

    private void Start()
    {
        _startTime = Time.time + Random.Range(2.0f, 2.0f);
        _spriteRenderer = GameObject.Find("redbox3").GetComponent<SpriteRenderer>();
        StartCoroutine(Redbox());
    }

    IEnumerator Redbox()
    {
        for(int i = 0; i < 5; i++)
        {
            _spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = new Color(1, 0, 0, 0);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Update()
    {
        if (!_hasFired && Time.time >= _startTime)
        {
            Fire();
            _hasFired = true;
        }
    }

    void Fire()
    {
        Vector2 pos = new Vector2(8.0f, 4.0f);
        Instantiate(_enemys[Random.Range(0, _enemys.Length)], pos, Quaternion.identity);
    }
}
