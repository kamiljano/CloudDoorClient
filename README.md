# About

CloudDoor bot. Connects to Azure IoT Hub and exposes remote methods to it allowing remote control over the device.

The backend code can be found [over here.](https://github.com/kamiljano/CloudDoorAzure)

# TODO

* Environment specific configurations - right now the app always connects to the localhost. In real life situation it should connect directly to the azure server
* Make application run fully in the background, so that the user is not aware of it
* Find a way to run interactive CLIs
* Download a file to the device
* Upload video from the camera
* Key logger
* Remote control using the OS GUI
* Chat
* Better error handling (retries upon generic exception, re-registration if device has been removed)
* Client self-removal upon command
* Register as a service
* Kill process when the native/third-party is switched on
* Copy itself to a random location and name in the file system at startup and run from there
* P2P communication
* Automatic client upgrades
* Forward the logs to the cloud
* DDOS command
* Website password brute force - allows multiple distributed clients to brute force a password of a single user. Requires the introduction of the backend service managing the batched operations

# Build

```
    dotnet publish -c Release -r win10-x64
    dotnet publish -c Release -r ubuntu.16.10-x64 
```