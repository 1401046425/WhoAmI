using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthographic : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;
    // Start is called before the first frame update
    public void Change2D()
    {
        m_Camera.orthographic = true;
    }

    public void Change3D()
    {
        m_Camera.orthographic = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
