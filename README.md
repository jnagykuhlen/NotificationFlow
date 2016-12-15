# Overview

NotificationFlow is a general-purpose message queue implementation in C#. It can be used to decouple application components by providing a central entity, the *Notification Manager*, which acts as a mediator. Notifications sent to the system are delivered to all components that subscribed for the particular notification type, not requiring the application components to explicitly referencing each other anymore. This is useful e.g. for processing game events, but by no means limited to this application field. Currently, the project only supports synchronous message flow, but may be extended to use multiple threads for parallel message processing in the future.