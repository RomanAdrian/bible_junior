
Advanced Example
-----------------

This example shows the use of multiple database objects to support
saving runtime generated objects.

The general idea is as follow:

1. We use the initial databox object for all assets we want to instantiate.
2. We use an empty runtime databox object which will maintain all instantiated
objects at runtime.

3. At runtime a freshly instantiated object gets its data from the initial
database and registers them into the runtime database with it's unique instance id.

4. We only change the data in the runtime database.

5. When loading, we iterate through all entries in the runtime database.
As we have saved the type, position and color of each object we can easily
re-create the saved state.



Scripts explained
-------------------


AdvancedDataboxLinking.cs
Registers the game object to the database according to objectID, fromDBId and toDBId variables.


AssignObjectValues.cs
Assigns all values to the object or the database.

PlaceAsset.cs
Makes sure the object moves along the mouse cursor and snaps to a grid. Also handles mouse click event and object instantiation.
After user has clicked it calls the LinkToNewDatabase method in the AdvancedDataboxLinking script.

SaveManager.cs
The save manager handles the restoring of saved objects.
After the database has been loaded a coroutine is being called which instantiates all saved objects back and registers them to the 
existing database as a new entry while removing the old entries.

UIManager.cs
The UI manager handles all ui inputs. It also loads the initial database on Awake and builds the build menu ui after initial database has been loaded