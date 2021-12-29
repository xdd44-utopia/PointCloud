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
	private const float activateSpeed = 1.25f;
	private const float deactivateSpeed = 0.75f;

	private Vector3 defaultScale = new Vector3(1f, 1f, 1f);

	private int selfDeactiveTimer = 0;
	private const float selfDeactiveLimit = 0.5f;

	private Vector3 originPos;
	private Vector3 basePos = new Vector3(0, 0, 0);
	private Vector3 targetPos = new Vector3(0, 0, 0);

	private float timer;
	private float offsetSpeed = 0.1f;
	private float offsetScale = 0.05f;
	private bool hasOffset = false;

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

		toggleActive(new Vector3(0, 0, 0), -1, false);
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
				if (hasOffset) {
					offset();
				}
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
	}

	private void updateTexture() {
		if (hasOffset) {
			mat.SetVector("Vector3_3ccdb16aef4a4656a55b99bdfb33b70c", new Vector4(transform.position.x + originPos.x, transform.position.y + originPos.y, transform.position.z + originPos.z, 0));
		}
		else {
			mat.SetVector("Vector3_3ccdb16aef4a4656a55b99bdfb33b70c", new Vector4(transform.position.x + Random.Range(0, 5), transform.position.y + Random.Range(0, 5), transform.position.z + Random.Range(0, 5), 0));
		}
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
			pool.GetComponent<PrefabPool>().removeBlock((int)basePos.x, (int)basePos.y, (int)basePos.z);
		}
	}

	public void toggleActive(Vector3 op, int bs, bool b) {
		state = State.activating;
		fadePos = 0;
		basePos = transform.position;
		targetPos = basePos;
		if (bs > 0) {
			originPos = op;
			defaultScale = new Vector3(bs, bs, bs);
			hasOffset = b;
		}
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
		float x = Mathf.PerlinNoise(timer * offsetSpeed + basePos.x * offsetScale, basePos.y * offsetScale);
		targetPos = basePos + new Vector3((x - 0.5f) * 2f, (x - 0.5f) * 1f, (x - 0.5f) * 2f);
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
