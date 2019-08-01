using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public float _RotationSpeed;
    Transform _Trans;
    // Start is called before the first frame update
    void Start()
    {
        _Trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _Trans.Rotate(Vector3.down * _RotationSpeed, Space.World);
    }
}
