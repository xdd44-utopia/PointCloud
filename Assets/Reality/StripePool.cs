using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripePool : MonoBehaviour
{
	public Material[] materials;
	public Transform currentCam;
	public GameObject prefab;
	private int groupCount = 0;
	// Start is called before the first frame update
	void Start()
	{
		// spawnStripes(
		// 	new Vector3(-2f, 0.5f, -1.5f),
		// 	new Vector3(0, 0, 0.04f),
		// 	new Vector4(5f, 9.5f, 0, 0),
		// 	new Vector4(0.01f, 0, 0, 0),
		// 	new Vector3(0.8f, 0.2f, 0.5f),
		// 	25,
		// 	0,
		// 	0.25f
		// );
		// spawnStripes(
		// 	new Vector3(-2.5f, 1f, -2f),
		// 	new Vector3(0.025f, 0, 0.0001f),
		// 	new Vector4(5f, 11.1f, 0, 0),
		// 	new Vector4(0.024f, 0, 0, 0),
		// 	new Vector3(0.8f, 0.2f, 0.5f),
		// 	25,
		// 	0,
		// 	0.25f
		// );

		for (int i=0;i<100;i++) {
			int[] dx = new int[6]{1, -1, 0, 0, 0, 0};
			int[] dy = new int[6]{0, 0, 1, -1, 0, 0};
			int[] dz = new int[6]{0, 0, 0, 0, 1, -1};
			int pick = (int)Random.Range(0, 6);
			Vector3 dir = new Vector3(dx[pick], dy[pick], dz[pick]);
			Vector3 pos = new Vector3(Random.Range(-3, 3), Random.Range(0, 5), Random.Range(-2, 0.75f));
			Vector4 tex = new Vector4(Random.Range(0, 20), Random.Range(0, 20), Random.Range(0, 20), 0);
			spawnStripes(
				pos,
				dir * Random.Range(0.01f, 0.05f),
				tex,
				new Vector4(0.05f, 0, 0, 0),
				new Vector3(Random.Range(0.1f, 0.25f), Random.Range(0.1f, 0.25f), 1),
				(int)Random.Range(25, 75),
				0,
				Random.Range(0.25f, 0.5f)
			);
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void spawnStripes(Vector3 pos, Vector3 dPos, Vector4 pTex, Vector4 dTex, Vector3 size, int num, int matIndex, float matScale) {
		groupCount++;
		Quaternion dir = Quaternion.Euler(0, 0, 0);
		if (dPos.z != 0) {
			dir = Quaternion.Euler(0, 0, 0);
		}
		else if (dPos.x != 0) {
			dir = Quaternion.Euler(0, 90, 0);
		}
		else if (dPos.y != 0) {
			if (pos.y > currentCam.position.y) {
				dir = Quaternion.Euler(-90, 0, 0);
			}
			else {
				dir = Quaternion.Euler(90, 0, 0);
			}
		}
		// if (dPos.z != 0) {
		// 	dir = Quaternion.Euler(0, 90, 0);
		// }
		// else if (dPos.x != 0) {
		// 	dir = Quaternion.Euler(0, 0, 0);
		// }
		// else if (dPos.y != 0) {
		// 	dir = Quaternion.Euler(0, 90, 0);
		// }
		for (int i=0;i<num;i++) {
			Material newMat = new Material(materials[matIndex]);
			newMat.SetFloat("Vector1_6cb8bb40d01445bbac20f33c93c535a4", matScale);
			newMat.SetVector("Vector3_3ccdb16aef4a4656a55b99bdfb33b70c", pTex - i * new Vector4(dPos.x, dPos.y, dPos.z, 0) + i * dTex);
			
			GameObject newStripe = Instantiate(prefab, pos + i * dPos, dir);
			newStripe.transform.localScale = size;
			newStripe.GetComponent<MeshRenderer>().material = newMat;
			newStripe.name = "Group " + groupCount;
		}
	}
}
