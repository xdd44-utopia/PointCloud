using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEntityController : MonoBehaviour
{
	private const float speed = 10f;
	private float currentAngle = 0;
	private bool isLeft = false;


	private float posChangeTimer = 0;
	private float posChangeDuration = 2.5f;
	// Start is called before the first frame update
	void Start()
	{
		if (transform.position.x < 0) {
			isLeft = true;
		}
		posChangeDuration = Random.Range(2.5f, 5f);
	}

	// Update is called once per frame
	void Update()
	{

		posChangeTimer -= Time.deltaTime;
		if (posChangeTimer < 0) {
			posChangeTimer = posChangeDuration;
			changePos();
		}

	}

	private void changePos(){
		float height = Random.Range(40, 80);
		float x = Random.Range(12.5f, 15);
		if (isLeft) {
			x = -x;
		}
		float z = Random.Range(-20, 20);
		transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
		transform.localPosition = new Vector3(x, height / 2, z);
	}
}
