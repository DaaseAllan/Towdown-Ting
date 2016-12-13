using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float Firerate = 0;
	public float Damage = 10;
	public float Bulletrange = 100;
	public float bulletspeed = 10;
	public LayerMask WhatToHit;
	public GameObject Bullet;
	public float Del1 = 0;
	public float Del2 = 0;
	public float Del3 = 0;
	public GameObject Middel;
	float TimeToFire = 0;
	Transform firepoint;
	public Sprite midte1sprite;
	public float bulletspread = 0;
	public float amountofbullets;

	private float spreadamount;
	private float bulletsshot;

	void Awake () {
		firepoint = transform.FindChild ("FirePoint");
		if (firepoint == null) {
			Debug.LogError ("FirePoint not found");



		}
	}

	void Start()
	{
		Del1 = Random.Range (1, 3);
		Del2 = 1;
		Del3 = Random.Range (1, 3);
		Debug.Log ("del" + Del1);

		if (Del2 == 1) 
		{
			Middel.GetComponent<SpriteRenderer> ().sprite = midte1sprite;
		
		}
	}


	void Update ()
	{
		if (Firerate == 0) 
		{
			if (Input.GetButtonDown("Fire1")&& Time.time > TimeToFire)
			{
				TimeToFire = Time.time + 0.3f;

				if (amountofbullets > bulletsshot) {
					ShootBullet ();
					bulletsshot += 1;
					Debug.Log (bulletsshot);
				} else 
				{
					bulletsshot = 0;
				}
			}
	}
		else 
		{
			if (Input.GetButton("Fire1") && Time.time > TimeToFire) 
			{
				TimeToFire = Time.time + 1/Firerate;
				ShootBullet ();
			}
		}
}
	void ShootRay() {
		Debug.Log ("Der er skudt");
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firepoint.position.x, firepoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, Bulletrange, WhatToHit);
		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.blue);
		if (hit.collider != null) 
		{
			Debug.Log ("sollid ramt");
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("Ramte " + hit.collider.name + " og gjorde " + Damage + " skade");
		}
	

	}
	void ShootBullet(){

			GameObject BulletPrefab = Instantiate (Bullet) as GameObject;
			BulletPrefab.transform.position = firepoint.transform.position;
			BulletPrefab.transform.up = transform.up;
			//	BulletPrefab.transform.rotation = Quaternion.Euler(new Vector3 (90,90,0));
			BulletPrefab.GetComponent<Rigidbody2D> ().AddForce (BulletPrefab.transform.up * bulletspeed);
			spreadamount = Random.Range (-bulletspread, bulletspread);
			BulletPrefab.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (spreadamount, spreadamount));
	
	}
}
