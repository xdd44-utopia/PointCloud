using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEntityController : MonoBehaviour
{
    private const float speed = 10f;
    private float currentAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        currentAngle += speed * Time.deltaTime;
    }
}
