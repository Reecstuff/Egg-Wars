using System.Collections;
using UnityEditor;
using UnityEngine;

public class ThisFaceCamera : MonoBehaviour
{

    void LateUpdate()
    {

        if(gameObject.activeInHierarchy)
        {
            if(Camera.main)
                transform.LookAt(Camera.main.transform);

        }
        //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
