EXTERNAL startQuest(questID)
-> main

=== main ===
Hey dude, I've got a job for you. Collect five coins and then talk to my brother south of here.
Do you wish to start the Quest?
     + [Accept]
      ~ startQuest("CollectCoinsQuest")
        -> chosen("Accept")
       
    + [Decline]
        -> chosen("Decline")
    
      === chosen(Quest) ===
You chose {Quest}
-> END  
        


