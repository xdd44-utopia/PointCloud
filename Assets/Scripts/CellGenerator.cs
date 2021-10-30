using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
	public GameObject cellPrefab;
	// Start is called before the first frame update
	void Start()
	{
		for (int i=0;i<10;i++) {
			GameObject.Instantiate(cellPrefab, new Vector3(7.5f, 4f, 7.5f), Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
