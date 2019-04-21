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
    [SerializeField] private float randomSumRest;
    [SerializeField] private GameObject rightChest;
    [SerializeField] private GameObject leftChest;
    [SerializeField] private float timeOutPowerUp;

    private float timer;
    private int[] numObjRow;
    public GameObject[] obstacles;
    public GameObject[] powerUps;
    private float difX;
    private float difZ;
    private Vector3[,] posObstacles;
    private bool[,] occupiedPos;
    private int maxObjects, iniObjects;
    private int numObjects;
    private bool inuse = false;
    private float spawnTimeSuddenDeath;
    private bool isSuddenDeath = false;

    //private int[] numObject;

    private int numObjHL = 0;
    private int numObjHR = 0;


    void Start()
    {
        Messenger<int, int>.AddListener(GameEvent.ROW_COL_OC, changeMatBool);
        Messenger.AddListener(GameEvent.SUDDEN_DEATH, suddenDeath);
        Messenger<int, int>.AddListener(GameEvent.ROW_COL_PU, freeMatrix);

        numObjects = 0;
        //maxObjects=numCols*numRows;
        //iniObjects=(int)Mathf.Floor((float)0.5*maxObjects);
        maxObjects = iniObjects = numRows * 2;
        //Inicialización de la matriz a las posiciones establecidas
        difX = (maxX - minX) / numCols;
        difZ = (maxZ - minZ) / numRows;

        numObjRow = new int[numRows];
        posObstacles = new Vector3[numRows, numCols];
        occupiedPos = new bool[numRows, numCols];

        float Xaux;
        float Zaux = minZ + (difZ / 2);

        for (int i = 0; i < numRows; i++)
        {
            Xaux = minX + (difX / 2);

            for (int j = 0; j < numCols; j++)
            {
                posObstacles[i, j] = new Vector3(Xaux, -40f, Zaux);
                occupiedPos[i, j] = false;
                Xaux += difX;
            }

            Zaux += difZ;
        }

        for (int z = 0; z < iniObjects; z++)
        {
            if (z == 0)
            {
                instantiateChest(true);
            }
            else if (z == 1)
            {
                instantiateChest(false);
            }
            else { instantiateObstacles(); }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void instantiateObstacles()
    {
        //Instanciando los objetos iniciales
        int ranCol = -1;
        int ranRow = -1;
        int ranObject;
        Vector3 auxV;
        float ranDifX, ranDifZ;
        int sumrest;
        int ranNum;
        int i = 0;
        GameObject instancia;
        bool half = true;

        if (numObjHR <= numObjHL)
        {
            half = true;
        }
        else if (numObjHL < numObjHR)
        {
            half = false;
        }

        if (timer >= timeOutPowerUp)
        {

            for (int k = 0; k < numRows; k++)
            {
                if (numObjRow[k] <= 1)
                {    
                    
                    ranCol = Random.Range(Mathf.RoundToInt(numCols / 2) - 1, Mathf.RoundToInt(numCols / 2) + 1);
                    while(occupiedPos[k, ranCol])
                    {
                        ranCol = Random.Range(Mathf.RoundToInt(numCols / 2) - 1, Mathf.RoundToInt(numCols / 2) + 1);
                    }
                    occupiedPos[k, ranCol] = true;
                    numObjRow[k] += 1;
                    auxV = posObstacles[k, ranCol];
                    auxV.y = -59.5f;
                    ranObject = Random.Range(0, powerUps.Length);
                    instancia = Instantiate<GameObject>(powerUps[ranObject], auxV, powerUps[ranObject].transform.rotation);
                    instancia.transform.GetChild(0).GetComponent<PowerUp>().setRow(k);
                    instancia.transform.GetChild(0).GetComponent<PowerUp>().setCol(ranCol);
                    timer = 0f;
                    numObjects += 1;
                    break;
                }
            }
        }
        else
        {
            while (i < 2000)
            {
                if (half == true)
                {
                    ranRow = Random.Range(0, numRows);
                    ranCol = Random.Range(0, Mathf.RoundToInt(numCols / 2));
                }
                else if (half == false)
                {
                    ranRow = Random.Range(0, numRows);
                    ranCol = Random.Range(Mathf.RoundToInt(numCols / 2), numCols);
                }


                if (!occupiedPos[ranRow, ranCol] && numObjRow[ranRow] <= 1)
                {
                    numObjRow[ranRow] += 1;
                    occupiedPos[ranRow, ranCol] = true;
                    break;
                }
                i++;
            }

            if (ranCol < Mathf.RoundToInt(numCols / 2))
            {
                numObjHR++;
            }
            else if (ranCol >= Mathf.RoundToInt(numCols / 2))
            {
                numObjHL++;
            }

            // Si no se inicializa la variable a algún valor fuera de un if tira error
            ranObject = 0;

            // Simular probabilidad
            ranNum = Random.Range(0, 100);

            // Aqui con ifs se decide la probabilidad de cada objet
            auxV = posObstacles[ranRow, ranCol];

            if (ranNum >= 70)//Spinner
            {
                ranObject = 1;
                auxV.y = -61.38f;
            }
            else if (ranNum < 40)//Barril
            {
                ranObject = 0;
                auxV.y = -59.864f;
            }
            else if (ranNum >= 40 && ranNum < 70)//Redirectioner
            {
                ranObject = 2;
                auxV.y = -60f;
            }

            if (i != 2000)
            {
                ranDifX = Random.Range(0f, randomSumRest);
                sumrest = Random.Range(0, 2);
                if (sumrest == 1) { auxV.x += ranDifX; }
                else { auxV.x -= ranDifX; }

                ranDifZ = Random.Range(0f, randomSumRest);
                sumrest = Random.Range(0, 2);
                if (sumrest == 1) { auxV.z += ranDifZ; }
                else { auxV.z -= ranDifZ; }

                if (ranCol != -1)
                {

                    instancia = Instantiate<GameObject>(obstacles[ranObject], auxV, obstacles[ranObject].transform.rotation);

                    if (ranNum < 40)
                    {
                        instancia.transform.GetChild(0).gameObject.GetComponent<BulletDivider>().setRow(ranRow);
                        instancia.transform.GetChild(0).gameObject.GetComponent<BulletDivider>().setCol(ranCol);
                    }
                    else if (ranNum >= 70)
                    {
                        instancia.transform.GetChild(0).gameObject.GetComponent<BulletSpinner>().setRow(ranRow);
                        instancia.transform.GetChild(0).gameObject.GetComponent<BulletSpinner>().setCol(ranCol);
                    }
                    else if (ranNum >= 40 && ranNum < 70)
                    {
                        instancia.GetComponent<BulletRedirectioner>().setRow(ranRow);
                        instancia.GetComponent<BulletRedirectioner>().setCol(ranCol);
                    }
                    numObjects += 1;
                }

            }
        }

    }

    void instantiateChest(bool right)
    {
        int ranRow = Random.Range(0, numRows);
        Vector3 auxV;
        float ranDifX, ranDifZ;
        int sumrest;
        numObjRow[ranRow] += 1;

        if (right)
        {
            int ranCol = numCols - 1;
            auxV = posObstacles[ranRow, ranCol];
            auxV.y = -60f;
            occupiedPos[ranRow, ranCol] = true;

            ranDifX = Random.Range(0f, randomSumRest);
            sumrest = Random.Range(0, 2);
            if (sumrest == 1) { auxV.x += ranDifX; }
            else { auxV.x -= ranDifX; }

            ranDifZ = Random.Range(0f, randomSumRest);
            sumrest = Random.Range(0, 2);
            if (sumrest == 1) { auxV.z += ranDifZ; }
            else { auxV.z -= ranDifZ; }

            rightChest.transform.position = auxV;

            if (ranCol != -1)
            {
                Instantiate(rightChest.transform, auxV, rightChest.transform.rotation);
                numObjects += 1;
                numObjHR += 1;
            }
        }
        else
        {
            int ranCol = 0;
            auxV = posObstacles[ranRow, ranCol];
            auxV.y = -60f;

            occupiedPos[ranRow, ranCol] = true;

            ranDifX = Random.Range(0f, randomSumRest);
            sumrest = Random.Range(0, 2);
            if (sumrest == 1) { auxV.x += ranDifX; }
            else { auxV.x -= ranDifX; }

            ranDifZ = Random.Range(0f, randomSumRest);
            sumrest = Random.Range(0, 2);
            if (sumrest == 1) { auxV.z += ranDifZ; }
            else { auxV.z -= ranDifZ; }

            leftChest.transform.position = auxV;

            if (ranCol != -1)
            {
                Instantiate(leftChest.transform, auxV, leftChest.transform.rotation);
                numObjects += 1;
                numObjHL += 1;
            }
        }
    }

    private void changeMatBool(int row, int col)
    {
        occupiedPos[row, col] = false;
        numObjRow[row] -= 1;
        if (col < Mathf.RoundToInt(numCols / 2))
        {
            numObjHR--;
        }
        else if (col >= Mathf.RoundToInt(numCols / 2))
        {
            numObjHL--;
        }
        numObjects -= 1;
        StartCoroutine(waitInstance());
    }

    void suddenDeath()
    {
        isSuddenDeath = true;
        spawnTimeSuddenDeath = 1f;
    }

    IEnumerator waitInstance()
    {
        //inuse=true;

        if (isSuddenDeath)
        {
            yield return new WaitForSeconds(spawnTimeSuddenDeath);
        }
        else
        {
            int rantime = Random.Range(2, 4);
            yield return new WaitForSeconds(rantime);
        }

        instantiateObstacles();
        //inuse=false;
    }

    private void OnDestroy()
    {
        Messenger<int, int>.RemoveListener(GameEvent.ROW_COL_OC, changeMatBool);
        Messenger.RemoveListener(GameEvent.SUDDEN_DEATH, suddenDeath);
        Messenger<int, int>.RemoveListener(GameEvent.ROW_COL_PU, freeMatrix);
    }

    private void freeMatrix(int row, int col)
    {
        occupiedPos[row, col] = false;
        numObjects -= 1;
        numObjRow[row] -= 1;
        StartCoroutine(waitInstance());
    }
}
