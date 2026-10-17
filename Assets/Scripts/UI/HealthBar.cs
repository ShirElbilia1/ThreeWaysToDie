using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text hpText;

    public void UpdateHealth(int currentHp, int maxHp)
    {
        slider.maxValue = maxHp;
        slider.value = Mathf.Clamp(currentHp, 0, maxHp); // keep value between 0 to max

        hpText.text = $"{Mathf.Clamp(currentHp, 0, maxHp)} / {maxHp}";
    }
}