EXTERNAL levelCheck(requiredLevel)

VAR levelMet = false

-> main

=== main ===

~ levelMet = levelCheck(5)

{levelMet == false:
    - You don't meet the level requirement to see the memory.
    -> END
}

{levelMet == true:
    - Hello, Adventurer.
    Or should I say Yardenfall, it's been a while since you heard that right?
    Remember, the fire? The village burnt down. We must find the others.
    
    If there are any... it's getting hazy again.
    -> END
}
