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
        Transform spot;

        switch (_spotType)
        {
            case SpotType.Entrance:
                spots = bb.targetBuilding.EntranceSpots.Count - 1;
                spot = bb.targetBuilding.EntranceSpots[Random.Range(0, spots)];
                break;
            case SpotType.Walk:
                spots = bb.targetBuilding.BuySpots.Count - 1;
                spot = bb.targetBuilding.BuySpots[Random.Range(0, spots)];
                break;
            case SpotType.Work:
                spots = bb.targetBuilding.WorkSpots.Count - 1;
                spot = bb.targetBuilding.WorkSpots[Random.Range(0, spots)];
                break;
            case SpotType.Delivery:
                spots = bb.targetBuilding.DeliverySpots.Count - 1;
                spot = bb.targetBuilding.DeliverySpots[Random.Range(0, spots)];
                break;
            default:
                spot = null;
                break;
        }

        this.bb.targetPosition = spot;
        this.controller.FinishWithSuccess();
    }
}
