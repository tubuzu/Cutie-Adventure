using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int level;
    [SerializeField] Sprite activeStar;
    [SerializeField] Sprite disabledStar;
    public void LoadLevel()
    {
        if (level < 1) return;
        SceneManager.LoadScene("Level_" + level.ToString("00"));
        // SceneLoader.Instance.LoadScene("Level_" + level.ToString("00"));
    }
    public void SetStar(int stars)
    {
        if (stars < 0 || stars > 3) return;
        Transform starContainer = transform.Find("Stars");
        for (int i = 0; i < starContainer.childCount; i++)
        {
            if (i < stars) starContainer.GetChild(i).GetComponent<Image>().sprite = activeStar;
            else starContainer.GetChild(i).GetComponent<Image>().sprite = disabledStar;
        }
    }
}
