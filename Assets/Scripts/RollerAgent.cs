using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

public class RollerAgent : Agent {
    public Transform target; // targetのTransform
    Rigidbody rBody;
    ForceDriveController controller;
    Camera camera;

    public override void Initialize() {
        rBody = GetComponent<Rigidbody>();
        controller = new ForceDriveController(this.gameObject);
        camera = GameObject.Find("TopCamera").GetComponent<Camera>();
        camera.enabled = true;
    }
    public void Start() {
        camera.enabled = true;
    }

    public override void OnEpisodeBegin() {
        
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(0, 0.5f, 0);
        

        target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    //状態取得時に呼ばれる
    public override void CollectObservations(VectorSensor sensor) {

        // //targetの位置
        // sensor.AddObservation(target.localPosition.x);
        // sensor.AddObservation(target.localPosition.z);

        // //RollerAgentの位置と速度
        // sensor.AddObservation(this.transform.localPosition.x);
        // sensor.AddObservation(this.transform.localPosition.z);
        // sensor.AddObservation(rBody.velocity.x);
        // sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers) {
        //行動の取得
        var action = actionBuffers.DiscreteActions;

        //行動の実行
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = (float)action[0] - 5.0f;
        controlSignal.z = (float)action[1] - 5.0f;
        controller.Controller(controlSignal);
        

        //報酬の設定
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);

        if (distanceToTarget < 1.42f) {
            SetReward(1.0f);
            EndEpisode();
        }

        if (this.transform.localPosition.y < 0) {
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut) {
        // var continuousActionsOut = actionsOut.DiscreteActions;
        // continuousActionsOut[0] = Input.GetAxis("Horizontal");
        // continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
    
}