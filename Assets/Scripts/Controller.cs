using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDriveController {
    Rigidbody rb;
    GameObject robot;

    private float _eps = 0.1f;
    public ForceDriveController(GameObject robot) {
        this.robot = robot;
        this.rb = robot.GetComponent<Rigidbody>();
    }

    public void Controller(Vector3 target) {
        Vector3 target_ = target;
        Vector3 coord = new Vector3(robot.transform.localPosition.x, 0, robot.transform.localPosition.z);
        Vector3 direction = target_ - coord;
        float distance = Vector3.Distance(target_, coord);

        if (distance < _eps) {
            rb.velocity = Vector3.zero;
            return;
        }


        rb.velocity = direction.normalized * 5;
    }
}