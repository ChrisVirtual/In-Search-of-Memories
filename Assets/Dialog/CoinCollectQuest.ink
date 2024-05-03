EXTERNAL startQuest(questID)
EXTERNAL checkCanStart(requiredLevel, questID)
EXTERNAL levelCheck(requiredLevel)
EXTERNAL completed(questID)

VAR canStart = false
VAR levelMet = false
VAR finished = false

-> main
=== main ===
~ finished = completed("CollectCoinsQuest")
~ levelMet = levelCheck(5)
~ canStart = checkCanStart(5, "CollectCoinsQuest")

{finished == true:
    - Thanks for your help earlier, The names bob btw
    -> END
}

{levelMet == false:
    - You are too low level to complete this quest. Go grab that purple exp orb and come back at level 5.
    -> END
}

{canStart == false:
    - How is the coin collecting going? Remember to talk to my brother to the west after you are done.
    -> END
}

{canStart == true and levelMet == true and finished == false:
    - Hey dude, I've got a job for you. Collect five coins and then talk to my brother west of here.
    Do you wish to start the Quest?
        + [Accept]
            ~ startQuest("CollectCoinsQuest")
                -> chosen("Accept")
        + [Decline]
            -> chosen("Decline")
}

=== chosen(Quest) ===
You chose {Quest}
-> END


