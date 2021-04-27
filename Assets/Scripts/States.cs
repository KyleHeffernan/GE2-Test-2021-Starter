using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State which makes dog move to the player. Once they are close, the ball is unparented and the dog is moved to the look at player state.
public class GoToPlayer : State
{
    public override void Enter()
    {
        owner.GetComponent<Seek>().targetGameObject = owner.GetComponent<Boid>().player;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        //Checking distance between dog and player
        if (Vector3.Distance(
            owner.GetComponent<Boid>().player.transform.position,
            owner.transform.position) < 10)
        {
            owner.GetComponent<Boid>().ball.transform.parent = null;
            owner.ChangeState(new LookAtPlayer());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }

}


//Makes the dog sit still and always face towards the player. Once the ball has moved a big distance from the player, the dog moves to the GoToBall state.
public class LookAtPlayer : State
{
        public override void Enter()
    {
        //Making dog sit still
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<Boid>().velocity = Vector3.zero;
        owner.GetComponent<Boid>().acceleration = Vector3.zero;
    }
    
    public override void Think()
    {
        //Making dog look at the player
        Vector3 dist = owner.GetComponent<Boid>().player.transform.position - owner.transform.position;
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.LookRotation(dist), Time.deltaTime );

        //Checking the distance between the player and the ball
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


//First plays a bark audio clip and moves the dog towards the ball. Once the dog is close, it moves the ball to the dogs mouth and moves to the GoToPlayer state.
public class GoToBall : State
{
    public override void Enter()
    {
        //Play bark clip
        AudioSource audio = owner.GetComponent<AudioSource>();
        audio.Play();

        //Set the ball to the target
        owner.GetComponent<Seek>().targetGameObject = owner.GetComponent<Boid>().ball;
        owner.GetComponent<Seek>().enabled = true;

    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<Boid>().ball.transform.position,
            owner.transform.position) < 1.2f)
        {
            owner.ChangeState(new GoToPlayer());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
        //Moving ball to the dogs mouth and making the ball a child object of the dogs mouth
        owner.GetComponent<Boid>().ball.transform.parent = owner.transform;
        owner.GetComponent<Boid>().ball.transform.position = owner.GetComponent<Boid>().attachPoint.position;
    }

}


