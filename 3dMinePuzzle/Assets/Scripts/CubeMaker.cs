using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cubepuzzle
{
    public class CubeMaker : MonoBehaviour
    {
        [SerializeField]
        private GameObject cubePrefab;

        private List<GameObject[]> cubeList = new List<GameObject[]>();

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
            GameObject parentGameObject = new GameObject();
            parentGameObject.name = "Cube Holder";

            // generate each column
            for (int k = 0; k < numberOfColumn; k++)
            {
                GameObject columnParent = new GameObject();
                columnParent.transform.position = Vector3.zero;
                columnParent.name = "Column" + k.ToString();
                columnParent.transform.parent = parentGameObject.transform;

                columnParent.SetActive(k < numberOfCubesPerRow);

                GameObject[] oneColumn = new GameObject[numberOfCubesPerRow * numberOfCubesPerRow];

                // generate each cube
                for (int i = 0; i < numberOfCubesPerRow; i++)
                {
                    for (int j = 0; j < numberOfCubesPerRow; j++)
                    {
                        columnParent.transform.position = new Vector3(0, -k + numberOfCubesPerRow - 0.5f, 0);

                        GameObject cube = Instantiate(cubePrefab);
                        cube.transform.parent = columnParent.transform;
                        cube.transform.localPosition = new Vector3(i - devideByHalf + rest, 0, j - devideByHalf + rest);

                        Debug.Log("i : " + i + " truncate : " + devideByHalf + " rest : " + rest + " hm : " + (i - devideByHalf + rest));
                        oneColumn[i * 3 + j] = cube;

                        cubeList.Add(oneColumn);
                    }
                }
            }
        }

        internal void MoveDown()
        {
            
        }
    }
}