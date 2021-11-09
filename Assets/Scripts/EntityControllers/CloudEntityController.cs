using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEntityController : MonoBehaviour
{
	private float speed = 50f;
	private float currentAngle = 0;
	private float radius = 17.5f;
	// Start is called before the first frame update
	void Start()
	{
		//speed = Random.Range(25f, 50f);
	}

	// Update is called once per frame
	void Update()
	{
		transform.rotation = Quaternion.Euler(0, -currentAngle, currentAngle * 2);
		transform.position = new Vector3(radius * Mathf.Cos(currentAngle * Mathf.PI / 180f), 40, radius * Mathf.Sin(currentAngle * Mathf.PI / 180f));
		currentAngle += speed * Time.deltaTime;
	}
}
