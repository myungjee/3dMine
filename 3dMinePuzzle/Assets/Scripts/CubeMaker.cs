using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cubepuzzle
{
    public class CubeMaker : MonoBehaviour
    {
        [SerializeField]
        private GameObject cubePrefab;
        private GameObject parentGameObject;

        private List<GameObject> cubeList = new List<GameObject>();

        [SerializeField]
        private int numberOfCubesPerRow;
        [SerializeField]
        private int numberOfColumn;

        // Start is called before the first frame update
        void Start()
        {
            parentGameObject = new GameObject();

            for (int i = 0; i < numberOfCubesPerRow; i++)
            {
                for (int j = 0; j < numberOfCubesPerRow; j++)
                {
                    for (int k = 0; k < numberOfColumn; k++)
                    {
                        GameObject cube = Instantiate(cubePrefab);
                        cube.transform.position = new Vector3(0.5f * (i - 1), 0.5f * k, 0.5f * (j - 1));
                        cube.transform.parent = parentGameObject.transform;
                        cubeList.Add(cube);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}