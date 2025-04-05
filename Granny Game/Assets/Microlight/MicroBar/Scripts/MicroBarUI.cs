using UnityEngine;
using UnityEngine.UI;

namespace Microlight.MicroBar
{
    public class MicroBarUI : MonoBehaviour
    {
        [Header("MicroBar Settings")]
        [SerializeField] private Image fillImage;
        [SerializeField] private Color fullColor = Color.green;
        [SerializeField] private Color emptyColor = Color.red;

        private float maxValue = 100f;

        public void Initialize()
        {
            UpdateBar(maxValue); // Full bar on init
        }

        public void SetNewMaxHP(float newMax)
        {
            maxValue = newMax;
        }

        public void UpdateBar(float current)
        {
            float fillAmount = Mathf.Clamp01(current / maxValue);
            fillImage.fillAmount = fillAmount;
            fillImage.color = Color.Lerp(emptyColor, fullColor, fillAmount);
        }
    }
}
