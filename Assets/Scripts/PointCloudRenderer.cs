using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PointCloudRenderer : MonoBehaviour
{
	public Texture2D tex;
	private Texture2D texColor;
	private Texture2D texPosScale;
	private VisualEffect vfx;
	private uint resW = 1000;
	private uint resH = 1000;

	private float particleSize = 0.02f;
	private bool toUpdate = false;
	private uint particleCount = 0;

	// Start is called before the first frame update
	void Start()
	{
		vfx = GetComponent<VisualEffect>();
		setParticles();
	}

	// Update is called once per frame
	void Update()
	{
		if (toUpdate) {
			toUpdate = false;
			vfx.Reinit();
			vfx.SetUInt(Shader.PropertyToID("ParticleCount"), particleCount);
			vfx.SetTexture(Shader.PropertyToID("TexColor"), texColor);
			vfx.SetTexture(Shader.PropertyToID("TexPosScale"), texPosScale);
			vfx.SetUInt(Shader.PropertyToID("ResolutionW"), resW);
			vfx.SetUInt(Shader.PropertyToID("ResolutionH"), resH);
		}
	}

	private void setParticles() {
		int texWidth = tex.width;
		int texHeight = tex.height;
		texColor = new Texture2D(texWidth, texHeight, TextureFormat.RGBAFloat, false);
		texPosScale = new Texture2D(texWidth, texHeight, TextureFormat.RGBAFloat, false);
		for (int i=0;i<texWidth;i++) {
			for (int j=0;j<texHeight;j++) {
				Color pix = tex.GetPixel(i, j);
				float grayScale = 0.299f * pix.r + 0.587f * pix.g + 0.114f * pix.b;
				texColor.SetPixel(i, j, new Color(pix.r, pix.g, pix.b, 1));
				texPosScale.SetPixel(i, j, new Color((i - texWidth / 2) / 100f, grayScale, (j - texHeight / 2) / 100f, particleSize));
			}
		}
		texColor.Apply();
		texPosScale.Apply();
		particleCount = (uint)(texWidth * texHeight);
		resW = (uint)texWidth;
		resH = (uint)texHeight;
		toUpdate = true;
	}
}
