using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	private float duration;
	private float timer;
	private const float minDuration = 2f;
	private const float maxDuration = 3f;
	private const int width = 16;
	private const int height = 9;
	private const float lerpSpeed = 1.5f;
	private Vector3 targetPos;
	// Start is called before the first frame update
	void Start()
	{
		timer = 0;
		duration = Random.Range(minDuration, maxDuration);
		pickTargetPos();
	}

	// Update is called once per frame
	void Update()
	{

		transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);

		timer += Time.deltaTime;
		if (timer > duration) {
			timer = 0;
			duration = Random.Range(minDuration, maxDuration);
			pickTargetPos();
		}

	}

	private void pickTargetPos() {
		int directionPicker = (int)Random.Range(0, 3);
		switch (directionPicker) {
			case 0: {
				targetPos = new Vector3((int)Random.Range(0, width) + 0.5f, transform.position.y, transform.position.z);
				break;
			}
			case 1: {
				targetPos = new Vector3(transform.position.x, (int)Random.Range(0, height) + 0.5f, transform.position.z);
				break;
			}
			case 2: {
				targetPos = new Vector3(transform.position.x, transform.position.y, (int)Random.Range(0, width) + 0.5f);
				break;
			}
		}
	}
}
