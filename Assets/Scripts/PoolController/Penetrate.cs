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
	private float interSpeed = 1.5f;
    void Start()
    {
        num = transforms.childCount;
		hasChild = (num >= 2);
		if (hasChild) {
			curPos = 0;
			nexPos = 1;
			transform.position = transforms.GetChild(0).position;
		}
    }

    // Update is called once per frame
    void Update()
    {
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
