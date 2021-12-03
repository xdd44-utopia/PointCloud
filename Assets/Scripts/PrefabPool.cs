using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
	public GameObject prefab;
	public GameObject[] entities;
	public int layer;

	private Queue<int> queue;
	private List<GameObject> list;

	private const int spaceSize = 128;
	private int[,,] space = new int[spaceSize * 2, spaceSize * 2, spaceSize * 2];


	// Start is called before the first frame update
	void Start()
	{
		layer = 1 << layer;
		queue = new Queue<int>();
		list = new List<GameObject>();
		for (int i=0;i<spaceSize * 2;i++) {
			for (int j=0;j<spaceSize * 2;j++) {
				for (int k=0;k<spaceSize * 2;k++) {
					space[i, j, k] = -1;
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		updateVoxel();
	}

	private void updateVoxel() {
		float checkXLower = transform.position.x - transform.localScale.x / 2;
		float checkXHigher = transform.position.x + transform.localScale.x / 2;
		float checkYLower = transform.position.y - transform.localScale.y / 2;
		float checkYHigher = transform.position.y + transform.localScale.y / 2;
		float checkZLower = transform.position.z - transform.localScale.z / 2;
		float checkZHigher = transform.position.z + transform.localScale.z / 2;
		for (int x=(int)checkXLower;x<=checkXHigher;x++) {
			for (int z=(int)checkZLower;z<=checkZHigher;z++) {
				RaycastHit hitDownward;
				RaycastHit hitUpward;
				int upper = spaceSize * 10;
				int lower = spaceSize * 10;
				if (Physics.Raycast(
					new Vector3(x, spaceSize, z),
					new Vector3(0, -1, 0),
					out hitDownward,
					Mathf.Infinity,
					layer)
				) {
					upper = (int)Mathf.Ceil(hitDownward.point.y);
				}
				if (Physics.Raycast(
					new Vector3(x, -spaceSize, z),
					new Vector3(0, 1, 0),
					out hitUpward,
					Mathf.Infinity,
					layer)
				) {
					lower = (int)Mathf.Floor(hitUpward.point.y);
				}
				if (upper != spaceSize * 10 && lower != spaceSize * 10) {
					upper = upper < (int)checkYHigher ? upper : (int)checkYHigher;
					lower = lower > (int)checkYLower ? lower : (int)checkYLower;
					for (int y=lower;y<=upper;y++) {
						addBlock(x, y, z);
					}
				}
			}
		}
	}
	
	public void addBlock(int x, int y, int z) {
		x += spaceSize;
		y += spaceSize;
		z += spaceSize;
		if (x < 0 || y < 0 || z < 0 || x >= spaceSize * 2 || y >= spaceSize * 2 || z >= spaceSize * 2) {
			return;
		}
		if (space[x, y, z] >= 0) {
			list[space[x, y, z]].GetComponent<BlockController>().confirmActive();
			return;
		}
		int pointer = getPrefab();
		list[pointer].transform.position = new Vector3(x - spaceSize, y - spaceSize, z - spaceSize);
		list[pointer].GetComponent<BlockController>().toggleActive();
		space[x, y, z] = pointer;
	}

	public void removeBlock(int x, int y, int z) {
		x += spaceSize;
		y += spaceSize;
		z += spaceSize;
		if (x < 0 || y < 0 || z < 0 || x >= spaceSize * 2 || y >= spaceSize * 2 || z >= spaceSize * 2) {
			return;
		}
		if (space[x, y, z] < 0) {
			return;
		}
		queue.Enqueue(space[x, y, z]);
		space[x, y, z] = -1;
	}

	private int getPrefab() {
		int pointer = 0;
		if (queue.Count > 0) {
			pointer = queue.Dequeue();
		}
		else {
			GameObject newObject = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
			list.Add(newObject);
			pointer = list.Count - 1;
			newObject.GetComponent<BlockController>().index = pointer;
			newObject.GetComponent<BlockController>().pool = this.gameObject;
		}
		return pointer;
	}
}
