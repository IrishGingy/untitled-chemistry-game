# speaker Sully
Here's the fish that you requested!
# speaker Louis
Oh, Sully, nice job.
# speaker Sully
Thanks! So, what's my next task? 
    # speaker Sully
    * [Xenosquid?] Maybe a little night fishing to find the slippery xenosquid?
    * [Ghostfin Shark?] Oh, how about the ghostfin shark? I've always wanted to see their ghost-like dorsal fin!
    * [Voidspire?] Maybe I should be looking for the voidspire? I think I've seen some notes around the island that might help me locate them.
    # speaker Louis
    - Sully, why don't you take this next week off? Take a rest week.
    # speaker Sully
    ** [A rest week?] But coach, how am I going to improve? Is there something I'm not doing well enough? I did all that you asked of me and I just want to catch more fish.
# speaker Louis
Yes, you did what I asked, but you just need to be more creative.
# speaker Sully
*** [I can be more creative!] Uh...yeah coach I can be more creative. I can work on trying to catch more unique fish and throw back the common ones.
    -> i_can
*** [More creative? How?] I'm not sure I know what you mean. How do I be more creative? Is there some way I can practice this?
    -> how
*** [Why didn't you ask that before?] \* You think about telling him he left no room to be creative and that what he was saying was ridiculous...but you can't. He's right, the fish you caught weren't special...you aren't special. \*
    -> how
    
    
== how ==
# speaker Louis
Try catching more unique fish. The more unique the better. <> -> ending

-> END

== i_can ==
# speaker Louis
That's the spirit! Trust me, you practice being creative and you'll really improve your angling skills. <> -> ending

== ending ==
For now though, have a good rest week. I really have to be going, great job today though.

-> END
