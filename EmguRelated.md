# Installation #
be sure to have [SP1](http://www.microsoft.com/downloads/details.aspx?familyid=A5C84275-3B97-4AB7-A40D-3802B2AF5FC2&displaylang=en) redistributable installed on MSVC90.

On x64 and Visual Studio Express use platform-target x68. If that is not present in Express then

Tools --> Options --> Projects and Solutions-->General  Check "Show advanced build configurations" If "Configuration Manager" doesn't show on the Buid menu, add it and click it. Active Solution Platform --> New --> Type or select the new platform x86