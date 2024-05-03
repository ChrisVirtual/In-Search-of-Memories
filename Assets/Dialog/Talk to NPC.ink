EXTERNAL startQuest(questID)
EXTERNAL checkCanStart(requiredLevel, questID)
EXTERNAL levelCheck(requiredLevel)
EXTERNAL completed(questID)

VAR canStart = false
VAR levelMet = false
VAR finished = false

-> main
=== main ===
~ finished = completed("TalkToNPCQuest")
~ levelMet = levelCheck(5)
~ canStart = checkCanStart(8, "TalkToNPCQuest")

{finished == true:
    - Thanks for your help.
    -> END
}

{levelMet == false:
    - You are too low level to complete this quest. Come back at level 9.
    -> END
}

{canStart == false:
    - Did you find out what you need to know? Remember to talk to the everyone in order from north to south
    -> END
}

{canStart == true and levelMet == true and finished == false:
    - Oi you, you new around here, go get to know the locals
    Do you wish to start the Quest?
        + [Accept]
            ~ startQuest("TalkToNPCQuest")
                -> chosen("Accept")
        + [Decline]
            -> chosen("Decline")
}

=== chosen(Quest) ===
You chose {Quest}
-> END
