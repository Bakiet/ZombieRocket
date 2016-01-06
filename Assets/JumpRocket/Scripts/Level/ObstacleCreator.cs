using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleCreator : MonoBehaviour {


    /// <summary>
    /// This list contains the available obstacles to spawn. in 0 position I recommend placing the wall and then adding enemies after it.
    /// </summary>
	public List<GameObject> obstacles;

    /// <summary>
    /// This is the position where the lateral walls will spawn. 
    /// </summary>
    public int wallPosition = 16;

    /// <summary>
    /// The row that will start spawning enemies, we allow the player a free of obstacles area to start so she gets the hows of the game.
    /// </summary>
    public int rowIndexToSpawnEnemies = 30;
    
    /// <summary>
    /// The row after which the objects will be spawned acording to the row Indexes for new enemies values.
    /// </summary>
    public int rowIndexToSpawnNewEnemies = 80;
    
    /// <summary>
    /// The rows that increase the range from which we get the objects to spawn.
    /// </summary>
    public int [] rowIndexesForNewEnemies = new int [] { 120, 180, 220, 340 };

    /// <summary>
    /// The list of power ups game objects.
    /// </summary>
    public List<GameObject> powerUps;
    
    /// <summary>
    /// the power ups probabilities.
    /// </summary>
    public List<float> powerUpsProbabilities;

    /// <summary>
    /// This is the amount of rows we spanw in advance.
    /// </summary>
    public int obstacleSpawnBuffer = 10;

    /// <summary>
    /// This varliable is for internal use, it shows what is the max index to choose from when selecting obstacles.
    /// </summary>
	int maxAvailable;
	int lastSpawnedBlock = -1;

    [HideInInspector]
	public List<GameObject> previous;

	// Use this for initialization
	void SpawnRow ( int i) {
		SpawnEnemy(i, maxAvailable);
		SpawnLateral(i);
		
		if (i > rowIndexToSpawnEnemies)
		{
			SpawnEnemy(i, 0);
		}
		
		if (i >= rowIndexToSpawnNewEnemies)
		{
			SpawnEnemy(i, maxAvailable);
		}
		
		SpawnPowerUps(i);

        for (int index = 0; index < rowIndexesForNewEnemies.Length; index++)
        {
            if (rowIndexesForNewEnemies[index] == i)
            {
                maxAvailable = index + 1;
            }
        }
	}

	public void SpawnBlock( int index)
	{
		if (lastSpawnedBlock < index)
		{

			List<GameObject> remove = new List<GameObject>();
			foreach( GameObject obj in previous)
			{
				if (obj == null || obj.transform.position.y <= index -20)
				{
					remove.Add(obj);
					GameObject.Destroy(obj);
				}
			}

			foreach(GameObject obj in remove)
			{
				previous.Remove(obj);
			}

			remove.Clear();

			if (index > 20)
			{
				for (int i = -(wallPosition-1); i < (wallPosition-1); i++)
				{
					GameObject enemy = GameObject.Instantiate(  obstacles[0], new Vector3(i, index - 20, 0), Quaternion.identity) as GameObject;
					enemy.transform.parent = transform;
					previous.Add(enemy);

				}
			}


			for (int i = index ; i < index + obstacleSpawnBuffer; i++)
			{
				SpawnRow(i);
			}

			lastSpawnedBlock = index;
		}

	}

	public void SpawnEnemy(int height, int maxEnemy)
	{
		GameObject enemy = GameObject.Instantiate( obstacles[Random.Range(0, maxEnemy)], new Vector3( Random.Range( -15, 15), height, 0), Quaternion.identity) as GameObject;
		enemy.transform.parent = transform;
		previous.Add(enemy);
	}

	public void SpawnLateral(int height)
	{
		GameObject enemy = GameObject.Instantiate( obstacles[0], new Vector3( -wallPosition, height, 0), Quaternion.identity) as GameObject;
		enemy.transform.parent = transform;
        enemy = GameObject.Instantiate(obstacles[0], new Vector3(wallPosition, height, 0), Quaternion.identity) as GameObject;
		enemy.transform.parent = transform;
		previous.Add(enemy);
	}

	public void SpawnPowerUps(int height)
	{
		if (Random.value <= powerUpsProbabilities[0])
		{
			GameObject pu = GameObject.Instantiate( powerUps[0], new Vector3( Random.Range( -5, 5), height, 0), Quaternion.identity) as GameObject;
			previous.Add(pu);
		}
        if (Random.value <= powerUpsProbabilities[1])
		{
            GameObject pu = GameObject.Instantiate(powerUps[1], new Vector3(Random.Range(-10, 10), height, 0), Quaternion.identity) as GameObject;
			previous.Add(pu);
		}

        if (Random.value <= powerUpsProbabilities[2])
        {
            GameObject astr = GameObject.Instantiate(powerUps[2], new Vector3(Random.Range(-8, 8), height, 0), Quaternion.identity) as GameObject;
            previous.Add(astr);
        }
	}

    public void DestroyObstaclesAt( Vector3 position)
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject ob in previous)
        {
            if (ob)
            {
                if ((ob.transform.position - position).magnitude < 10)
                {
                    toRemove.Add(ob);
                }
            }
        }

        foreach (GameObject ob in toRemove)
        {
            if (ob)
            {
                previous.Remove(ob);
                GameObject.DestroyImmediate(ob);
            }
        }
    }

}
