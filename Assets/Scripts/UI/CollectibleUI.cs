using UnityEngine;
using UnityEngine.UI;

public class CollectibleUI : MonoBehaviour
{
    [SerializeField] private GameObject collectiblePanel;
    [SerializeField] private Image storyImage;
    [SerializeField] private Sprite placeholderImage;

    public void ShowStory(Sprite image)
    {
        storyImage.sprite = image != null ? image : placeholderImage;

        collectiblePanel.SetActive(true);
        Time.timeScale = 0f; // stop time
    }

    public void CloseStory()
    {
        collectiblePanel.SetActive(false);
        Time.timeScale = 1f; // resume time
    }
}
