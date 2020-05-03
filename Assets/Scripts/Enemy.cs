using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private bool ladoDireito;
	[SerializeField]
	private float velocidade = 3;
	private Animator animator;
	//Parado
	private float tempoIdle;
	[SerializeField]
	private float duracaoIdle =5;
	//Patrulhando
	private float tempoPatrulhar=5;
	[SerializeField]
	private float duracaoPatrulhar =5;
	//Atacando
	private float tempoAtacar=2;
	[SerializeField]
	private float duracaoAtacar=2;
	[SerializeField]
	private bool patrulhando;
	[SerializeField]
	private bool atacar;
	[SerializeField]
	private float playerDistancia;
	[SerializeField]
	private float ataqueDistancia;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private float dano;
	[SerializeField]
	private EdgeCollider2D weapon;
	[SerializeField]
	//public float life;
	private LifeEnemy life;
	private float restartDelay = 1.5f;
	private float restartTimer;
	//Plus Weapon
	public GameObject weapon1;
	public GameObject weapon2;
	public static bool weaponGenerated = true;
	public GameObject positionWeapon;
	private bool geraArma;
	// Use this for initialization
	void Start () {
		geraArma = true;
		ladoDireito = true;
		animator = GetComponent<Animator> ();
		patrulhando = false;
		atacar = false;
		weapon.enabled = false;
		life = GetComponent<LifeEnemy> ();
	}

	// Update is called once per frame
	void Update () {
		MudarEstado ();
		playerDistancia = transform.position.x - Player.Instance.transform.position.x;
		if (Mathf.Abs (playerDistancia) < ataqueDistancia) {
			atacar = true;
			patrulhando = false;
			Idle ();
			Atacar ();
		} else {
			MudarEstado ();
		}
		DesativarInimigo ();
		ResetarAtaquar ();
	}

	void OnTriggerEnter2D(Collider2D collision){
		if(collision.tag == "LimiteEnemy"){
			MudaDirecao ();
		}
		if (collision.tag == "PlayerSword") {
			float d = this.dano + 2;
			SofrerDano (d);
		}

		if (collision.tag == "PlayerKunai") {
			float d = this.dano + 1;
			SofrerDano (d);
		}

		if (collision.tag == "PlayerShuriken") {
			float d = this.dano + 0.5f;
			SofrerDano (d);
		}

		if (collision.tag == "PlayerWeapon") {
			SofrerDano (dano);
		}
	}

	private void SofrerDano(float dano){
		life.curHealth -= dano;
		if (life.curHealth <= 0) {
			animator.SetTrigger ("Morrendo");
		} else {
			animator.SetLayerWeight (1, 1);
			animator.SetTrigger ("Dano");
		}
	}

	public void DesativarInimigo(){
		if (life.curHealth <= 0) {
			restartTimer += Time.deltaTime;
			if (geraArma) {
				GenerateWeapon ();
				geraArma = false;
			}
			if (restartTimer >= restartDelay) {
				gameObject.SetActive (false);
			}

		}
	}

	public void SetLayerDano(){
		animator.SetLayerWeight (1, 0);
	}

	private void Mover(){
		transform.Translate (PegarDirecao() * (velocidade * Time.deltaTime));
	}

	private Vector2 PegarDirecao(){
		return ladoDireito ? Vector2.right : Vector2.left;
	}

	private void MudaDirecao(){
		ladoDireito = !ladoDireito;
		this.transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	//Métodos para animações e ações

	private void Idle(){
		tempoIdle += Time.deltaTime;
		if (tempoIdle <= duracaoIdle) {
			animator.SetBool ("Patrulhando", patrulhando);
			tempoPatrulhar = 0;
		} else {
			patrulhando = true;
		}
	}

	private void Patrulhar(){
		tempoPatrulhar += Time.deltaTime;
		if (tempoPatrulhar <= duracaoPatrulhar) {
			animator.SetBool ("Patrulhando", patrulhando);
			Mover ();
			tempoIdle = 0;
		} else {
			patrulhando = false;
		}
	}

	private void Atacar(){
		if (playerDistancia < 0 & !ladoDireito || playerDistancia > 0 && ladoDireito) {
			MudaDirecao ();
		}
		if (atacar) {
			tempoAtacar += Time.deltaTime;
			if(tempoAtacar >= duracaoAtacar){
				animator.SetTrigger ("Atacando");
				tempoAtacar = 0;
			}
		}
	}

	private void MudarEstado(){
		if (!atacar) {
			if (!patrulhando) {
				Idle ();
			} else {
				Patrulhar ();
			}
		}
	}

	public void ResetarAtaquar(){
		atacar = false;
	}

	public void MeleeAttack () {
		weapon.enabled = !weapon.enabled;
	}

	public void Inicializar(Vector2 dir) {
		ladoDireito = transform.localScale.x > 0;
		if (dir.x > 0 && !ladoDireito || dir.x<0 && ladoDireito) {
			ladoDireito = !ladoDireito;
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}

	void GenerateWeapon() {
		if (positionWeapon != null) {
			GameObject instance = null;
			if (weaponGenerated) {
				instance = weapon1;
				weaponGenerated = false;
			} else {
				instance = weapon2;
				weaponGenerated = true;
			}

			GameObject weaponInstance = (GameObject)(Instantiate (instance, positionWeapon.transform.position, Quaternion.identity));
		}
	}
}
