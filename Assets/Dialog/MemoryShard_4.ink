EXTERNAL levelCheck(requiredLevel)

VAR levelMet = false

-> main

=== main ===

~ levelMet = levelCheck(15)

{levelMet == false:
    - You don't meet the level requirement to see the memory.
    -> END
}

{levelMet == true:
    - Yardenfall, You are a monster.
    Ausaaf, do save and load.
    
    Weird the developers were talking to me for a second.
    
    Anyways, you. are. a. Monster.
    
    We trusted you, stop fighting, these "Monsters" aren't your enemies.
    They're here to help, please yeild.
    -> END
}
