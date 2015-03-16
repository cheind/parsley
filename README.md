# Welcome to Parsley #

Parsley is an open source initiative to build a low-cost 3D scanner. With Parsley you can create your own 3D models easily by scanning them from your desktop.

<a href='http://www.youtube.com/watch?feature=player_embedded&v=Nmu-038qYmo' target='_blank'><img src='http://img.youtube.com/vi/Nmu-038qYmo/0.jpg' width='425' height=344 /></a>
<a href='http://www.youtube.com/watch?feature=player_embedded&v=-TEoq6cFrvg' target='_blank'><img src='http://img.youtube.com/vi/-TEoq6cFrvg/0.jpg' width='425' height=344 /></a>
More videos can be found on the [Videos](Videos.md) page.

## Requirements ##
Parsley requires hardware to work. Usually this hardware makes 3D scanning expensive. Parsley has been designed with low-cost budgets in mind. As a result you need a standard webcam, a line based laser, and some cartons to glue your printed patterns on. All available for under one hundred Euro.

The software itself is free for use, free for modification and free for commercial applications. Check the license terms at the bottom of the page.

## Getting Started ##
Head over to the [GettingStarted](GettingStarted.md) guide for an introduction on installing and using Parsley. Make sure you read the [SafetyNotes](SafetyNotes.md) before using Parsley.

## Design and Implementation ##
From a users perspective Parsley is an application that allows acquisition of 3D point and texture data.

Under the hood, Parsley is a framework for rapid development of machine vision and especially 3d vision algorithms. Parsley is extensible through a flexible and simple add-in system. Predefined interfaces allow development of algorithms without modifying Parsleys core components. Additionally Parsley offers automatic generation of user interfaces to release the algorithm developer of this tedious task.

Parsley does not try to reinvent the wheel - it bases on well established libraries such as [OpenCV](http://opencv.willowgarage.com/wiki/) and [MathNET](http://www.mathdotnet.com/) and uses them where applicable.

## Retrospect and Outlook ##
Parsley was born in 2009 when Christoph read a publication called ["Low-Cost Laser Range Scanner and Fast Surface Registration Approach"](http://www.rob.cs.tu-bs.de/content/03-research/01-projects/35-3dscanner/swi_2006_09_konferenz_dagm.pdf). This paper has turned into what is nowadays the [DAVID](http://www.david-laserscanner.com/) laser scanner. Since DAVID does not offer source code, Christoph started Parsley as a free-time project to create a flexible 3d scanning framework.

In 2010 Matthias teamed up with Christoph to create an even more user friendly Parsley. Innovative ideas, such as using marker tracking for real-time pose estimation, emerged in this extremely productive joint venture.

In the future we plan add other 3d acquisition methods such as structured light and stereo vision to Parsley. Besides technical advances, we hope that software community picks up Parsley and extend its functionality.

## License ##
```
Parsley
Copyright (c) 2010, Christoph Heindl
Copyright (c) 2010, Matthias Plasch
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:
- Redistributions of source code must retain the above copyright notice, this list 
  of conditions and the following disclaimer.
- Redistributions in binary form must reproduce the above copyright notice, this list 
  of conditions and the following disclaimer in the documentation and/or other materials 
  provided with the distribution.
- Neither the name of the Christoph Heindl nor the names of its contributors may be 
  used to endorse or promote products derived from this software without specific prior 
  written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING 
IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
```
