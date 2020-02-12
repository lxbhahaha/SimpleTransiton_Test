using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    public Vector3 dir;
    public float multi;

    void Update()
    {
        transform.Rotate(dir * multi * Time.deltaTime, Space.World);
    }
}
