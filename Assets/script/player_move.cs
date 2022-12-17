using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    public float walk_speed;
    public float run_speed;
    float apply_speed;

    public float jump_force;

    public float look_sensitivity;
    //camera limit
    public float camera_limit_x;
    public float cur_camera_limit_x;

    bool is_run;
    bool is_ground;
    bool is_ladder;

    public Camera _camera;

    Rigidbody rigid;
    CapsuleCollider capsule;
    game_manager manager;
    void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
        manager = FindObjectOfType<game_manager>();
        rigid = GetComponent<Rigidbody>();
        apply_speed = walk_speed;
    }

    
    void Update()
    {
        if (manager.is_menu_show)
        {
            return;
        }
        camere_rotation();
        character_rotation();
        do_run();
        do_jump();
    }

    void FixedUpdate()
    {
        if (manager.is_menu_show)
        {
            return;
        }
        p_move();
        ladder_move();
    }
    void p_move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move_horizontal = transform.right * x;
        Vector3 move_vertical = transform.forward * z;

        Vector3 velocity = (move_horizontal + move_vertical).normalized * apply_speed;
        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
    void camere_rotation()
    {
        float x_rotation = Input.GetAxisRaw("Mouse Y") * look_sensitivity;
        cur_camera_limit_x -= x_rotation;
        cur_camera_limit_x = Mathf.Clamp(cur_camera_limit_x, camera_limit_x * -1, camera_limit_x);

        _camera.transform.localEulerAngles = new Vector3(cur_camera_limit_x, 0, 0);
    }
    void character_rotation()
    {
        float y_rotation = Input.GetAxisRaw("Mouse X");

        Vector3 character_rotation_y = new Vector3(0, y_rotation, 0) * look_sensitivity;

        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(character_rotation_y));
    }

    void do_run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running_cancel();
        }
    }
    void running()
    {
        is_run = true;
        apply_speed = run_speed;
    }
    void running_cancel()
    {
        is_run = false;
        apply_speed = walk_speed;
    }

    void do_jump()
    {
        is_ground = Physics.Raycast(transform.position, Vector3.down, capsule.bounds.extents.y + 0.1f);
        if (Input.GetButtonDown("Jump") && is_ground)
        {
            rigid.velocity = transform.up * jump_force;
        }
    }

    void ladder_move()
    {
        if (is_ladder)
        {
            rigid.useGravity = false;
            bool upKey = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
            bool downKey = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
            if (upKey)
            {
                gameObject.transform.Translate(0, walk_speed * Time.deltaTime, 0);
            }
            else if (downKey && !is_ground)
            {
                gameObject.transform.Translate(0, -walk_speed * Time.deltaTime, 0);

            }
        }
        else
        {
            rigid.useGravity = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ladder")
        {
            is_ladder = true;
            Debug.Log("ladder");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            is_ladder = false;
            Debug.Log("no_ladder");
        }
    }
}
