using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSergii : MonoBehaviour
{
    public Transform CenterPoint;
    [SerializeField]
    private int _Altitude;
    public float Speed;
    public int Radius;
    private float _angle = Mathf.PI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _angle += Speed * Time.deltaTime;
        transform.position = new Vector3(CenterPoint.position.x + Mathf.Sin(_angle) * Radius, _Altitude, CenterPoint.position.z + Mathf.Cos(_angle) * Radius);
        transform.LookAt(CenterPoint);
    }
}
