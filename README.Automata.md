AUTOMATA
========

This package provides basic dia shapes for modelling Automata (/ creating State Transition Diagrams for State Machines). 

Copy shapes and sheets directories to your ~/.dia directory and restart Dia to use the shapes.

The shapes provided are as follows:

1. Start State: The initial(/start) state of the automata - marked by a circle with a single arrow pointing into it.
Files: shapes/Automata/startstate.shape, shapes/Automata/sstate.png

2. Final State: The accepting(/final) state - indicated by a double (concentric) circles with the state-label.
Files: shapes/Automata/finalstate.shape, shapes/Automata/fstate.png

3. Intermediate State: Any state which is neither Start nor Final will be an intermediate state - indicated by a single circle with the state label.
Files: shapes/Automata/startstate.shape, shapes/Automata/istate.png

4. Start/Final State: A state which is both initial and accepting - marked by double concentric circles with a single inbound arrow. 

Only the nodes in the Transition graph are provided. Use standard Dia arrows to create the transition edges.
Hint: For best results attach the transition symbol to the corresponding connection arrow.

Created by Faconvert sy4p.24.png -alpha on -channel RGBA -fuzz 8% -fill none -draw 'color 0,0 floodfill' sy4p.t.png
raz Shahbazker (faraz shahbazker gmail com)
Edited by Minh, Le Ngoc (ngocminh.oss@gmail.com)
