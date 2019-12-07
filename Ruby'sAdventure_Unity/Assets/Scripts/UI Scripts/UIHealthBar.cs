using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class take care of scaling the UI image that is used as a health bar, based on the ratio sent to it.
/// It is a singleton so it can be called from anywhere (e.g. PlayerController SetHealth)
/// </summary>
public class UIHealthBar : MonoBehaviour
{
    // Allow changes to health bar in any script via UIHealthBar.instance
    public static UIHealthBar instance
    {
        get;
        private set;
    }

    public Image mask;
    float originalSize;

    // Store a static instance of "this" when the object is created
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        originalSize = mask.rectTransform.rect.width;

    }

    // Update is called once per frame
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
