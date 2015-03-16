# Introduction #
Welcome to Parsley! This wiki page provides information how to install and setup Parsley on a PC - for users and developers. If Parsley is new to you, we recommend to work through the _Getting Started Tutorial_ below.

Make sure that you've read the [SafetyNotes](SafetyNotes.md) before continuing with this tutorial.

Subsequent to the installation tutorial, the first steps how to configure Parsley, in order to obtain satisfying scanning results, are explained on this page.

# Parsley Hardware setup #
Parsley requires only a low cost hardware setup which consists of the components listed below:
  * **Standard webcam** (observe, that rising resolution settings might lead to small frame rates).
  * Robust **reference body**, made of stiff carton materials or even wooden plates.
  * Battery supplied **laser** with laser ray extension lens (line shaped). Read the [SafetyNotes](SafetyNotes.md) again.
  * Optional, but recommended: A kind of tripod, belonging to the reference body, where the webcam can be mounted (the cameras' position relative to the reference body must not change after the calibration had been performed).

The reference body should be made up of a ground plane, with two side planes mounted together perpendicular and the side walls should be placed onto the ground plane. Therefore the whole construction is shaped like cube with three faces missing.

![http://parsley.googlecode.com/svn/trunk/doc/ParsleyHardwareSetup.png](http://parsley.googlecode.com/svn/trunk/doc/ParsleyHardwareSetup.png)

The _Calibration Patterns_ (pdf documents, containing the printouts are available), needed for the camera calibration should be glued onto the side walls. However, we recommend to use **removeable calibration plates**, made of e.g. stiff plastic material, where to glue the patterns on.

# Parsley Users #
This sub paragraph deals with the installation of the Parsley, based on compiled binary files for common users.
Before installing Parsley, make sure that your computer system fulfills the following requirements:
  * Parsley is designed for Windows operating systems.
  * Install the _Microsoft .Net Framework 3.5 Service Pack 1_ - download from the [MicrosoftDownloadCenter](http://www.microsoft.com/downloads/en/default.aspx).
  * Install the _Microsoft Visual C++ 2008 Redistributable Package (x86) Service Pack 1_ - available at [MicrosoftDownloadCenter](http://www.microsoft.com/downloads/en/default.aspx).

After installing these software packages successfully, download the Parsley 0.2 binaries, which are available as ZIP file, from the Parsley [Downloads](http://code.google.com/p/parsley/downloads/list) page.
Now, create a folder on the Windows partition (e.g. C:\Programs\Parsley\) and extract the contents of the ZIP file into the folder.
Your newly created Parsley folder should now contain several **dll** files, the **Parsley executable** (Parsley.exe) and a folder called **Patterns**, containing _Marker Images_ and different pdf files with **CalibrationPatterns** ready for printing.

If your Parsley installation has been successful, the Parsley _Welcome Window_ should appear soon after the executable file has been started.

# Parsley Developers #
Parsley users who intend making source code changes and to further the development of Parsley, shall find information, how to build Parsley using the _Microsoft Visual Studio 2008_ development environment, below.

Firstly, make sure that _Microsoft Visual Studio 2008_, supporting C# and C++, is installed on your computer system.
Secondly, use a subversion client to check out the source code into a specified folder (e.g. C:\ProgramFiles\Parsley). Please find more information regarding the source check out and the repository address on page [Source](http://code.google.com/p/parsley/source/checkout).

Thirdly, after checking out the source code, you need to download the **ThirdPartyLibraries** from the Parsley [Download](http://code.google.com/p/parsley/downloads/list) page and include them into the Parsley project:
  * Download the **ThirdPartyLibraries** ZIP file and extract the contents into the specified folder, which contains the source checkout.
  * The location where the source checkout is stored should now contain a folder _ThirdParty_.
  * Open the folder and run the script file. Now the needed library files will be copied into the corresponding project folders.

As you can see, the Parsley project folder already contains _Visual Studio Solution_- and _Project_ files. By double-clicking the solution file, the Parsley project will be imported automatically into the development environment.
Observe, that Parsley consists of several C# sub projects and one C++ sub project, named **Draw3D**. Before you try to build Parsley firstly, make sure that you have chosen the **Solution Platform** _X86_. You can also enclose **debug information** by changing the **Solution Configuration** to _Debug_. For more detailed information open the _ConfigurationManager_.

Due to existing sub project **dependencies** try to build Parsley according to the building sequence below (All sub projects are C# projects, except _Draw3D_):
  1. Parsley.Draw3D (C++)
  1. Parsley.Core
  1. Parsley.Core.CalibrationPatterns
  1. Parsley.Core.LaserLineAlgorithms
  1. Parsley.Core.LaserPlaneAlgorithms
  1. Parsley.Core.BuildingBlocks
  1. Parsley.UI
  1. Playground
  1. SlickInterface
  1. Parsley
_Visual Studio_ provides a **project dependencies** menu in where you can specify the building sequence, so that you do not need to build the sub projects one by one, any longer. If you use other development environments, consider that building C# and C++ sub projects together in one **project solution**, might not be possible. In that case you need to create a separate **C++ solution** in order to build the _Draw3D_ sub project.

# Setup Parsley and perform the first 3D Object Scan #
When the Parsley application is started for the first time, no Parsley configuration is available and an empty configuration file _CurrentParsley.cfg_ is being created in the folder, where the Parsley executable is located.
Before a 3D scan with satisfying results can be performed, the camera needs to be calibrated and a set of Parsley parameters need to be adjusted.

A set of **wiki pages** will help you to perform these parameter settings. Furthermore, the page [Videos](http://code.google.com/p/parsley/wiki/Videos) contains a list of videos, presenting the most important steps in configuring Parsley and showing example object scans. The list below represents a summary how to set up Parsley step by step:
  * Read the [SafetyNotes](SafetyNotes.md) one more time.
  * Perform the _Intrinsic Camera Calibration_: Wiki page [IntrinsicCalibration](IntrinsicCalibration.md).
  * Perform the _Extrinsic Camera Calibration_: Wiki page [ExtrinsicCalibration](ExtrinsicCalibration.md).
  * Set up the _Parsley Scanning Options_: Wiki page [ParsleyOptionsMenu](ParsleyOptionsMenu.md).
  * Perform a _3D Object Scan_: Wiki page [Parsley3DScan](Parsley3DScan.md).
  * Have fun, and satisfying results :-).