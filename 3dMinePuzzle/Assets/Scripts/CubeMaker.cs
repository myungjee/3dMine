using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cubepuzzle
{
    public class CubeMaker : MonoBehaviour
    {
        [SerializeField]
        private GameObject normalCubePrefab;
        [SerializeField]
        private GameObject bombCubePrefab;
        [SerializeField]
        private GameObject goalCubePrefab;

        private List<GameObject> columnParentObj = new List<GameObject>();

        private List<GameObject[]> cubeList = new List<GameObject[]>();
        private GameObject parentGameObject;
        private int currentLevel = 0;

        [SerializeField]
        private int numberOfCubesPerRow;

        [SerializeField]
        private int numberOfColumn;

        private float devideByHalf;
        private float rest;

        public float Min;
        
        void Awake()
        {
            devideByHalf = (float)numberOfCubesPerRow / 2f;
            rest = devideByHalf - (int)devideByHalf;
            Min = -devideByHalf + rest;

            GenerateCubes();
        }

        internal Vector3 StartingPosition()
        {
            return new Vector3((numberOfCubesPerRow - devideByHalf + rest - 1), numberOfCubesPerRow, (numberOfCubesPerRow - devideByHalf + rest - 1));
        }

        // generate cubes as a map
        private void GenerateCubes()
        {
            parentGameObject = new GameObject();
            parentGameObject.name = "Cube Holder";

            // generate each column
            for (int k = 0; k < numberOfColumn; k++)
            {
                GameObject columnParent = new GameObject();
                columnParent.transform.position = Vector3.zero;
                columnParent.name = "Column" + k.ToString();
                columnParent.transform.parent = parentGameObject.transform;

                columnParentObj.Add(columnParent);

                columnParent.SetActive(k < numberOfCubesPerRow);

                GameObject[] oneColumn = new GameObject[numberOfCubesPerRow * numberOfCubesPerRow];

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
                        else if (UnityEngine.Random.value > 0.2f || (k == 0 && i + 1 == numberOfCubesPerRow && j + 1 == numberOfCubesPerRow))
                        {
                            cube = Instantiate(normalCubePrefab);
                        }
                        else
                        {
                            cube = Instantiate(bombCubePrefab);
                        }
                        cube.transform.parent = columnParent.transform;
                        cube.transform.localPosition = new Vector3(i - devideByHalf + rest, 0, j - devideByHalf + rest);
                        
                        oneColumn[i * 3 + j] = cube;

                        cubeList.Add(oneColumn);
                    }
                }
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
    }
}