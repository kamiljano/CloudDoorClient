# About

CloudDoor bot. Connects to Azure IoT Hub and exposes remote methods to it allowing remote control over the device.

The backend code can be found [over here.](https://github.com/kamiljano/CloudDoorAzure)

# TODO

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

# Build

```
    dotnet publish -c Release -r win10-x64
    dotnet publish -c Release -r ubuntu.16.10-x64 
```