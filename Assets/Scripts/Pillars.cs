using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour
{
    [SerializeField] Transform objectToMove;
    [SerializeField] Transform[] points;
    int initial = 0;
    int final = 1;

    [SerializeField] AnimationCurve ease;
    [SerializeField] float animationDuration;
    float elapsedTime;

    void Start()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        while (true)
        {
            elapsedTime = 0;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;

                objectToMove.position = Vector3.LerpUnclamped(
                    points[initial].position,
                    points[final].position,
                    ease.Evaluate(elapsedTime / animationDuration)
                    );

                yield return null;

            }

            initial = final;
            final = (final + 1) % points.Length;

            yield return null;
        }
    }


}
