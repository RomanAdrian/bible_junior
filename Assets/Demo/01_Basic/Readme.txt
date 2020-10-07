
01 Basic Demo scene
-------------------


The basic scene shows you the basic principals of Databox and how you can load and save existing objects during runtime.


SimpleDataboxLinkking.cs
------------------------
The cube and the sphere objects have both a script called "SimpleDataboxLinking"
This script waits for the database to be loaded.
On loaded it takes all values from the database and assigns them to the sphere.
(position, color, speed, direction)


DataboxExample.cs
-----------------
The DataboxExample script has some methods which are being called
by the UI.(Save, Load, Reset) It also loads the database on start.
