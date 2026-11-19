using UnityEngine;
using UnityEngine.UI;

public class HopeSystem : MonoBehaviour
{
    public Slider hopeBar;

    public float maxHope = 100f;
    public float currentHope = 100f;

    void Start()
    {
        currentHope = maxHope;

        hopeBar.minValue = 0;
        hopeBar.maxValue = maxHope;

        UpdateHopeBar();
    }

    public void LoseHope(float amount)
    {
        currentHope -= amount;
        currentHope = Mathf.Clamp(
            currentHope, 0, maxHope
        );

        UpdateHopeBar();

        if (currentHope <= 0)
        {
            Debug.Log("Shiori has run out of Hope!");
        }
    }

    public void GainHope(float amount)
    {
        currentHope += amount;
        currentHope = Mathf.Clamp(
            currentHope, 0, maxHope
        );

        UpdateHopeBar();
    }

    void UpdateHopeBar()
    {
        if (hopeBar != null)
        {
            hopeBar.value = currentHope;
        }
    }
}
