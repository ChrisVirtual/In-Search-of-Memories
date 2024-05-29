EXTERNAL levelCheck(requiredLevel)

VAR levelMet = false

-> main

=== main ===

~ levelMet = levelCheck(10)

{levelMet == false:
    - You don't meet the level requirement to see the memory.
    -> END
}

{levelMet == true:
    - Hello again, Yardenfall.
    They are screaming, Scared. What did you do?
    
    I told you to save them... Why...
    
    You must extinguish the fire.
    
    PLEASE YARDENFALL, THE FIRE SPREADS.
    -> END
}
