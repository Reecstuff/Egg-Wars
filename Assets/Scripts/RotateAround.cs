using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField]
    float speed = 3;

    public bool Rotate = false;

    private void FixedUpdate()
    {
        if(Rotate)
            transform.Rotate(Vector3.up * speed, Space.Self);
    }
}
