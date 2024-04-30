EXTERNAL startQuest(questID)
EXTERNAL checkCanStart(requiredLevel, questID)
EXTERNAL levelCheck(requiredLevel)
EXTERNAL completed(questID)

VAR canStart = false
VAR levelMet = false
VAR finished = false

-> main
=== main ===
~ finished = completed("QuestID")
~ levelMet = levelCheck(1-100)
~ canStart = checkCanStart(1-100, "QuestID")

{finished == true:
    - Thanks for your help.
    -> END
}

{levelMet == false:
    - You are too low level to complete this quest. Come back at level ?.
    -> END
}

{canStart == false:
    - Reminder on what to do for the quest, or who to talk to next 
    -> END
}

{canStart == true and levelMet == true and finished == false:
    - Quest description in here
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

