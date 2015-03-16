# Introduction #
This wiki page deals with the Parsley options which need to be set up before starting a new 3D scan. The _Parsley Options_ menu can be split up into the settings categories below:
  * _ScanWorkflow_ contains the settings for the scanning process, such as _LaserLineAlgorithms_, _PlaneDetectionAlgorithms_, _ROI_ (region of interest) and _PointAccumulator_ types.
  * _Camera_ includes parameters for the camera configuration. The intrinsic paramters as well as the frame size (resolution) need to be set.
  * _Laser_ allows to define the color of the provided laser.
  * _ReferenceBody_ consists of the reference planes which are defined by the saved extrinsic camera parameters for each of the planes (left plane, right plane and ground plane).
  * _Positioner_: Parsley supports _MarkerPositioners_ which consist of markers of different pattern types (circle, chessboard or marker patterns).

The paragraphs below describe how to set the Parsley Options, especially for the _ScanWorkflow_ in order to obtain satisfying scanning results (watch reference video [ParsleyOptions](http://www.youtube.com/watch?v=1lCWBAVj3-o)).

# How to configure Parsley for the 3D Object Scan #
## Set up the _ReferenceBody_ and the _Positioner_ ##
After performing the Extrinsic Calibration for the reference planes (see [ExtrinsicCalibration](http://code.google.com/p/parsley/wiki/ExtrinsicCalibration)) , the _ReferenceBody_ as well as the _Positioner_ can be set up.
For configuring the _ReferenceBody_, assign the corresponding extrinsic camera parameters to the given properties **LeftPlane**, **RightPlane** and **GroundPlane**.

Currently, Parsley supports _MarkerPositioners_, which consist of a predefined _CompositePattern_ that is used to detect the positioners' orientation.  Setting up the positioner requires the definition of a _CompositePattern_, containing two markers.

## Adjust the _ScanWorkflow_ settings ##
Parameter changes in the settings category _ScanWorkflow_ affect the quality of the 3D scan directly.
The ScanWorkflow parameter set and reasonable values (based on various tests) are described below.
### _LaserLineAlgorithm_ ###
Concerning the LaserLineAlgorithm, which is needed find a laser line in the camera frame, Parsley offers two alternatives as they are:
  * _WeightedAverage_, which is based on the weighted average pixel intensity of the laser points. This algorithm requires two parameters: _IntensityThreshold_ determines the minimum intensity value needed for a point, to be treated as valid laser point. _SearchDirection_ defines in which direction the frame should be scanned through in order to find laser points (TopDown or BottomUp).
  * _BrightestPixel_, which is based on the highest intensity value of a laser point in a laser line. The laser line is considered as found, if at least one pixel exceeds the minimum intensity, given by the parameter _IntensityThreshold_.

Reasonable values for the _IntensityThreshold_ are located in the range 150 - 240. In order to find out the proper threshold value, change to the _Laser Configuration_ menu (_Setup_ tab). While pointing with the laster in direction of the reference body, use the _Live Image_ window to find out if the laser line can be found. If the line is detected correctly a continuously green coloured line is drawn at the position of the laser line. Vary the _IntensityThreshold_ value to optimize your result. The value of _IntensityThreshold_ depends mainly on the **strenght** of your laser module and the lighting conditions.

### _LaserPlaneAlgorithm_ ###
As an algorithm to detect the laser plane, Parsley provides the **Ransac** algorithm which uses the intersection points, of laser point **eye rays** with the reference planes of the reference body, to estimate a plane (the laser plane) which contains the detected intersection points.
The parameters which specify the behaviour of the plane algorithm are described below.
  * _MinimumConsensus_: Defines the minimum number of consensus points (located directly on the plane) in percent. Reasonable values between 0.3 and 0.6. The lower the value, the higher becomes the uncertainty of the laser plane detection. Too high values lead to longer algorithm working times and to a higher number of rejected planes.
  * _OnlyOutOfROI_: If this value is set to true, only laser points which are located outside of the region of interest will be involved in the plane detection algorithm.
  * _Accuracy_: Defines the maximum allowed distance (in unit length) a point may be displaced from the plane, in order to be still considered as a point which belongs to the plane. Reasonable values between 0.2 and 0.5. While too high values lead to high plane detecting uncertainties, however, too low values will raise the probability of plane rejections.
  * _Iterations_: Set the maximum number of iterations of the Ransac algorithm to find a proper plane. Standard value is 30 iterations. The higher the number of iterations is set, the longer the computing time will be.
  * _Constraint_: This property cannot be changed and it is set to _RejectParallelPlanes_ which means, that detected laser planes which are orientated parallel to reference planes will be rejected.

### _LaserPlaneFilterAlgorithm_ ###
Parsley provides one _LaserPlaneFilter_ which rejects detected laser planes with an included angle, with respect to the cameras' **optical axis**, smaller than the property value _MinimumAngle_. Hereby the optimum value depends on the position of the camera relative to the reference body. The standard value is set to 30 degrees.

### _PointAccumulator_ ###
_PointAccumulators_ are used in Parsley to store 3D point information for every pixel of the 2D camera frame image. Imagine, that one 2D point corresponds to several 3D points (e.g. due to uncertainties) which have been found during a certain scanning period. But which of the scanned 3D points should be set in relation to the single 2D point?
  * If a _MedianPointAccumulator_ is used, the median eye ray length (of the eye ray through the 2D pixel) represents the location of the corresponding 3D point.
  * The _MeanPointAccumulator_ uses the mean value of the eye ray lengths to extract the corresponding 3D point.

### _MinimumDistanceToReferencePlane_ ###
This parameter is used to determine the minimum distance (unit length) of a scanned 3D point to a reference plane, in order to be accepted as 3D object point. Consider the value with respect to the Z-axis of a reference plane. Chosing a value smaller than 0 means that points close to the reference plane will be rejected.

### _ROI_ (region of interest) ###
The **region of interest** _ROI_ defines the subpicture of the camera frame which should be considered for scanning the object 3D points. This means, only 3D points which have been found in this specified region will be added to the 3D pointcloud. A subpicture can be chosen by drawing a rectangular region, using the mouse cursor in the _LiveImage_ window.

### Comments to the _ScanWorkflow_ parameter set ###
After these parameters have been adjusted, one can start an object scan using the _Record_ tab.
Observe that the example parameter values are mainly based on test experiences. Furthermore it might take some trials to find out a proper parameter set.