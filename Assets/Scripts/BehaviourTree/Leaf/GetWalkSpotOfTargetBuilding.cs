using UnityEngine;
using System.Collections;
using Assets.Scripts.Util;

/// <summary>
/// Gets the spot of a building and sets it as the blackboard.targetPosition
/// </summary>
public class GetSpotOfBuilding : LeafTask {
    private readonly SpotType _spotType;

    /// <summary>
    /// Gets the spot of a building (entrace, walk, work, etc) and sets that spot as the target position in the blackboard
    /// </summary>
    /// <param name="blackboard"></param>
    /// <param name="spotType"></param>
    public GetSpotOfBuilding(BlackBoard blackboard, SpotType spotType) : base(blackboard) {
        this._spotType = spotType;
    }

    public override void Start() {
        base.Start();
        int spots;
        Vector3 spot;

        switch (_spotType)
        {
            case SpotType.Entrance:
                spots = bb.targetBuilding.entranceSpots.Count - 1;
                spot = bb.targetBuilding.entranceSpots[Random.Range(0, spots)].position;
                break;
            case SpotType.Walk:
                spots = bb.targetBuilding.walkSpots.Count - 1;
                spot = bb.targetBuilding.walkSpots[Random.Range(0, spots)].position;
                break;
            case SpotType.Work:
                spots = bb.targetBuilding.workSpots.Count - 1;
                spot = bb.targetBuilding.workSpots[Random.Range(0, spots)].position;
                break;
            case SpotType.Delivery:
                spots = bb.targetBuilding.deliverySpots.Count - 1;
                spot = bb.targetBuilding.deliverySpots[Random.Range(0, spots)].position;
                break;
            default:
                spot = Vector3.zero;
                break;
        }

        this.bb.targetPosition = spot;
        this.controller.FinishWithSuccess();
    }
}
