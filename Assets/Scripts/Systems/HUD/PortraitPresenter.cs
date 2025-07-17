using UnityEngine;
using UnityEngine.UI;

public class PortraitPresenter : MonoBehaviour
{
    private Image m_Portrait;

    private void Awake()
    {
        
        m_Portrait = GetComponent<Image>();
        if (m_Portrait == null)
        {
            Debug.LogError("PortraitPresenter requires an Image component.");
        }
        
    }

    private void UpdateView(Sprite sprite)
    {
        if (m_Portrait != null && sprite != null)
        {
            m_Portrait.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Portrait or sprite is null. Cannot update portrait view.");
        }
    }

    public void UpdatePortrait(Sprite portrait)
    {
        if (portrait != null)
        {
            UpdateView(portrait);
        }
        else
        {
            Debug.LogError("Portrait image is null. Cannot update portrait.");
        }
    }
}