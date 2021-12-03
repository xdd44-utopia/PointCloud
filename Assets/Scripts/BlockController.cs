using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	[HideInInspector]
	public GameObject pool;
	public int index;
	public Material matPrefab;
	private Material mat;
	private const float lerpSpeed = 5f;

	private float fadePos;
	private const float activateSpeed = 1f;
	private const float deactivateSpeed = 0.75f;

	private Vector3 defaultScale = new Vector3(1.5f, 1.5f, 1.5f);

	private int selfDeactiveTimer = 0;
	private const int selfDeactiveLimit = 5;

	private Vector3 basePos = new Vector3(0, 0, 0);
	private Vector3 targetPos = new Vector3(0, 0, 0);

	private float timer;
	private float offsetSpeed = 0.1f;
	private float offsetScale = 0.25f;

	private enum State {
		activating,
		activated,
		deactivating,
		sleep
	}

	private State state;

	// Start is called before the first frame update
	void Start()
	{
		mat = new Material(matPrefab);
		GetComponent<MeshRenderer>().material = mat;

		state = State.sleep;

		toggleActive();
	}

	// Update is called once per frame
	void Update()
	{

		switch (state) {
			case State.activating: {
				activating();
				break;
			}
			case State.deactivating: {
				deactivating();
				break;
			}
			case State.activated: {
				transform.localScale = defaultScale;
				selfDeactiveTimer++;
				//offset();
				if (selfDeactiveTimer >= selfDeactiveLimit) {
					toggleDeactive();
				}
				break;
			}
			case State.sleep: {
				transform.localScale = new Vector3(0, 0, 0);
				break;
			}
		}

		timer += Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10);
		// if (index == 0) {
		// 	Debug.Log(transform.position + " " + targetPos);
		// }

	}

	private void updateTexture() {
		mat.SetVector("Vector3_3ccdb16aef4a4656a55b99bdfb33b70c", new Vector4(transform.position.x, transform.position.y, transform.position.z, 0));
	}
	
	private void activating() {
		updateTexture();
		transform.localScale = fadeVector(new Vector3(0, 0, 0), defaultScale, fadePos);
		fadePos += Time.deltaTime * activateSpeed;
		if (fadePos > 1) {
			state = State.activated;
		}
	}
	private void deactivating() {
		transform.localScale = fadeVector(defaultScale, new Vector3(0, 0, 0), fadePos);
		fadePos += Time.deltaTime * deactivateSpeed;
		if (fadePos > 1) {
			state = State.sleep;
			pool.GetComponent<PrefabPool>().removeBlock((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
		}
	}

	public void toggleActive() {
		state = State.activating;
		fadePos = 0;
		basePos = transform.position;
		targetPos = basePos;
		confirmActive();
	}
	public void toggleDeactive() {
		state = State.deactivating;
		fadePos = 0;
	}

	public void confirmActive() {
		selfDeactiveTimer = 0;
		if (state == State.deactivating) {
			state = State.activating;
			fadePos = 1 - fadePos;
		}
	}

	private void offset() {
		float x = Mathf.PerlinNoise(timer * offsetSpeed + transform.position.z * offsetScale, transform.position.y * offsetScale);
		targetPos = basePos + new Vector3((x - 0.5f) * 1f, 0, 0);
	}

	private Vector3 fadeVector(Vector3 x, Vector3 y, float t) {
		return new Vector3(fadeFloat(x.x, y.x, t), fadeFloat(x.y, y.y, t), fadeFloat(x.z, y.z, t));
	}

	private float fadeFloat(float x, float y, float t) {
		t = t >= 0 ? t : 0;
		t = t <= 1 ? t : 1;
		t = 6 * Mathf.Pow(t, 5) - 15 * Mathf.Pow(t, 4) + 10 * Mathf.Pow(t, 3);
		return x + (y - x) * t;
	}
}
