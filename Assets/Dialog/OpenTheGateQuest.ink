EXTERNAL levelCheck(requiredLevel)

VAR levelMet = false

-> main

=== main ===

~ levelMet = levelCheck(5)

{levelMet == false:
    - You don't meet the level requirement to proceed with this dialogue.
    -> END
}

{levelMet == true:
    - Hello, Adventurer.
    It's been a while, but you must remember.
    Throughout the harsh elements, you survived, but memories "sacred memories" have been lost.
    Throughout your journey, you must collect memory shards, recollect, remember.
    Death is not the end, but forgetting is. 
    The journey will not be easy, but it never is. Good luck, friend.
    -> END
}
