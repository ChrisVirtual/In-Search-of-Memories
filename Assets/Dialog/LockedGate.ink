EXTERNAL checkKey(key)
EXTERNAL deleteGate()
EXTERNAL handIn(questID)
EXTERNAL completeQuest(questID)

VAR canStart = false

-> main
=== main ===
~ canStart = checkKey("key")
{canStart == false:
    You don't have the right key for this gate
    -> END
}

{canStart == true:
    - Use your key to Open the gate?
    + [Yes]
     ~ deleteGate()
     ~ completeQuest("FetchKeyQuest")
        You unlock the gate with your key, The gate explodes
 ->END
        -> chosen("Yes")
    + [No]
    You choose not to unlock the gate
        -> chosen("No")
}
=== chosen(LockedGate) ===
-> END