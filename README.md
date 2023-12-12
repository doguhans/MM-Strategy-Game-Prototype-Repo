# MM-Strategy-Game-Prototype-Repo
Multiplayer Mobile Strategy Game Prototype  (Happy Hour Games)

Game Prototype Logic:

***CREATE or JOIN ROOM SCENE***
On execution user's can see the Create or Join room scene canvas. 
In order to begin matchmaking, Creating a room is **mandatory**.
To create a room one user must enter a name at the text view and press "Create Room " button. 
(Preferable: One can choose itself a unique ID by clicking the ID creator button which can be seen at right top corner in the scene 
right next to game rooms listings view, if not selected the ID of that user appears as "-1" by default)
After the game room is created room name appears at the room listings view which can be seen by other user(s).
By clicking on the room name that appears in room listings menu another user can join the same room with the creator of the room. (Creator of the room becomes Master client)
Only one other user can join the room.

****CURRENT ROOM SCENE***
After Joining the room Current Room canvas appears on the screen.

+Only master client can start the game but with one important condition which is explained below: 
-At this scene client user, other then the creator of the room(Master client), needs to Press **N** ready-check button, which stands in between start game button and leave room buttons, in order and change the text on the button to **R**.

Without ready check is checked Master client **CAN-NOT** start the game by clicking or tapping on the "Start Game" button.
Users can also leave the game room to join or create another room by clicking or tapping on the "Leave Room" button (before game starts).
When game starts the Game scene appears to both master client's scene and other clients scene.

Three moveable player game objects become instantiated for each player when the game scene appears.
******************* How To Control Player Game Objects *******************
To move one player game object user needs to click or tap on the game object **ONE TIME**.
If the user clicks or taps **SECOND TIME** on the same game object it stops doing its actions.
After tapped or clicked on the game object user can select a destination for that game object.
While one game object is moving if user clicks of taps on another game object, which is assigned for that user, the game object that is already moving stops doing its actions until further clicks or taps made on that game object.
Resources can be collected by tapping or clicking action done on them.
Player game objects move using shortest path algorithm and finds its own shortest path for designated destination.

-Hope you can enjoy, cheers.

