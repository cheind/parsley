# Introduction #
Camera calibration is used to determine a set of parameters which are used to describe the mapping between the 3D environment and the produced 2D camera image.
These parameters can be subdivided into two groups.
  * The Intrinsic Camera Parameters describe the internal geometry of a camera including radial and tangential distortion coefficients.
  * The Extrinsic Camera Parameters describe the relation between the 3D camera coordinate system (located in the projection center) and a given world coordinate system (position and orientation).
The following chapter describes how to perform the extrinsic calibration with Parsley.

# Extrinsic Calibration #
Extrinsic Camera Parameters define the relationship between 3D camera coordinate system and a given world coordinate system, using a simple coordinate transformation matrix. This **transformation matrix** consists of two submatrices, as they are, the _rotation matrix_ and the _translation matrix_.
Note, that the extrinsic camera paramters depend on the intrinsic camera calibration, which means that the intrinsic calibration needs to be performed in advance (see [Intrinsic Calibration](http://code.google.com/p/parsley/wiki/IntrinsicCalibration)).

For the scanning process it is obligatory to define at least two reference planes of the test setup. If necessary also the ground plane of the test setup can be defined as a reference plane, e.g. if you use a **MarkerPositioner**. Therefore, we need to perform the extrinsic calibration for every reference plane we would like to define (these planes define the **reference body**).

After the extrinsic calibration has been performed, the cameras position relative to the test setup must not change anymore. Otherwise the calibration has to be repeated.

## Extrinsic calibration settings in Parsley ##
Similar to the intrinsic calibration, the _PatternEditor_ can be used in order to create the needed calibration patterns. As a reasonable pattern for the extrinsic calibration one can choose a circle pattern with the following parameters (depending on the wall sizes of your Parsley hardware setup):
  * Circle diameter: 40 mm
  * Circle distance in X direction: 200 mm
  * Circle distance in Y direction: 120 mm
  * Pattern size: 2;2

For setting up a positioner plane we recommend to use **CompositePatterns**, since the orientation of the positioner can be tracked using two markers. If one marker is likely to hide behind the test object, especially when the positioners' orientation has been changed, the second marker (different shape than the first marker) is used to find the new pose.

![http://parsley.googlecode.com/svn/trunk/doc/frames/CompositePattern.png](http://parsley.googlecode.com/svn/trunk/doc/frames/CompositePattern.png)

Setting up a _CompositePattern_, with the _PatternEditor_ requires two predefined _MarkerPatterns_ M1 and M2. Furthermore, the matrix coordinate transformation **T<sub>21</sub>** between the coordinate systems 2 and 1, of the two markers, needs to be set up in the editor. With reference to the example image, we find out that the **rotational angle** can be set to 180 degrees (rotational displacement of the axes X<sub>2</sub> and X<sub>1</sub> - observe the orientation of the coordinate system axes, based on the _Right-Hand-Rule_!). For the translation vector we set the vector, expressed in coordinate system 2, describing the position of the **point of origin** of coordinate system 1 (z coordinate is equal to 0).

Using this coordinate transformation, the pose of coordinate system 1 (_main coordinate system_) can be calculated even if only marker 2 can be found. Please find more detailed explanations about _CompositePatterns_ in the source code comments of the class [CompositePattern](http://code.google.com/p/parsley/source/browse/trunk/Parsley.Core.CalibrationPatterns/CompositePattern.cs).

How to set up different patterns with the _PatternEditor_ is described on wiki page [Intrinsic Calibration](http://code.google.com/p/parsley/wiki/IntrinsicCalibration).

## Execution of the extrinsic calibration ##
In order to perform the extrinsic calibration, change to the _Extrinsic Calibration Slide_ found in the _Setup_ tab. Hit the _LiveImage_ button to show a window with the cameras' live image.
After your plane patterns have been placed properly on the test setup, use the _Load Pattern_ button to load the corresponding pattern, you want to use for the extrinsic calibration. Now, the exitrinsic calibration can be performed separately for every reference plane.

Use the computer mouse in the _LiveImage_ window to define a **sub image** to identify, where the pattern (which should be detected for the extrinsic calibration) is located. The sub image is created by holding the left mouse button and dragging the cursor in the image window. If the selected pattern has been successfully detected, the plane error is displayed in the status bar. Note, that the calibration is performed continuously and updated if the plane error is decreasing. Afterwards, use the _Save Extrinsics_ button to save the extrinsic calibration for the chosen plane (watch video how to perform the calibration: [ExtrinsicCalibration](http://www.youtube.com/watch?v=m5p2aJcp1CY)).

If removeable calibration planes are used, observe, that a large plane error is produced if the calibration plane is removed after performing the calibration. In order to avoid this calibration error the numeric field can be used to set a plane displacement, which shifts the detected plane (extrinsic matrix) in Z - direction. For example: If the calibration pattern has a thickness of 4 millimeters, set the numeric field to the value 4 if you intend to remove the calibration plane, after performing the extrinsic calibration. For a precise compensation of this uncertainty, the thickness **t** of the calibration plane needs to be known exactly. The image below, shows the relations.

![http://parsley.googlecode.com/svn/trunk/doc/shiftedCalibrationPlane.png](http://parsley.googlecode.com/svn/trunk/doc/shiftedCalibrationPlane.png)

After the extrinsic calibration has been performed for every reference plane, the extrinsic parameters need to be set for the **Reference Body**, and for the **Positioner** in the _Parsley Options_ menu (_Setup_ tab).

## Troubleshooting and hints regarding the Extrinsic Calibration Process ##
  * Resonable calibration error values are smaller than a value of e = 0.2.
  * In case of too high error values, make sure that the pattern has been configured correctly (dimensions, pattern shape size,...). Furthermore, try to eliminate a possible unevenness of the printed pattern (e.g. glue the printed patterns on a stable surface like a stiff plastic plate).
  * Avoid any percussion during the calibration process, because this might lead to a changing position of the camera relative to the reference body (repetition of calibration will be necessary).
  * Try to adjust the cameras focusing settings using [AMCap](http://www.noeld.com/programs.asp?cat=video#AMCap), so that both - scanning object and the reference planes - are well focused. Note, that the [Intrinsic Calibration](http://code.google.com/p/parsley/wiki/IntrinsicCalibration) needs to be performed again after changing the camera focus.