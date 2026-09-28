using UnityEngine;
using UnityEngine.UI;

public class ShuffleColor : MonoBehaviour
{
    [SerializeField] private Sprite[] colors;
    [SerializeField] private Image Image;
    int PrevColorIndex = -1;
    int newColorIndex = 0;

    private void Start()
    {
        UpdateColor();
    }
    public void UpdateColor()
    {
        do
        {
            newColorIndex = Random.Range(0, colors.Length);
        } while (PrevColorIndex == newColorIndex);

        PrevColorIndex = newColorIndex;

        Image.sprite = colors[PrevColorIndex];
    }
}
