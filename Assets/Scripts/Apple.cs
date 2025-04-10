using UnityEngine;

public class Apple : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SnakeController snake = other.GetComponent<SnakeController>();
            if (snake != null)
            {
                snake.Grow();
                FindObjectOfType<AppleSpawner>().SpawnApple();
                Destroy(gameObject);
            }
        }
    }
}
