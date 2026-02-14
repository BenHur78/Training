# Steps
- Creating a system with high levels parts and low levels parts
- We will model a gineology, relationships betweem different people
- Imagine a system where we perform queries to a gineology database
- Because the high level depends on Relationships, it means it cannot change without affecting the high level module.
- How relationships can change?


# What we learned here
- The principle teach us how to invert the direction of the dependencies. Before the high level module depend upon the low level module.
- Now we can create an abstraction that the low level module will depend on. This way the high level module will not depend
on low level things.
- Now the low level module can change without impacting the high level module.