# Bob
-> main

=== main ===
Hey dude, I've got a job for you Collect five coins and then talk to my brother south of here.
    + [Accept]
        -> chosen("Accept")
    + [Decline]
        -> chosen("Decline")
        
=== chosen(CoinCollectQuest) ===
You chose {CoinCollectQuest}
-> END