using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Generator : MonoBehaviour
{	
	//x<=10 || x>=-10
	//z<= 11.5|| z>=1.5
	//y=59.5
	[SerializeField] private float minX;
	[SerializeField] private float maxX;
	[SerializeField] private float minZ;
	[SerializeField] private float maxZ;
	[SerializeField] private int numCols;
	[SerializeField] private int numRows;

	private GameObject[] obstacles;
    private float difX;
    private float difZ;
    private Vector3 [,] posObstacles;
    private bool [,] occupiedPos;
    private int maxObjects, iniObjects;

    void Start()
    {
    	//Inicialización del array de Obstáculos
    	obstacles = Resources.LoadAll<GameObject> ("Obstacles");

    	//Número máximo de objetos y número inicial de objetos
    	maxObjects=numCols*numRows;
    	iniObjects=(int)Mathf.Floor((float)0.65*maxObjects);
    	
    	//Inicialización de la matriz a las posiciones establecidas
    	difX = (maxX-minX)/numCols;
    	difZ = (maxZ-minZ)/numRows;

    	posObstacles=new Vector3[numRows,numCols];
    	occupiedPos=new bool[numRows,numCols];

    	float Xaux;
    	float Zaux = minZ+(difZ/2);

    	for(int i=0; i < numRows; i++)
    	{
    		Xaux=minX+(difX/2);

    		for(int j=0; j < numCols; j++)
    		{
    			posObstacles[i,j] = new Vector3(Xaux,-59.5f,Zaux);
    			occupiedPos[i,j] = false;
    			Xaux+=difX;
    		}

    		Zaux+=difZ;
    	}

    	//Instanciando los objetos iniciales
    	int ranCol;
    	int ranRow;
    	int ranObject;
    	Vector3 auxV;
    	float ranDifX,ranDifZ;
    	int sumrest;

    	for(int z=0; z < iniObjects; z++)
    	{
    		while(true)
    		{
    			ranRow = Random.Range(0,numRows);
    			ranCol = Random.Range(0,numCols);

    			if(!occupiedPos[ranRow,ranCol])
    			{
    				occupiedPos[ranRow,ranCol]=true;
    				break;
    			}
    		}

    		ranObject=Random.Range(0,obstacles.Length);

    		auxV=posObstacles[ranRow,ranCol];

    		ranDifX=Random.Range(0f,1f);
    		sumrest=Random.Range(0,2);
    		if(sumrest==1){auxV.x+=ranDifX;}
    		else{auxV.x-=ranDifX;}

    		ranDifZ=Random.Range(0f,1f);
    		sumrest=Random.Range(0,2);
    		if(sumrest==1){auxV.z+=ranDifZ;}
    		else{auxV.z-=ranDifZ;}

    		Instantiate(obstacles[ranObject].transform,auxV, Quaternion.identity);
       	}
    }

    void Update()
    {
        
    }
}
