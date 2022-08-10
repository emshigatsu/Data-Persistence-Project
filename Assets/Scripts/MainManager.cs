using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
	public static MainManager Instance;

	public Brick BrickPrefab;
	public int LineCount = 6;
	public Rigidbody Ball;

	public Text ScoreText;
	public Text bestScoreText;
	public string playerName2;
	public string bestPlayer;

	public GameObject GameOverText;

	private bool m_Started = false;
	public int m_Points;

	private bool m_GameOver = false;

	public int highScore;

	private void Awake()
	{
		bool isNew = false;
		if (Instance != null)
		{
			Destroy(Instance.gameObject);
			isNew = true;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		LoadScore();

		if (isNew) OnStart();
	}


	// Start is called before the first frame update
	public void OnStart()
	{
		const float step = 0.6f;
		int perLine = Mathf.FloorToInt(4.0f / step);

		int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
		for (int i = 0; i < LineCount; ++i)
		{
			for (int x = 0; x < perLine; ++x)
			{
				Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
				var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
				brick.PointValue = pointCountArray[i];
				brick.onDestroyed.AddListener(AddPoint);
			}
		}

		playerName2 = SceneData.Instance.playerName;
		bestScoreText.text = "Best Score :" + bestPlayer + ": " + highScore;
	}
	public void StartNew()
	{
		LoadScore();
		SceneManager.LoadScene(0);
		Destroy(GameObject.FindWithTag("brick"));
	}



	private void Update()
	{
		if (!m_Started)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				m_Started = true;
				float randomDirection = Random.Range(-1.0f, 1.0f);
				Vector3 forceDir = new Vector3(randomDirection, 1, 0);
				forceDir.Normalize();

				Ball.transform.SetParent(null);
				Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
			}
		}
		else if (m_GameOver)
		{
			GameOver();
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}

		}
	}

	void AddPoint(int point)
	{
		m_Points += point;
		ScoreText.text = $"Score : {m_Points}";
	}


	public void GameOver()
	{
		m_GameOver = true;
		GameOverText.SetActive(true);
		if (m_Points > highScore)
		{
			highScore = m_Points;
			bestPlayer = playerName2;
			SaveScore();
		}
		bestScoreText.text = "Best Score :" + bestPlayer + ": " + highScore;
	}
	[System.Serializable]
	class SaveData
	{
		public int highScore;
		public string bestPlayer;
	}

	public void SaveScore()
	{
		string path = Application.persistentDataPath + "/savefile.json";

		SaveData data = new SaveData();

		data.highScore = m_Points;

		data.bestPlayer = playerName2;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadScore()
	{
		string path = Application.persistentDataPath + "/savefile.json";

		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			highScore = data.highScore;
			bestPlayer = data.bestPlayer;
		}
	}
}



