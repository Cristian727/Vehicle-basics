using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float friction;
    public float frictionBrake;
    public float maxSpeed;
    public Rigidbody rb;
    float playerSpeed;
    [SerializeField] ParticleSystem trailParticles;
    [SerializeField] ParticleSystem trailParticlesLeft;
    [SerializeField] ParticleSystem trailParticlesRight;
    ParticleSystem.MainModule trailMainModule;
    bool particlesActives;
    Vector3 movementInput;
    public float rotationSpeed = 5;
    public TrailRenderer trailRenderer;


    Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Enable();
        controls.Movement.Enable();
        //controls.UI.disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void ActivateHandbrake()
    {
        rb.velocity = new Vector3(rb.velocity.x / (1 + frictionBrake * Time.deltaTime), rb.velocity.y, rb.velocity.z / (1 + frictionBrake * Time.deltaTime));
        trailRenderer.emitting = true;
    }

    void DeactivateHandbrake()
    {
        trailRenderer.emitting = false;
    }


    // Update is called once per frame
    void Update()
    {

        //if (controls.Movement.Handbreak.IsPressed())
            //print("a");
       // else
            //print("b");

        //print(controls.Movement.Turn.ReadValue<float>());




        //transform.forward
        movementInput = Vector3.zero;

        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        //{
        //    movementInput.z += 1;
        //}
        //if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        //{
        //    movementInput.z -=1;
        //}

        movementInput.x = controls.Movement.TurnStick.ReadValue<Vector2>().x != 0 ?
            controls.Movement.TurnStick.ReadValue<Vector2>().x
            :
            controls.Movement.Turn.ReadValue<float>();

        movementInput.z = controls.Movement.FrontBack.ReadValue<float>();

        if (controls.Movement.Handbreak.IsPressed())
        {
            ActivateHandbrake();
        }
        else
        {
            DeactivateHandbrake();
        }


        //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        //{
        //    movementInput.x += 1;
        //}
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        //{
        //    movementInput.x -= 1;
        //}

        //print(movementInput);


        //controls.Movement.Handbreak.IsPressed();




    }


    private void FixedUpdate()
    {
        

        if (movementInput.z > 0)
        {
            rb.velocity += transform.forward * acceleration * Time.deltaTime; //framerate-independent

            if (!particlesActives)
            {
                print("a");
                trailParticles.Play();
                particlesActives = true;
            }
            Vector3 movementInput = Vector3.zero;

        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x / (1 + friction * Time.deltaTime), rb.velocity.y, rb.velocity.z / (1 + friction * Time.deltaTime));
            if (particlesActives)
            {
                trailParticles.Stop();
                particlesActives = false;
            }
        }

        Vector3 tmpVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        playerSpeed = tmpVelocity.magnitude;

        if (playerSpeed > maxSpeed)
        {
            // pa darle a un vector una longitud concreta:
            // normalizar vector y multiplicarlo por esa longitud
            tmpVelocity = tmpVelocity.normalized * maxSpeed;

            rb.velocity = new Vector3(tmpVelocity.x, rb.velocity.y, tmpVelocity.z);
        }

        if (movementInput.x != 0)
        {
            float angularSpeed = movementInput.x * rotationSpeed;

            rb.angularVelocity = new Vector3(0, angularSpeed, 0);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
        //Partíclas derecha izquierda
        if (movementInput.x > 0)
        {
            trailParticlesLeft.Play();
        }
        else
        {
            trailParticlesLeft.Stop();
        }

        if (movementInput.x < 0)
        {
            trailParticlesRight.Play();
        } else
        {
            trailParticlesRight.Stop();
        }

        if (movementInput.z < 0)
        {
            if (transform.InverseTransformDirection(rb.velocity).z > 0)
            {
               rb.velocity = new Vector3(rb.velocity.x / (1 + frictionBrake * Time.deltaTime), rb.velocity.y, rb.velocity.z / (1 + friction * Time.deltaTime));
            } else
            {
               rb.velocity -= transform.forward * acceleration * Time.deltaTime;
            }
    
        }

    }
}
