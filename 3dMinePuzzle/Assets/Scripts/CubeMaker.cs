using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cubepuzzle
{
    public class CubeMaker : MonoBehaviour, ICubeMaker
    {
        [SerializeField]
        private GameObject cube01;        
        [SerializeField]
        private GameObject goalCubePrefab;
        [SerializeField]
        private GameObject[] cubeList;
        
        private List<GameObject> columnParentObj = new List<GameObject>();

        private int[] numberOfBomb;
        private GameObject parentGameObject;
        private int currentLevel = 0;

        [SerializeField]
        private int numberOfCubesPerRow;

        [SerializeField]
        private int numberOfColumn;

        private float devideByHalf;
        private float rest;

        public float Min { get; set; }

        public int NumberOfCubesPerRow
        {
            get { return numberOfCubesPerRow; }
        }

        public int NumberOfColumn
        {
            get { return numberOfColumn; }
        }
        
        void Awake()
        {
            devideByHalf = (float)numberOfCubesPerRow / 2f;
            rest = devideByHalf - (int)devideByHalf;
            Min = -devideByHalf + rest;

            GenerateCubes();
        }

        public Vector3 StartingPosition()
        {
            return new Vector3((numberOfCubesPerRow - devideByHalf + rest - 1), numberOfCubesPerRow, (numberOfCubesPerRow - devideByHalf + rest - 1));
        }

        // generate cubes as a map
        private void GenerateCubes()
        {
            parentGameObject = new GameObject();
            parentGameObject.name = "Cube Holder";
            var numberOfCubes = numberOfColumn * numberOfCubesPerRow * numberOfCubesPerRow;
            cubeList = new GameObject[numberOfCubes];
            numberOfBomb = new int[numberOfCubes];

            // generate each column
            for (int k = 0; k < numberOfColumn; k++)
            {
                GameObject columnParent = new GameObject();
                columnParent.transform.position = Vector3.zero;
                columnParent.name = "Column" + k.ToString();
                columnParent.transform.parent = parentGameObject.transform;

                columnParentObj.Add(columnParent);

                columnParent.SetActive(k < numberOfCubesPerRow);

                // generate each cube
                for (int i = 0; i < numberOfCubesPerRow; i++)
                {
                    for (int j = 0; j < numberOfCubesPerRow; j++)
                    {
                        columnParent.transform.position = new Vector3(0, -k + numberOfCubesPerRow - 0.5f, 0);

                        GameObject cube;

                        if (k + 1 == numberOfColumn && i == 0 & j == 0)
                        {
                            cube = Instantiate(goalCubePrefab);
                        }
                        else if (UnityEngine.Random.value > 0.1f || (k == 0 && i + 1 == numberOfCubesPerRow && j + 1 == numberOfCubesPerRow))
                        {
                            cube = Instantiate(cube01);
                        }
                        else
                        {
                            cube = Instantiate(cube01);
                            cube.AddComponent<LosingCube>();
                            if (j + 1 < numberOfCubesPerRow)
                                numberOfBomb[(k * numberOfCubesPerRow * numberOfCubesPerRow) + (i * numberOfCubesPerRow) + j + 1] += 1;
                            if (j > 0)
                                numberOfBomb[(k * numberOfCubesPerRow * numberOfCubesPerRow) + (i * numberOfCubesPerRow) + j - 1] += 1;
                            if (i + 1 < numberOfCubesPerRow)
                                numberOfBomb[(k * numberOfCubesPerRow * numberOfCubesPerRow) + ((i + 1) * numberOfCubesPerRow) + j] += 1;
                            if (i > 0)
                                numberOfBomb[(k * numberOfCubesPerRow * numberOfCubesPerRow) + ((i - 1) * numberOfCubesPerRow) + j] += 1;
                            if (k > 0)
                            {
                                numberOfBomb[((k - 1) * numberOfCubesPerRow * numberOfCubesPerRow) + (i * numberOfCubesPerRow) + j] += 1;
                            }
                            if (k + 1 < numberOfColumn)
                            {
                                numberOfBomb[((k + 1) * numberOfCubesPerRow * numberOfCubesPerRow) + (i * numberOfCubesPerRow) + j] += 1;
                            }
                        }
                        cube.transform.parent = columnParent.transform;
                        cube.transform.localPosition = new Vector3(i - devideByHalf + rest, 0, j - devideByHalf + rest);

                        cubeList[(k * numberOfCubesPerRow * numberOfCubesPerRow) + (i * numberOfCubesPerRow) + j] = cube;
                    }
                }
            }

            VisualizeNumber();
        }

        private void VisualizeNumber()
        {
            for (int i = 0; i < cubeList.Length; i++)
            {
                if (i != ((numberOfColumn - 1) * numberOfCubesPerRow * numberOfCubesPerRow))
                    cubeList[i].GetComponentInChildren<Text>().text = numberOfBomb[i].ToString();
            }
        }

        public void MoveDown()
        {
            if (currentLevel < columnParentObj.Count - 1)
            {
                StartCoroutine(MoveCubesDown());
                UpdateVisualization();

                currentLevel++;
            }
        }

        private IEnumerator MoveCubesDown()
        {
            for (float time = 0; time < 1; time += Time.deltaTime)
            {
                parentGameObject.transform.position = new Vector3(0, currentLevel + time, 0);
            }

            yield return null;
        }

        private void UpdateVisualization()
        {
            columnParentObj[currentLevel].SetActive(false);
            columnParentObj[Mathf.Clamp(currentLevel + 3, 0, numberOfColumn - 1)].SetActive(true);
        }

        public void UpdateRevealedCubes(int pos)
        {
            if (numberOfBomb[pos] == 0)
            {
                var posY = Mathf.FloorToInt(pos / numberOfColumn);
                var posX = Mathf.FloorToInt((pos - (posY * numberOfCubesPerRow * numberOfCubesPerRow)) / numberOfCubesPerRow);
                var posZ = pos - (posY * numberOfCubesPerRow * numberOfCubesPerRow) - (posX * numberOfCubesPerRow);

                if (posZ + 1 < numberOfCubesPerRow)
                    cubeList[pos + 1].GetComponent<OnTriggerEnterColorChange>().ColorChange();
                if (posZ > 0)
                    cubeList[pos - 1].GetComponent<OnTriggerEnterColorChange>().ColorChange();

                if (posX + 1 < numberOfCubesPerRow)
                    cubeList[pos + numberOfCubesPerRow].GetComponent<OnTriggerEnterColorChange>().ColorChange();
                if (posX > 0)
                    cubeList[pos - numberOfCubesPerRow].GetComponent<OnTriggerEnterColorChange>().ColorChange();
                if (posY > 0)
                {
                    cubeList[pos - (numberOfCubesPerRow * numberOfCubesPerRow)].GetComponent<OnTriggerEnterColorChange>().ColorChange();
                }
                if (posY + 1 < numberOfColumn)
                {
                    cubeList[pos + (numberOfCubesPerRow * numberOfCubesPerRow)].GetComponent<OnTriggerEnterColorChange>().ColorChange();
                }
            }
        }
    }
}