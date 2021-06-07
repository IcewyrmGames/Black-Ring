
// THINGS TO NOTE/QUESTIONS
/*
    - Do we want to make use of quotations?: Quotations will be shown in the text and are not necessary
    - Is an emotion tag required? Or shall we always use a default character asset if an emotion tag is not specified?
    -     * p: [ Poke her shoulder ] Would something like this require a character prefix? To denote who does an action? Also it would it be p: [ Poke her shoulder ] not [ p: Poke her shoulder ]? Note how 'p:' produces a new line at the "Poke her shoulder" option
    - Note that while making a loop id you say "no" to dying a horrible death it removes the option to say no on the next go
*/

// EMOTIONS USED
/*
    surprise, happy, angry
*/

// EMOTIONS LIST
/*
    frustrated, concerned, confused, smug
*/
// FUNCTIONALITY USED
/*
    https://github.com/inkle/ink/blob/master/Documentation/WritingWithInk.md
    - prefixes, tags, comments, multiple choices, knots, diverts, glue, loops, stitches
*/


// KNOTS, STITCHES
/*
    - Each dateable character has their own knot.
*/

s: Welcome to BlackRing!
    -> testmenu

=== testmenu ===

s: Time to start the test story! Which character's storyline would you like to test?

    * s: JUG
    -> JUG
    * s: MAG
    -> MAG
    * s: THO
    -> THO
    * s: ZHA
    -> ZHA
    * s: MIL
    -> MIL
    * s: Original testscript 
    -> testscript

=== JUG ===
    -> charactertesting
    
=== MAG ===
    -> charactertesting
    
=== THO ===
    -> charactertesting

=== ZHA === 
    -> charactertesting
    
=== MIL ===
    -> charactertesting
    
=== charactertesting ===
s: You have finished this character's test storyline. Returning to the test menu. 
    -> testmenu

=== testscript ===
s: You make your way to her house. 
    -> bedhead

=== bedhead ===

s: You walk into her room. She's sitting on her bed, head buried into a book, headphones blasting.

    * p: "Hey, can you hear me?"
    -> reaction1
    * p: [ Poke her shoulder ]
    -> reaction2

=== reaction1 ===

s: She perks up at the sound of your voice.
JUGDEF: "Oh, I didn't realize you would be here so quickly." #surprise
    -> response

=== reaction2 ===

s: She startles at your poke.
JUGDEF: "AGH WHY DID YOU POKE ME" #angry
    -> response

=== response ===

    * p: "Well, I live nearby."
    -> happilyeverafter
    * p: "I like to be on time."
    -> JUG_sosoprompt
    * p: "I made haste[--"]
    -> responseglue
    * p: "The store was closed, so I ended up being able to come here right away, when I thought it was going to take a long time before I was able to --"
    -> JUG_interrupts

= responseglue
<> and butternut strudle and --"
-> JUG_interrupts

= JUG_sosoprompt
JUGDEF: "Cool, you're prompt!" #happy
    -> happilyeverafter
    
= JUG_interrupts
    JUGDEF: "Wow, that's a lot of words!" #surprise
    -> whatnext

=== whatnext ===
JUGDEF: "So what would you like to do?" #default

    * p: "Live a happy life" -> happilyeverafter
    * p: "Make a lot of funyuns"
    -> makingfunyuns
    * p: "Use a full address for a stitch."
    -> response.JUG_interrupts

=== happilyeverafter ===
s: They lived happily ever after.

    -> END

=== makingfunyuns ===
s: You make a funyun.
JUGDEF: Would you like to make another funyun?

    * no
    -> makingfunyuns
    * yes
    -> canttakeitanymore

=== canttakeitanymore ===
s: HAHA THIS IS THE END

    -> END
