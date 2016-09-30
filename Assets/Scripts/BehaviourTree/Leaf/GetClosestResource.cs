using UnityEngine;
using System.Collections;

public class GetClosestResource : LeafTask {
    public GetClosestResource(BlackBoard blackboard) : base(blackboard) {

    }

    public override void Start() {
        base.Start();

        this.bb.targetResource = null; //Set this to null initially

        this.bb.targetResource = FindClosestResource();
        this.bb.targetPosition = new Vector3();
        if (this.bb.targetResource) {
            var added = this.bb.targetResource.AddHarvester();
            this.bb.targetPosition = this.bb.targetResource.transform.position;
            if(added)
                this.controller.FinishWithSuccess();
        }else {
            this.controller.FinishWithFailure();
        }

    }

    Resource FindClosestResource() {
        var list = GameMainScript.resources;
        GameObject closest = null;
        GameObject closestAvailable = null;
        float closestDst = float.MaxValue;
        float closestAvailDst = float.MaxValue;

        list.ForEach((resource) => {
            var dst = Vector3.Distance(resource.transform.position, this.bb.myself.transform.position);
            if (closest == null || (dst <= closestDst && resource.CanAddHarvester())) {
                closestDst = dst;
                closest = resource.gameObject;
            }

            if (resource.CanAddHarvester() && dst <= closestAvailDst) {
                closestAvailable = resource.gameObject;
                closestAvailDst = dst;
            }
        });

        Resource closestResource;
        if (closest != null) closestResource = closest.GetComponent<Resource>(); else closestResource = null;

        Resource availableResource;
        if (closestAvailable != null) availableResource = closestAvailable.GetComponent<Resource>(); else availableResource = null;

        if (closestResource && closestResource.CanAddHarvester()) {
            return closestResource;
        } else if (availableResource) {
            return availableResource;
        }

        return null;
    }
}
