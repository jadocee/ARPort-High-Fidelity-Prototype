<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0">

![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

# ARPort

## What is it?

ARPort is a high-fidelity prototype application for the HoloLens 2, targeted at customers and other individuals of international airports. The goal of the application is to simplify the user's experience at an international airport by providing solutions to common problems.

<!-- TODO Add to this; maybe use information from the report? -->

This project was created with the goal of understanding the importance and implementation of design concepts in Human-Computer Interaction (HCI), and to demonstrate the use of AR (Augmented Reality) applications at an international airport when considering a world where AR and HMD (Head-Mounted Display) technology is commonplace. 

ARPort was designed and developed in completing [SENG2260](https://www.newcastle.edu.au/course/SENG2260) at the University of Newcastle, Australia.

## Features

ARPort provides protype implementations for the following features:

### Navigation

Assists the user with navigating between landmarks of an international airport. The user can select a landmark from the 'navigation menu' and the application will indicate a direction for the user to travel.

[Microsoft Spatial Anchors](https://learn.microsoft.com/en-us/windows/mixed-reality/design/spatial-anchors) are used to track the landmarks of an international airport. They can be created by accessing the 'Landmark Manager' menu.

<div style="display:flex;flex-direction:row;justify-content:center;align-content:center;padding-top:6px;">
<img src="Images/ARPort-Navigation-Demo.gif" alt="Navigation Demo" loading="lazy" style="max-width:100%;height:auto;width:auto\9;">
</div>

### Group Tracking

Provides the user with information about their current travel party. The user can access the 'My Group' menu to view the names and locations of each member of their group. Ideally, the user (or member) will have the option to disable sharing their location to the 'My Group' feature temporarily or permanently.

The user can select a member of their group from the 'My Group' menu to invoke the [navigation system](#Navigation) and begin navigation to the location of the selected member.

<div style="display:flex;flex-direction:row;justify-content:center;align-content:center;padding-top:6px;">
<img src="Images/ARPort-Group-Tracking-Demo.gif" alt="Group Tracking Demo" loading="lazy" style="max-width:100%;height:auto;width:auto\9;">
</div>

### Notifications Center

<div style="display:flex;flex-direction:row;justify-content:center;align-content:center;padding-top:6px;width:100%;">
<img src="Images/HandMenu_LeftHand_Alt.jpg" alt="Notifications Center" loading="lazy" style="width:auto;height:auto;max-width:80%;"> 
</div>

### Alerts and Announcements

<div style="display:flex;flex-direction:row;justify-content:center;align-content:center;padding-top:6px;width:100%;">
<img src="Images/PSA_Demo.jpg" alt="Notifications Center" loading="lazy" style="width:auto;height:auto;max-width:80%;"> 
</div>

## Dependencies

* HoloLens 2 [HMD](https://www.microsoft.com/en-us/hololens/buy) or [Emulator](https://learn.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/using-the-hololens-emulator)
* Unity version 2020.3.38f1 (download from the [Unity download archive](https://unity3d.com/get-unity/download/archive))
* [MRTK3](https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk3-overview/)


<div style="width:100%;display:flex;justify-content:center;">
<div style="width:50%;">
<a href="https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk3-overview/">
<img src="https://user-images.githubusercontent.com/13754172/122838841-b736e200-d2ab-11eb-85d6-f75fac6bce36.png" alt="made-with-mrtk">
</div>
</a>
</div>




<!-- ## References

<div style="display:flex;flex-direction:row;gap:1rem;">
<div style="flex:1;min-width:fit-content;">
[1]
</div>
<div>
<a name="1">V. Bogicevic, W. Yang, C. Cobanoglu, A. Bilgihan, and M. Bujisic, “Traveler anxiety and enjoyment: The effect of airport environment on traveler’s emotions,” Journal of Air Transport Management, vol. 57, pp. 122–129, 2016, issn: 0969-6997. doi: https://doi.org/10.1016/j.jairtraman. 2016.07.019. \[Online\]. Available: <a href="https://www.sciencedirect.com/science/article/pii/S0969699715300697">https://www.sciencedirect.com/science/article/pii/S0969699715300697</a></a>
</div>
</div> -->