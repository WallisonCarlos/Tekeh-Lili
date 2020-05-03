using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {

	private static Player instace;
	public Rigidbody2D rb2D;
	private float horizontal;
	private float vertical;
	[SerializeField]
	public float velocidade = 8;
	[SerializeField]
	private float climbSpeed = 5;
	public bool ladoDireito;
	[SerializeField]
	private float vida = 100;
	public Animator animator { get; set;}
	[SerializeField]
	private LayerMask platform;
	//Wall
	[SerializeField]
	private LayerMask wall;
	[SerializeField]
	private Transform wallCheck;
	[SerializeField]
	private float wallTouchRadius = 0.2f;
	bool touchingWall = false;
	[SerializeField]
	private Vector2 pointCollisionWall = Vector2.zero;
	//End Wall
	[SerializeField]
	private Vector2 pontoColisaoPiso = Vector2.zero;
	public bool isFloor { get; set;}
	private bool emAcao;
	private bool emAtaque;
	private bool attack;
	[SerializeField]
	private float raio;
	private Color corColisao = Color.red;
	public float jumpForce = 300f;
	public float jumpPushForce = 10f;
	[SerializeField]
	private GameObject kunai;
	[SerializeField]
	private GameObject posicaoKunai;
	[SerializeField]
	private GameObject shurikens;
	[SerializeField]
	private GameObject posicaoShurikens;
	[SerializeField]
	private EdgeCollider2D swordCollider;
	public bool onLadder {get; set;}
	private IUseable useable;
	private Life life;
	private float restartDelay = 1.5f;
	private float restartTimer;
	private bool doubleJump = false;
	public int score = 0;
	public int kunaiCount = 5;
	public int shurikenCount = 10;
	public Text scoreKunai;
	public Text scoreShuriken;
	//Sounds
	public AudioClip shurikenSound;
	public AudioClip kunaiSound;
	public AudioClip moedaSound;
	public AudioClip espadaSound;
	private AudioSource audio;
	void Start () {
		GetData ();
		scoreKunai.text = "" + kunaiCount.ToString ();
		scoreShuriken.text = "" + shurikenCount.ToString ();
		life = GetComponent<Life> ();
		rb2D = GetComponent<Rigidbody2D> ();
		ladoDireito = transform.localScale.x > 0;
		animator = GetComponent<Animator> ();
		onLadder = false;
		audio = GetComponent<AudioSource> ();
		audio.playOnAwake = false;
		GetData ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		Flip (horizontal);
		HandleMovement (horizontal, vertical);
		IsFloor ();
		ControlLayers ();
		HandleInputs ();
		HandleAttacks ();
		Action ();
		if (life.vidaPersonagem <= 0) {
			restartTimer += Time.deltaTime;
			if (restartTimer >= restartDelay) {
				//Application.LoadLevel (Application.loadedLevel);
				Scene scene = SceneManager.GetActiveScene ();
				if (scene.buildIndex == 3 || scene.buildIndex == 5 || scene.buildIndex == 7) {
					Application.LoadLevel (Application.loadedLevel);
				} else {
					rb2D.transform.position = CheckPoint.GetActiveCheckPointPosition ();
				}
				life.vidaPersonagem = 100;
				PlayerPrefs.DeleteKey ("dataPlayer");
			}
		}
		if (isFloor) 
		{
			doubleJump = false;
		}
		if (touchingWall) 
		{
			isFloor = false; 
			doubleJump = false; 
		}
	}

	void Update () {
		ResetValues ();
	}

	private void GetData () {
		if (PlayerPrefs.HasKey ("dataPlayer") && PlayerPrefs.GetInt ("dataPlayer") == 1) {
			Life life = Instance.GetComponent<Life> ();
			life.vidaPersonagem = PlayerPrefs.GetFloat ("vidaPersonagem");
			kunaiCount = PlayerPrefs.GetInt ("kunaiCount");
			shurikenCount = PlayerPrefs.GetInt ("shurikenCount");
			score = PlayerPrefs.GetInt ("score");
		}
	}

	private void ResetValues (){
		emAcao = false;
		attack = false;
	}

	public static Player Instance {
		get { 
			if (instace == null) {
				instace = GameObject.FindObjectOfType<Player> ();
			}
			return instace;
		}
	}

	private void HandleMovement (float horizontal, float vertical) {
		if (!this.animator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") && !this.animator.GetCurrentAnimatorStateInfo (0).IsTag ("Throw")) {
			rb2D.velocity = new Vector2 (horizontal * velocidade, rb2D.velocity.y);
		}
		animator.SetFloat ("Velocidade", Mathf.Abs (horizontal));
		if (onLadder) {
			animator.speed = vertical != 0 ? Mathf.Abs (vertical) : Mathf.Abs (horizontal);
			rb2D.velocity = new Vector2 (horizontal * climbSpeed, vertical * climbSpeed);
		}
	}

	private void Flip (float horizontal) {
		if (horizontal > 0 && !ladoDireito || horizontal < 0 && ladoDireito) {
			ladoDireito = !ladoDireito;
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}

	private void IsFloor () {
		var pontoPosicao = pontoColisaoPiso;
		pontoPosicao.x += transform.position.x;
		pontoPosicao.y += transform.position.y;
		isFloor = Physics2D.OverlapCircle (pontoPosicao, raio, platform);
		var pointPositionWall = pointCollisionWall;
		pointPositionWall.x += transform.position.x;
		pointPositionWall.y += transform.position.y;
		touchingWall = Physics2D.OverlapCircle(pointPositionWall, wallTouchRadius, wall);
		Cair ();
	}

	void OnDrawGizmos () {
		Gizmos.color = corColisao;
		var pontoPosicao = pontoColisaoPiso;
		pontoPosicao.x += transform.position.x;
		pontoPosicao.y += transform.position.y;
		Gizmos.DrawWireSphere (pontoPosicao, raio);
		Gizmos.color = Color.blue;
		var pointPositionWall = pointCollisionWall;
		pointPositionWall.x += transform.position.x;
		pointPositionWall.y += transform.position.y;
		Gizmos.DrawWireSphere (pointPositionWall, wallTouchRadius);
	}

	private void Use () {
		if (useable != null) {
			useable.Use ();
		} 
	}

	private void Jump (){
		rb2D.gravityScale = 1.6f;
		if (isFloor && rb2D.velocity.y <= 0) {
			rb2D.AddForce (new Vector2 (0, jumpForce));
			animator.SetTrigger ("Pular");
		}
	}
	private void Cair () {
		if (!isFloor && rb2D.velocity.y <= 0) {
			animator.SetBool ("Cair", true);
			animator.ResetTrigger ("Pular");
		}
		if (isFloor) {
			animator.SetBool ("Cair", false);
		}
	}

	private void ControlLayers () {
		if (!isFloor) {
			animator.SetLayerWeight (1, 1);
		} else {
			animator.SetLayerWeight (1, 0);
		}

	}
		

	private void Action (){
		if(emAtaque && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
			animator.SetTrigger ("Atacar");
			rb2D.velocity = Vector2.zero;
		}
		if (emAcao && !animator.GetCurrentAnimatorStateInfo (0).IsTag ("Throw")) {
			animator.SetTrigger ("Lancar");
			rb2D.velocity = Vector2.zero;
			StartCoroutine(StartCountDown());
			if (Input.GetKeyDown (KeyCode.LeftAlt) || Input.GetKeyDown (KeyCode.C)) {
				AcaoAtirar ();
			}
			if (Input.GetKeyDown (KeyCode.X)) {
				AcaoAtirarShuriken ();
			}
		}
		
	}

	private void AcaoAtirar(){
		if (kunaiCount > 0) {
			GameObject tempProjetil = (GameObject)(Instantiate (kunai, posicaoKunai.transform.position, Quaternion.identity));
			if (ladoDireito) {
				tempProjetil.GetComponent<Kunai> ().Inicializar (Vector2.right);
			} else {
				tempProjetil.GetComponent<Kunai> ().Inicializar (Vector2.left);
			}
			audio.clip = kunaiSound;
			audio.time = 1.5f;
			audio.Play();
			kunaiCount -= 1; 
			scoreKunai.text = "" + kunaiCount.ToString ();
		}
	}

	private void AcaoAtirarShuriken (){
		if (shurikenCount > 0) {
			GameObject tempProjetil = (GameObject)(Instantiate (shurikens, posicaoShurikens.transform.position, Quaternion.identity));
			if (ladoDireito) {
				tempProjetil.GetComponent<Shuriken> ().Inicializar (Vector2.right);
			} else {
				tempProjetil.GetComponent<Shuriken> ().Inicializar (Vector2.left);
			}
			audio.clip = shurikenSound;
			audio.Play();
			shurikenCount -= 1;
			scoreShuriken.text = "" + shurikenCount.ToString ();
		}
	}

	private IEnumerator StartCountDown (float countDownValue = 10) {
		while(countDownValue>0){
			yield return new WaitForSeconds (1.0f);
			countDownValue--;
		}
	}

	private void HandleAttacks () {
		if (attack && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
			animator.SetTrigger ("Atacar");
			rb2D.velocity = Vector2.zero;
		}
	}

	public void MeleeAttack () {
		swordCollider.enabled = !swordCollider.enabled;
	}

	private void HandleInputs () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			audio.clip = espadaSound;
			audio.Play ();
			attack = true;
		}
		if ((isFloor || !doubleJump) && (Input.GetButtonDown("Jump") || Input.GetKeyDown (KeyCode.UpArrow)) && !onLadder) {
			Jump ();
			if (!doubleJump && !isFloor) {
				doubleJump = true;
			}
		}

		if (touchingWall && Input.GetButtonDown("Jump") && !onLadder) {
				WallJump ();	
		}

		if (Input.GetKeyDown (KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C)) {
			emAcao = true;
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			Use ();
		}
	}
	void OnTriggerEnter2D (Collider2D collision) {
		Life life = gameObject.GetComponent<Life> ();
		GameObject go = collision.gameObject;
		if (go.CompareTag("EnemyWeapon")) {
			life.SetVidaPersonagem (life.GetVidaPersonagem ()-1f);
			if (life.GetVidaPersonagem () > 0) {
				animator.SetLayerWeight (2, 1);
				animator.SetTrigger ("Dano");
			} else {
				animator.SetTrigger ("Morrer");
			}
		}
		if (go.CompareTag("MallocWeapon")) {
			life.SetVidaPersonagem (life.GetVidaPersonagem ()-5f);
			if (life.GetVidaPersonagem () > 0) {
				animator.SetLayerWeight (2, 1);
				animator.SetTrigger ("Dano");
			} else {
				animator.SetTrigger ("Morrer");
			}
		}

		if (go.CompareTag("SnowgrowWeapon")) {
			life.SetVidaPersonagem (life.GetVidaPersonagem ()-10f);
			if (life.GetVidaPersonagem () > 0) {
				animator.SetLayerWeight (2, 1);
				animator.SetTrigger ("Dano");
			} else {
				animator.SetTrigger ("Morrer");
			}
		}

		if (go.CompareTag("DrogonWeapon")) {
			life.SetVidaPersonagem (life.GetVidaPersonagem ()-15f);
			if (life.GetVidaPersonagem () > 0) {
				animator.SetLayerWeight (2, 1);
				animator.SetTrigger ("Dano");
			} else {
				animator.SetTrigger ("Morrer");
			}
		}

		if (go.CompareTag("Life")) {
			life.PlusLife (20);
			Debug.Log (collision.gameObject.tag);
			Destroy (go);
		}
			
		if (collision.tag == "Useable") {
			useable = collision.GetComponent<IUseable> ();
		}
	}

	public void OnCollisionEnter2D (Collision2D outro) {
		GameObject go = outro.gameObject;
		if (go.tag == "Coin") {
			//GameManager.Instace.CollectedCoins++;
			Coin coin = go.GetComponent<Coin>();
			coin.SetCountCoins ();
			audio.clip = moedaSound;
			audio.Play();
			Destroy (go);
		}

		if (go.tag == "Life") {
			life.PlusLife (20); 
			Destroy (go);
		}

		if (go.tag == "KunaiPlus") {
			kunaiCount += 1; 
			scoreKunai.text = "" + kunaiCount.ToString ();
			Destroy (go);
		}

		if (go.tag == "ShurikenPlus") {
			shurikenCount += 1;
			scoreShuriken.text = "" + shurikenCount.ToString ();
			Destroy (go);
		}

	}
		
	private void OnTriggerExit2D (Collider2D outro) {
		if (outro.tag == "Useable") {
			useable = null;
		} 
	}
	public void SetLayerDamage () {
		animator.SetLayerWeight (2,0);
	}


	public void Dead () {
		PlayerPrefs.DeleteKey ("dataPlayer");
		life.vidaPersonagem = 0;
	}

	void WallJump () {
		rb2D.AddForce (new Vector2 (jumpPushForce*(-transform.localScale.x), jumpForce));
	}
}
