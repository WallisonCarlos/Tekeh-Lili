using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
		Esta classe é responsável por gerar os inimigos intermediários aleatóriamente
		De acordo com a posição de um objeto gerador
	*/
public class GeneratorEnemy : MonoBehaviour {
	//Variável do tipo de objeto que quero gerar
	[SerializeField]
	private GameObject enemy;
	//Variáveis que são instanciadas com o tipo de objeto que quero gerar, no caso Enemy
	[SerializeField]
	private GameObject instance1;
	[SerializeField]
	private GameObject instance2;
	[SerializeField]
	private GameObject instance3;
	//Verifica se é para continuar gerando
	private bool generate = false;
	//Delay para geração de inimigos
	public float generateDelay = 0.5f;
	//Variável para contar o tempo
	private float generateTimer;
	//Número maximo que vai ser gerado
	[SerializeField]
	private int generateMax;
	//Contagem de quantos foram gerados
	private int generateCount;
	//Variavel que verifica se o primeiro já foi gerado, caso não ele gera rapidamente, e os outros são gerados com uma maior demora
	private bool primeiro = false;
	// Use this for initialization
	void Start () {
		primeiro = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Gerando os inimigos
		if (generate && (generateCount < generateMax)) {
			generateTimer += Time.deltaTime;
			if (generateTimer >= generateDelay) {
				GenerateEnemy ();
				generateCount++;
				generateTimer = 0;
				if (primeiro) {
					primeiro = true;
					generateDelay += 5.5f;
				}
			}
		}
	}
	//Verifica se o player está na região onde são gerados os inimigos, se ele tiver gera inimigos
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			generate = true;	
		}
	}
	//Se o player sair da região onde gera inimigos, o gerado para de gerar
	void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			generate = false;	
		}
	}

	void GenerateEnemy() {
		GameObject instance = null;
		float random = Random.Range (1.0f, 15.0f);
		if (random <= 5) {
			instance = instance1;
		} else if (random > 5 && random <= 10) {
			instance = instance2;
		} else {
			instance = instance3;
		}
			
		GameObject enemyInstance = (GameObject) (Instantiate(enemy, instance.transform.position, Quaternion.identity));

	}
}
