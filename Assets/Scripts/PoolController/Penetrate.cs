using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrate : MonoBehaviour
{
	public Transform transforms;
	private int num;
	private int curPos;
	private int nexPos;
	private bool hasChild;
	private float interpolation = 0;
	public float interSpeed;
	void Start()
	{
		num = transforms.childCount;
		hasChild = (num >= 2);
		if (hasChild) {
			curPos = Random.Range(0, num);
			nexPos = (curPos + 1) % num;
			transform.position = transforms.GetChild(curPos).position;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (hasChild) {
			transform.position = transforms.GetChild(curPos).position + (transforms.GetChild(nexPos).position - transforms.GetChild(curPos).position) * interpolation;
			interpolation += Time.deltaTime * interSpeed;
			if (interpolation > 1) {
				interpolation = 0;
				curPos++;
				nexPos++;
				curPos %= num;
				nexPos %= num;
			}
		}
	}
}
