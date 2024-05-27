EXTERNAL startQuest(questID)
EXTERNAL checkCanStart(requiredLevel, questID)
EXTERNAL levelCheck(requiredLevel)
EXTERNAL completed(questID)
EXTERNAL handIn(questID)
EXTERNAL completeQuest(questID)

VAR canStart = false
VAR levelMet = false
VAR finished = false
VAR readyToHandIn = false

-> main
=== main ===
~ finished = completed("SlayEnemiesQuest")
~ levelMet = levelCheck(5)
~ canStart = checkCanStart(5, "SlayEnemiesQuest")
~ readyToHandIn = handIn("SlayEnemiesQuest")

{finished == true:
    - Thanks again, I'm suprised you were able to take care of that all by yourself
    -> END
}

{readyToHandIn == true:
    - Wow great job, you're pretty strong, here's your reward
    ~ completeQuest("SlayEnemiesQuest")
    -> END
}
{levelMet == false:
    - You look too weak right now. You need more experience come back at level 5.
    -> END
}

{canStart == false:
    - You killed them all yet? Come back to me when you are finished
    -> END
}

{canStart == true and levelMet == true and finished == false:
    - Hey tough guy, You reckon you can take care of some of these monsters?. I've got something for you if you can.
    Do you wish to start the Quest?
        + [Accept]
            ~ startQuest("SlayEnemiesQuest")
                -> chosen("Accept")
        + [Decline]
            -> chosen("Decline")
}

=== chosen(Quest) ===
You chose {Quest}
-> END

