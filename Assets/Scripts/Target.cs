using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    List<Transform> positions = new List<Transform>();
    int current = 0;

    private void Awake()
    {
        var objects = GameObject.FindGameObjectsWithTag("Pillar");

        for (int i = 0; i < objects.Length; i++)
        {
            positions.Add(objects[i].transform);
        }

        MoveToNext();
    }

    public void MoveToNext()
    {
        int previous = current;
        int randomIndex = previous;

        while (randomIndex == previous)
        {
            randomIndex = Random.Range(0, positions.Count);
        }

        current = randomIndex;
        //NO FUNCIONA NA
        // Ajusta la posición para que esté justo debajo del pilar
        Vector3 newPosition = positions[current].position;
        newPosition.y -= 1; // Aquí ajusta la cantidad que desees restar en el eje Y para que esté justo debajo

        transform.position = newPosition;
    }
}
