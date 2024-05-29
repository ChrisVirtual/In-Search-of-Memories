EXTERNAL ShopOpen()
EXTERNAL ShopClose()

VAR shopDisplay = false

-> main

=== main ===
My name is Earl, Would you like to browse my wares?
    + [Yes]
        ~ ShopOpen()
        shopDisplay = true
        -> chosen("Yes")
    + [No]
        -> chosen("No")
        {shopDisplay == true:
        - Pleasure doing buisness
        ~ ShopClose()
        }
=== chosen(Trader) ===
-> END