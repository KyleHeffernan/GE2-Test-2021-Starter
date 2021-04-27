using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPlayer : State
{
    public override void Enter()
    {
        owner.GetComponent<Seek>().targetGameObject = owner.GetComponent<Boid>().player;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<Boid>().player.transform.position,
            owner.transform.position) < 10)
        {
            

            owner.GetComponent<Boid>().ball.transform.parent = null;
            //Vector3 add1 = new Vector3(1,0,0);
            //owner.GetComponent<Boid>().ball.transform.position = owner.GetComponent<Boid>().ball.transform.position + add1;
            
            owner.ChangeState(new LookAtPlayer());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }

}


public class LookAtPlayer : State
{
        public override void Enter()
    {
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<Boid>().velocity = Vector3.zero;
        owner.GetComponent<Boid>().acceleration = Vector3.zero;
        
    }
    
    public override void Think()
    {
        Vector3 dist = owner.GetComponent<Boid>().player.transform.position - owner.transform.position;

        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.LookRotation(dist), Time.deltaTime );

        if (Vector3.Distance(
            owner.GetComponent<Boid>().ball.transform.position,
            owner.GetComponent<Boid>().player.transform.position) > 10)
        {
            owner.ChangeState(new GoToBall());
        }
    }
    
    public override void Exit()
    {
        //owner.GetComponent<Seek>().enabled = true;
    }

}

public class GoToBall : State
{
    public override void Enter()
    {
        AudioSource audio = owner.GetComponent<AudioSource>();
        audio.Play();



        owner.GetComponent<Seek>().targetGameObject = owner.GetComponent<Boid>().ball;
        owner.GetComponent<Seek>().enabled = true;

    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<Boid>().ball.transform.position,
            owner.transform.position) < 1)
        {
            owner.ChangeState(new GoToPlayer());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<Boid>().ball.transform.parent = owner.transform;
        owner.GetComponent<Boid>().ball.transform.position = owner.GetComponent<Boid>().attachPoint.position;
    }

}


