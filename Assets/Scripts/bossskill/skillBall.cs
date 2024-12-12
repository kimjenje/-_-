using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillBall : MonoBehaviour
{
    public float _speed;
    public Rigidbody2D _rd;
    public Transform _myTF;

    private void Awake()
    {
        _rd = GetComponent<Rigidbody2D>();
        _myTF = GetComponent<Transform>();
    }

    void Start()
    {
        Vector2 diagonalDirection = new Vector2(2.5f, 0.9f).normalized;
        _rd.velocity = diagonalDirection * _speed;
    }

    void Update()
    {
        // 공의 위치를 확인하여 x 값이 -12를 넘어가면 삭제
        if (_myTF.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }
}
