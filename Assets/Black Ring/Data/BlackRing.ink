//It’ Monday morning. 8:05am to be exact. Grogginess clouds your head and clutches your mind as you stumble into the bus and take a seat by the window. You groan internally as you lightly plop your head against the glass.

Welcome to our demo.

//* Plop. -> plop
* Start! -> park_hand_hold

== plop
“Rough Night?” 
* You don't even know. -> she_grins 

== she_grins
A girl with short, brown hair and ember eyes takes the seat next to you.

* So what's your story? -> forgotten_headphones

== forgotten_headphones
A few stops later, you get up to leave at your stop.

* Scchhhhhhhhh -> forgotten_headphones_2

== forgotten_headphones_2

Your foot bumps into Maggie's headphones. 

* Uh oh. -> forgotten_headphones_peek_inside

== forgotten_headphones_peek_inside
You pick up the headphones and look inside. You see a name scrawled inside, next to a tiny scribble of a phone number. 

* Keep them. -> forgotten_headphones_keep_them
* Text her. -> forgotten_headphones_text_her

== forgotten_headphones_keep_them
Really? You're going to keep them? For yourself?
* Second guess yourself. -> forgotten_headphones_second_guess #name:
* Yeah, I guess I am. -> this_ending_is_for_losers #name:

== this_ending_is_for_losers
Wow, you're an asshole.
-> END.

== forgotten_headphones_second_guess
* -> forgotten_headphones_what_now

== forgotten_headphones_what_now
-> END.

== forgotten_headphones_text_her
You text her. She profusely thanks you and invites you to coffee. -> coffee_date

== coffee_date
She's moving into a new apartment.
* Offer to help her move. -> moving_offer_help
* Gee, that's tough. -> moving_gee_tough

== moving_gee_tough
-> END.

== moving_offer_help
I'd love that! 
-> moving_show_up

== moving_show_up
You show up and help her move. She asks you to dinner as thanks.
* Say yes -> dinner_a_go_go

== dinner_a_go_go
You eat pasta together.
*The date goes well. -> dinner_what_next
*The date goes poorly. -> generic_end

== dinner_what_next
The night is starting to dim. You plates are nearly empty, and the waitor brings over the check.
* Grab the check. -> check_pay
* Grab for your wallet.. slowly. -> check_too_slow

== check_pay 
You grab for the check, and your hands bump into each other. "Too slow." She winks, and pulls out her card.
* -> after_dinner

== check_too_slow
You reach for your wallet, but she already has the check in hand. "I've got it," she says, winking at you. 
-> after_dinner

== after_dinner
* Ask if she wants to head to the park. -> continue_to_park
* Call it a night. -> asexual_talk_dealbreaker

== continue_to_park
"Sure, you mean Delaney Park? I like to go there on occasion to write, actually." she grins.
* Reach for her hand.-> park_hand_hold
* Continue walking. -> park_walking

== park_hand_hold
She grasps your hand in hers as you reach for it. You notice her hand is cool to the touch. Lightly at first, and as though hestitating, she slowly grasps your hand more firmly.
-> asexual_talk

== asexual_talk
"Listen... I need to talk to you about something." 
* "Sure, anything." -> asexual_talk2

== asexual_talk2
"I really like you, and I'd like to continue going out, but I want to make sure we understand each other right, you know?"
* "Uh Oh," []you think internally, not entirely sure where this is heading. -> asexual_talk_do_you_know

== asexual_talk_do_you_know
"Well... I'm asexual. I'm not sure if you know what that means, but it's something you should know about me if you are interested in dating. If you have any questions, let me know, and I'll answer anything."
* "I don't really know["] what that means. Can you explain it to me?" -> asexual_talk_explain_asexuality
* "I'm familiar with [it"] the term, but that can mean a lot of different things. What does that mean for you?" -> asexual_talk_explain_for_maggie

== asexual_talk_explain_asexuality
"Okay. So. Asexual can mean a lot of different things- but to some degree it refers to not experiencing sexual attraction."
-> asexual_talk_explain_for_maggie

== asexual_talk_explain_for_maggie
"I personally am asexual, but also romantic. Which can make dating a little hard for me. Because although I want to fall in love and find a partner, for many people, sex and love are pieces to the whole. For me, those are two entirely separate things."
* "Okay. That makes sense." -> asexual_talk_do_you_have_any_questions 

== asexual_talk_do_you_have_any_questions
"{ I really am happy to answer questions though. Do you have a|Do you have a|A}ny more questions?"
* { not asexual_talk_q1 } "Sure.. If you don't mind me asking, how did you discover this about youself?" -> asexual_talk_q1
* { not asexual_talk_q2 } "Is it only sex? Or is it any physical touch?" -> asexual_talk_q2
* "I don't have any more questions." -> asexual_talk_no_more_questions

== asexual_talk_q1
"Well... it actually took me a long time to figure out. I had never heard about asexuality when I was young, so I always assumed that sex was something that everybody got interested in as they got older." 
* "And you never did?" -> asexual_talk_q1_p2

== asexual_talk_q1_p2
"I mean, I kinda did - but not really. It's like I tried to talk myself into being interested in it. And... I fell in love. It's something I thought would come naturally. And for the longest time, I thought something was wrong with me, because I - I loved him. But sex was just going through the motions. It wasn't something I ever enjoyed, and that feeling never came."
* "So then... you have, you know, tried it then?" -> asexual_talk_q1_p3 

== asexual_talk_q1_p3
"What, sex?" She laughs. "Yeah, I did. Try it. For over 3 years. Then I just couldn't do it anymore - and my relationship fell apart. We both wanted different things, but unfortunately, we discovered it too late. I was young, and put a lot of pressure on myself. I wish I knew this was a thing when I was younger. It would have saved me a lot of heartache." 

* "That must have been rough." ->asexual_talk_q1_p4

== asexual_talk_q1_p4
"Yeah, it was. I thought for sure the feelings would come with him. But after that, I stopped second-guessing myself and allowed myself to just be. And it was a huge weight off my shoulders when I finally just accepted it." 
-> asexual_talk_do_you_have_any_questions

== asexual_talk_q2
"For me, honestly, it's just sex. I still like cuddling. I love hugs. I'm a big hugger. I like the feeling of closeness. But I know I don't want sex - and I don't want to start a relationship unless that is understood. I don't want to date having different expectations. That's a recipe for disaster."
-> asexual_talk_do_you_have_any_questions

== asexual_talk_no_more_questions
"Guess it's my turn then. What are your thoughts on everything? How do you feel about it?"
-> premature_end
//* [Need More Time] "I guess I'd need more time to think about it." -> asexual_talk_more_time
//* [Also Asexual] "I'm actually asexual as well. I've never dated anyone else who is!" -> asexual_talk_both_asexual
//* [Dealbreaker] "To be honest, I really like you Maggie, but I'm not sure that I would feel happy long term in a relationship without sex."  -> asexual_talk_dealbreaker

== asexual_talk_more_time
She pauses. "I understand.". she looks at you and smiles. "Let me know once you've had time to think it over."
-> premature_end

== premature_end
\---
\---
THANK YOU FOR PLAYING!

It seems we've run out of content for this demo. Let us know if you enjoyed it, we'd love to hear feedback!
Please check out our page at www.icewyrm.games and like us on Facebook at facebook.com/icewyrmgames!

-> END.

== asexual_talk_both_asexual
-> END.

== park_date_end
-> END.

== park_walking
-> END.

== asexual_talk_dealbreaker
That doesn't quite work for you you think, but you'd like to remain friends. 
-> END

== generic_end
Well, that's that!
->END

