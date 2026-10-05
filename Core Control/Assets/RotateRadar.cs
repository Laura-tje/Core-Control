using UnityEngine;

public class RotateRadar : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;


    void Update()
    {
        this.gameObject.transform.Rotate(0, 0, rotationSpeed *Time.deltaTime);
    }
}
