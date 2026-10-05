using UnityEngine;
using UnityEngine.UI;

// Zet dit op de rechter aansluiting (Image, Raycast Target AAN).
public class WireEnd : MonoBehaviour
{
    public Color wireColor = Color.red;
    public bool Connected { get; set; }

    void Start()
    {
        GetComponent<RawImage>().color = wireColor;
    }
}
