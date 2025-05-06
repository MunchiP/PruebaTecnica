// Desarrollo de código para Prueba Técnica - Generation
// Desarrolladora: Paula Amaya
// Fecha: 6 -  Mayo  - 2025



using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   
   // Array para los elementos
    public GameObject[] objetosPrefabs;
    public int poolSize; // Referncia específica al número de objetos existentes en el juego - 10 según requisito
    [SerializeField] List<GameObject> pooledObjets = new List<GameObject>(); // Almaceno los objetos instanciados usados o no

    // Espeficaciones de tiempos e intervalos
    public float spawnTimer = 0f; 
    public float spawnInteral = 0.2f;  // Aparecen los objetos cada 2 segundos según requisito

    // Límite del área donde aparecerán los obstáculos
    private float minX = -3.5f ;
    private float maxX = 7f ;
    private float minZ = -9.5f ;
    private float maxZ = -2.5f ;
    private float yPosition = 3f;

    // Lugar donde desaparecen los obstáculos
    public float desaparecenPuntoz = 9.8f;

    // Enlace con la interfaz para mostrar el contador de los elementos
    public TMP_Text contadorOjbetos;

    void Start()
    {
        // Inicializo la función de agregar a la piscina como elemento de entrada - el tamaño de la piscina
        AddToPool(poolSize);
    }

    void Update()
    { 

        // Parte del código para el contador
        contadorOjbetos.text = "Contador: " +pooledObjets.Count(obj => obj.activeInHierarchy);



        spawnTimer += Time.deltaTime;

        // Si la lista de objetos es menor que el tamaño de la piscina, entonces use el método de agregar un objeto
        if (pooledObjets.Count < poolSize)
        {
            AddToPool(poolSize - pooledObjets.Count);
        }

        if (spawnTimer >= spawnInteral)
        {
            spawnTimer = 0f;
            // Obtiene un objeto de la piscina
            GameObject temporal = FistDesative();

            if (temporal != null)
            {
                Vector3 posicionRandom = new Vector3(Random.Range(minX,maxX), yPosition, Random.Range(minZ,maxZ));
                 temporal.transform.position = posicionRandom;
                 temporal.SetActive(true);
            }
        }

        // Código que desaparece los objetos
        foreach (GameObject item in pooledObjets)
            {
                if (item.activeInHierarchy && item.transform.position.z > desaparecenPuntoz)
                {
                    item.SetActive(false);
                }
            }
    }

        // -M- método para la verificación y creación de un objeto en la Pool
        GameObject FistDesative()
        {
            // Recorre cada uno de los objetos  y verifica 
            foreach (GameObject item in pooledObjets)
            {
                // sí el item no está activado en la Herencia - Inspector
                if (!item.activeInHierarchy) 
                {
                    //Entonre regrese el item
                    return item; 
                }
            }
                 // En caso de que todos los elementos estén creados, entonces crea uno nuevo
    return null;
        }

        // -M- método de la Pool -- agrego como parámetro de entrada el tamaño de la Pool declararo arriva, intervenido en el inspector
        void AddToPool( int poolSize)
        {
            for (int i = 0; i < poolSize; i++) // Recorro mi array hasta el tamaño de la Pool
            {
                // Selecciona el prefab aleatorio del arreglo de objetosPrefab
                // (0, objetosPrefabs.Lenght) aquí es: "0", porque el arreglo asume donde empieza pero no donde termina
                int randomPrefab = Random.Range(0, objetosPrefabs.Length);
                // Toma ese prefab de la lista
                GameObject prefabs = objetosPrefabs[randomPrefab];
                // Lo instancia en la posición 0 y sin rotación
                GameObject objetoCreado = Instantiate(prefabs, Vector3.zero, Quaternion.identity);
                // Los crea pero los deja en falso papra después usarlos
                objetoCreado.SetActive(false);
                // Luego de crear el objeto, de manera random, de la lista e instanciarlo en la posición 
                // Los agrego a la piscina
                pooledObjets.Add(objetoCreado);
            }
        }

}
