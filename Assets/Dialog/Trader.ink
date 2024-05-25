EXTERNAL TraderInteract()

-> main

=== main ===
My name is Earl, Would you like to browse my wares?
    + [Yes]
        ~ TraderInteract()
        -> chosen("Yes")
    + [No]
        -> chosen("No")
=== chosen(Trader) ===
-> END