using UnityEngine;

public class MoveFront : MonoBehaviour
{
   // -V- Velocidad los objetos
    public float speed = 20f;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
