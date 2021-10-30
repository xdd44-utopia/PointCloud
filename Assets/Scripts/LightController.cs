using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;	

public class LightController : MonoBehaviour
{
	public Texture2D texSide;
	public Texture2D texTop;
	private HDAdditionalLightData lightData;
	private const int size = 120;
	// Start is called before the first frame update
	void Start()
	{
		lightData = GetComponent<HDAdditionalLightData>();
	}

	// Update is called once per frame
	void Update()
	{
		Color c1 = texSide.GetPixel((int)(transform.position.z + 0.5f) * size, (int)(transform.position.y + 0.5f) * size);
		Color c2 = texTop.GetPixel((int)(transform.position.x + 0.5f) * size, (int)(transform.position.z + 0.5f) * size);
		Color c3 = texSide.GetPixel((int)(transform.position.x + 0.5f) * size, (int)(transform.position.y + 0.5f) * size);
		lightData.color = new Color((c1.r + c2.r + c3.r) / 3, (c1.g + c2.g + c3.g) / 3, (c1.b + c2.b + c3.b) / 3, 1);
		// if (transform.localPosition.x != 0) {
		// 	lightData.color = texSide.GetPixel((int)(transform.position.z + 0.5f) * size, (int)(transform.position.y + 0.5f) * size);
		// }
		// else if (transform.localPosition.y != 0) {
		// 	lightData.color = texTop.GetPixel((int)(transform.position.x + 0.5f) * size, (int)(transform.position.z + 0.5f) * size);
		// }
		// else if (transform.localPosition.z != 0) {
		// 	lightData.color = texSide.GetPixel((int)(transform.position.x + 0.5f) * size, (int)(transform.position.y + 0.5f) * size);
		// }
	}
}
