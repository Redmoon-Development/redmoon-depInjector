# Red Moon Dependency Injector

## Description
A Dependency Injection System for Coordinating Plugins and Clients with UniRX optional Support.

## How to Use (Install)
Follow the Install Instructions for https://github.com/sandolkakos/unity-package-manager-utilities

For Git Versioning and Updates, Install: https://github.com/mob-sakai/UpmGitExtension#usage

Then Just copy the link below and add it to your project via Unity Package Manager: [Installing from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
```
https://github.com/Redmoon-Development/redmoon-depInjector.git
```

## How to Use (Work)

### IProvider

IProvider is an Interface that can be registered via calling 
```
DepInjector.AddProvider(this);
```
This will expose your Provider to all the Clients. Try to only register Managers or Macro Classes instead of many small instances.

### IClient

IClient is an Interface that can be registered via calling
```
DepInjector.AddProvider(this);
```
IClient will be informed of whenever a Provider is added, removed, or finalized.
Use this to build up your own instances, subscribe to events, and call various functions.

## Plans

- Add BaseManager Class (Basic Manager that Auto-Does Management of Adding and Removing as a Provider and Client)

## License
MIT License

There is no legally binding modifications to the MIT License, but if you are using my stuff, I would appreciate doing one of the following: buy me a beer if you ever meet me at a bar or invite me to potential money-making operations via my Contact Information provided below.

## Contact Me
Contact me at jackel1020@gmail.com.
If you want your message to actually be read, add "[Github-Message]" to your subject line.
