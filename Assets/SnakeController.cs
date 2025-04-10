using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float stepSize = 1f;                // 一次移動的距離
    public float moveInterval = 0.2f;          // 每次移動的間隔時間
    public GameObject bodyPartPrefab;

    private Vector2 moveDirection = Vector2.right;
    private List<Transform> bodyParts = new List<Transform>();
    private float moveTimer = 0f;
    private bool isAlive = true;

    void Update()
    {
        if (!isAlive) return;

        HandleInput();

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            Move();
            moveTimer = 0f;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (moveDirection != Vector2.down)
                moveDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (moveDirection != Vector2.up)
                moveDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (moveDirection != Vector2.right)
                moveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (moveDirection != Vector2.left)
                moveDirection = Vector2.right;
        }
    }

    void Move()
    {
        Vector3 previousPosition = transform.position;
        transform.position += (Vector3)(moveDirection * stepSize);

        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 temp = bodyParts[i].position;
            bodyParts[i].position = previousPosition;
            previousPosition = temp;
        }

        CheckCollision();
    }

    public void Grow()
    {
        GameObject newPart = Instantiate(bodyPartPrefab, transform.position, Quaternion.identity);
        bodyParts.Add(newPart.transform);
    }

    void CheckCollision()
    {
        // 撞到自己
        foreach (Transform part in bodyParts)
        {
            if (part.position == transform.position)
            {
                Die();
                return;
            }
        }

        // 撞牆（假設地圖邊界是 ±10）
        if (Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 10)
        {
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        GameManager.Instance.GameOver(bodyParts.Count, Time.timeSinceLevelLoad);
    }

    public void ResetSnake()
    {
        transform.position = Vector3.zero;
        moveDirection = Vector2.right;
        foreach (Transform part in bodyParts)
        {
            Destroy(part.gameObject);
        }
        bodyParts.Clear();
        isAlive = true;
    }
}
