using System.Runtime;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    private List<Transform> segments;
    public Transform segmentPrefab;
    public int initialSize = 3;
    // Start is called before the first frame update
    void Start()
    {
        segments = new List<Transform>();
        segments.Add(this.transform);

        for (int i = 1; i < initialSize; i++)
        {
            Transform newSegment = Instantiate(segmentPrefab);
            newSegment.position = segments[segments.Count - 1].position;
            segments.Add(newSegment);
            // RandomizePosition();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) {
            direction = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) {
            direction = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) {
            direction = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) {
            direction = Vector2.right;
        }
    }

void FixedUpdate()
{
    for (int i = segments.Count - 1; i > 0; i--)
    {
        segments[i].position = segments[i - 1].position;
    }

    this.transform.position = new Vector3(
        Mathf.Round(this.transform.position.x) + direction.x,
        Mathf.Round(this.transform.position.y) + direction.y,
        0
    );
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collided with {other.name}");
        if (other.name == "Wall") {
            Debug.Log("Oops i hit a wall");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (other.name == "SnakeSegment(Clone)") {
            Debug.Log("Oops i ate myself");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (other.name == "Food") {
            // Debug.Log("eating");
            Transform newSegment = Instantiate(segmentPrefab);
            newSegment.position = segments[segments.Count - 1].position;
            segments.Add(newSegment);
            // RandomizePosition();
        }

    }
}
