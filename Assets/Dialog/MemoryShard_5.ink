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
    - Yardenfall, I curse you.
    Through the ends of the Earth.
    
    You shall roam this Earth without save and loading.
    
    Death is not the end, but forgetting is. 
    -> END
}
