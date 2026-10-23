
using UnityEngine;
using UnityEngine.UI;

public class CollectibleUI : MonoBehaviour
{
    [SerializeField] private GameObject collectiblePanel;
    [SerializeField] private Image storyImage;
    [SerializeField] private Sprite placeholderImage;
    [SerializeField] private Animator popupAnimator;

    public void ShowStory(Sprite image)
    {
        storyImage.sprite = image != null ? image : placeholderImage;

        collectiblePanel.SetActive(true);

        popupAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        popupAnimator.Play("CollectiblePop", 0, 0f);

        Time.timeScale = 0f; // stop time
    }

    public void CloseStory()
    {
        collectiblePanel.SetActive(false);
        Time.timeScale = 1f; // resume time
    }
}
