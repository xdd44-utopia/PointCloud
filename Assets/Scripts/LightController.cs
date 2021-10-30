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
		if (transform.localPosition.x != 0) {
			lightData.color = texSide.GetPixel((int)(transform.position.z + 0.5f) * size, (int)(transform.position.y + 0.5f) * size);
		}
		else if (transform.localPosition.y != 0) {
			lightData.color = texTop.GetPixel((int)(transform.position.x + 0.5f) * size, (int)(transform.position.z + 0.5f) * size);
		}
		else if (transform.localPosition.z != 0) {
			lightData.color = texSide.GetPixel((int)(transform.position.x + 0.5f) * size, (int)(transform.position.y + 0.5f) * size);
		}
	}
}
