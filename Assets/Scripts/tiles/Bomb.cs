﻿using UnityEngine;
using System.Collections;
using Pixelbox;

namespace Bomberman.Tiles {
	public class Bomb : Destructible {
		[HideInInspector] public int flameSize;
		[HideInInspector] public int timer;

		public GameObject explosionPrefab;

		protected bool isMoving = false;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public override void Init(MapItem item, Stage stage) {
			base.Init(item, stage);
			flameSize = 4;
			timer = 60 + Random.Range(0, 120); // TODO
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Init(int i, int j, int flameSize, int timer) {
			this.i = i;
			this.j = j;
			this.flameSize = flameSize;
			this.timer = timer;
			this.stage = Stage.instance;

			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.sortingOrder = j;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		void Update() {
			timer--;
			// TODO animation
			if (isMoving) return;
			if (timer < 0) Explode();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		protected override IEnumerator ExplosionCoroutine() {
			// wait for exactly 6 frames before explosion
			for (int c = 0; c < 6; c++) {
				yield return null;
			}

			GameObject instance = (GameObject)Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			instance.GetComponent<Explosion>().Init(i, j, flameSize);

			for (int f = 0; f < destructAnim.Length; f++) {
				spriteRenderer.sprite = destructAnim[f];
				for (int c = 0; c < 5; c++) {
					// wait for exactly one frame
					yield return null;
				}
			}

			stage.RemoveTile(i, j);
			Destroy(gameObject);
		}
	}
}
