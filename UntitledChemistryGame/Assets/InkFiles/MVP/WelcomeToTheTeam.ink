# speaker Louis
Hey Sully, could I talk to you for a moment?
    
    # speaker Sully
    * [Sure!] Yeah, what's up?
        # speaker Louis
        I'm Louis, the head coach of the Anglers. You've really shown some skill these past couple days.
        -> skill
        
== skill ==
# speaker Sully
* [Thank you] Thank you, I appreciate it.
    # speaker Louis
    Listen, I think you have what it takes to be part of the Anglers. Have you ever been in 3rd division before?
    -> third_division
    
    
== third_division == 
# speaker Sully
* [No] No, never. I think the highest I've been before is 5th division.
    # speaker Louis
    Well, like I said, I think you have what it takes, but I need to know if you really want it. Are you interested in being on the Anglers? Are you going to put in the work, and be as good if not better than you've been today?
    -> are_you_ready
        
== are_you_ready ==
# speaker Sully
* [Yes, sir!] <>
    -> fake_confidence
# speaker Sully
* [I think I can keep this up] <>
    -> not_confident
* [I'm really not sure if I'm good enough] <>
    -> not_confident
* [Surely you have the wrong person] <>
    -> not_confident
    
== fake_confidence ==
# speaker Sully
Yes sir, I would love to be a part of the Anglers! I can keep it up, I won't disappoint you. Thank you so much!
    # speaker Louis
    Good to hear. Are your parents here today?
    # speaker Sully
    * [Yes] Yup, they are over on the other coast near the goal.
        # speaker Louis
        Great, can you get them over here with us? I'll tell you guys the next steps.
            # speaker Sully
            Awesome, will do
            -> END

== not_confident ==
\* You think about saying what you really think: You can't promise him anything, and you know that in some way, you are bound to disappoint this man. You will never be what he wants you to be, and there is nothing you can do about it. But, you know he just wants to hear you say yes...and you want to say it whether you believe it or not. \*
-> fake_confidence