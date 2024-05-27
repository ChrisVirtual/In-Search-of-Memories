EXTERNAL startQuest(questID)
EXTERNAL checkCanStart(requiredLevel, questID)
EXTERNAL levelCheck(requiredLevel)
EXTERNAL completed(questID)
EXTERNAL spawnKey()

VAR canStart = false
VAR levelMet = false
VAR finished = false

-> main
=== main ===
~ finished = completed("FetchKeyQuest")
~ levelMet = levelCheck(7)
~ canStart = checkCanStart(7, "FetchKeyQuest")


{finished == true:
    - Good job getting that gate open!
    -> END
}


{levelMet == false:
    - You don't look like you can handle this mission. come back once you've hit level 7.
    -> END
}

{canStart == false:
    - You find that key yet?
    -> END
}

{canStart == true and levelMet == true and finished == false:
    - We desperately need to get this gate open to establish a new  trading route, but there's a problem.
I lost the key somewhere in the nearby forest. Can you help us find the key and unlock the gate to the northeast? 
Your efforts will be handsomely rewarded 
    Do you wish to start the Quest?
        + [Accept]
            ~ spawnKey()
            ~ startQuest("FetchKeyQuest")
                -> chosen("Accept")
        + [Decline]
            -> chosen("Decline")
}

=== chosen(Quest) ===
You chose {Quest}
-> END

