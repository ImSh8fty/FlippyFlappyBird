using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    private bool isUpsideDown = false;
    private bool gravityFlipped = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction = Vector3.up * strength * (gravityFlipped ? -1 : 1);
        }

        direction.y += gravity * Time.deltaTime * (gravityFlipped ? -1 : 1);
        transform.position += direction * Time.deltaTime;
    }

    public void SetUpsideDown(bool value)
    {
        isUpsideDown = value;
        transform.localScale = new Vector3(1f, isUpsideDown ? -1f : 1f, 1f);
    }

    public void SetGravityFlipped(bool value)
    {
        gravityFlipped = value;
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length) spriteIndex = 0;
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.CompareTag("Score"))
        {
            FindObjectOfType<GameManager>().IncreasesScore();
        }
    }
}
