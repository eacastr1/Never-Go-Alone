public class Eagle : Entity
{
    protected override void Awake()
    {
        base.Awake();
        // Additional initialization for Eagle can be added here
    }

    protected void FixedUpdate()
    {
        Rigidbody.linearVelocity = Player.Instance.PlayerController.AltInput * 4.5f;
        HandleFlip();
    }

    private void HandleFlip()
    {
        if (Player.Instance.PlayerController.AltInput.x > 0)
        {
            SpriteRenderer.flipX = true;
        }
        else if (Player.Instance.PlayerController.AltInput.x < 0)
        {
            SpriteRenderer.flipX = false;
        }
    }

}