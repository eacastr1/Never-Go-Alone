using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    public Image HeartPrefab;
    public Sprite FullHeartSprite;
    public Sprite HalfHeartSprite;
    public Sprite EmptyHeartSprite;

    private List<Image> hearts = new List<Image>();

    public void InitializeHearts(float maxHearts)
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }

        hearts.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            Image newHeart = Instantiate(HeartPrefab, transform);
            newHeart.sprite = FullHeartSprite;
            // newHeart.color = Color.red;
            hearts.Add(newHeart);
        }
    }

    public void UpdateHearts(float currentHealth)
    {
        float roundedHealth = RoundToNearestHalf(currentHealth);

        for (int i = 0; i < hearts.Count; i++)
        {
            float heartValue = i + 1;

            if (heartValue <= roundedHealth)
            {
                hearts[i].sprite = FullHeartSprite;
            }
            else if (heartValue - 0.5f == roundedHealth)
            {
                hearts[i].sprite = HalfHeartSprite;
            }
            else
            {
                hearts[i].sprite = EmptyHeartSprite;
            }
        }
    }

    public float RoundToNearestHalf(float value)
    {
        return Mathf.Round(value * 2f) / 2f;
    }
}