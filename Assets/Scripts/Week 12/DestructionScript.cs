using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionScript : MonoBehaviour
{
    public List<Transform> pieces;

    float totalDestroyTime = 5f;
    float destroyTimePerPiece;

    public AnimationCurve scaleCurve;
    public AnimationCurve yCurve;

    ParticleSystem particles;

    CinemachineImpulseSource impulseSource;

    public AudioClip explosion;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        destroyTimePerPiece = totalDestroyTime / pieces.Count;
        particles = GetComponent<ParticleSystem>();
        var main = particles.main;
        main.duration = totalDestroyTime;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        impulseSource.m_ImpulseDefinition.m_ImpulseDuration = totalDestroyTime;

        audioSource = GetComponent<AudioSource>();
    }

    public void Activate()
    {
        StartCoroutine(DestroyObject());
        impulseSource.GenerateImpulseWithForce(0.2f);
    }

    IEnumerator DestroyObject()
    {
        particles.Play();

        foreach (Transform piece in pieces)
        {
            particles.Emit(5);
            audioSource.PlayOneShot(explosion);
            yield return StartCoroutine(DestroyPiece(piece));
        }
        Destroy(gameObject);
    }

    IEnumerator DestroyPiece(Transform piece)
    {
        float xSpeed = Random.Range(-4f, 4f);

        float timer = 0f;

        while (timer < destroyTimePerPiece)
        {
            timer += Time.deltaTime;

            float destroyProgression = timer / destroyTimePerPiece;

            piece.localScale = Vector3.one * scaleCurve.Evaluate(destroyProgression);
            Vector3 pos = piece.position;
            pos.y += yCurve.Evaluate(destroyProgression) * Time.deltaTime;
            pos.x += xSpeed * Time.deltaTime;
            piece.position = pos;


            yield return null;
        }
    }
}
