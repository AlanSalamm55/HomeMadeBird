using System;
using UnityEngine;

public class ScrollableImage : MonoBehaviour
{
    [SerializeField] private float endOfSprite = 17.23f;
    [SerializeField] private float speed = 2f;

    private void Start()
    {
        ResetPosition();
    }

    void Update()
    {
        float moveX = speed * Time.deltaTime * -1;

        transform.Translate(moveX, 0, 0);

        // Reset position if it goes beyond the bounds
        if (Mathf.Abs(transform.position.x) > endOfSprite)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = Vector3.zero;
    }
}