# Introduction #
Parsley and it's third-party libraries need to operate on different coordinate frames. Therefore, coordination and mapping between those coordinate frames is crucial. The following components in Parsley manipulate coordinate frames
  * _Parsley.Core_
  * _Parsley.Draw3D_
  * _Emgu_
  * _OpenCV_
  * _System.Drawing_
  * _OpenSceneGraph_

The following coordinate systems will apply to all components, unless otherwise noted.
Parsley uses [right-handed](http://en.wikipedia.org/wiki/Right-handed_coordinate_system) coordinate systems. When expressed as matrix, _Parsley.Core_|_Emgu_|_OpenCV_ store basis vectors in columns. In the case of homogeneous coordinates, the translation is therefore found in the fourth column. _OpenSceneGraph_ stores basis vectors in rows, which requires a matrix [transposition](http://en.wikipedia.org/wiki/Transpose) to convert to matrices expressed in the components _Parsley.Core_|_Emgu_|_OpenCV_. This transposition is currently carried out implicitly by routines of _Parsley.Draw3D_.

# Image Coordinate System #
The image coordinate system is a two-dimensional coordinate system. The units are in pixels. The origin is in the top-left corner, positive x-axis points to the right and the positive y-axis points downward.

![http://parsley.googlecode.com/svn/trunk/doc/frames/ImageCS.png](http://parsley.googlecode.com/svn/trunk/doc/frames/ImageCS.png)

When accessing images in _System.Drawing_|_Emgu_ using the `[]` operator, the access pattern is `[row,column]` which corresponds to coordinates `[y,x]` in the image coordinate system.

# Camera Coordinate System #
The camera coordinate system is a three-dimensional coordinate system. The units are millimeters. Its origin lies in the projection-center of the pinhole camera model. The x-axis and y-axis run parallel to the x-axis and y-axis of the [image coordinate system](CoordinateSystems#Image_Coordinate_System.md). The view direction corresponds to the positive z-axis. _OpenSceneGraph_ uses the negative z-axis as view direction, which requires a rotation to fix.

![http://parsley.googlecode.com/svn/trunk/doc/frames/CameraCS.png](http://parsley.googlecode.com/svn/trunk/doc/frames/CameraCS.png)

The principal axis is a straight line through the projection center and orthogonal to the image plane. The principal point is the intersection of the principal axis and the image plane. The units of the principal point are pixels. The focal length is the distance of the center of projection to the principal point measured along the principal axis. In _OpenCV_ the units are pixels and given for the x-direction and y-direction separately since pixels need not to be square. Focal length and principal points are stored in the [intrinsic calibration matrix](http://opencv.willowgarage.com/documentation/camera_calibration_and_3d_reconstruction.html).

# Chessboard Coordinate System #
The chessboard coordinate system is a three-dimensional coordinate system. The units are in millimeters.
When viewed from the top, so that the top-left field is black, then the x-axis points to the right, the y-axis points downwards and the z-axis points into the pattern.  The origin lies in the plane defined by the chessboard pattern and is fixed to the first inner corner measured along the described axes.

![http://parsley.googlecode.com/svn/trunk/doc/frames/ChessboardCS.png](http://parsley.googlecode.com/svn/trunk/doc/frames/ChessboardCS.png)

The chessboard coordinate system can be found by estimating a transformation that aligns the corner points expressed in the chessboard coordinate system with corner points detected in the image coordinate system. This calibration is called extrinsic calibration.

# Circle Pattern Coordinate System #
Another pattern which can be used for intrinsic or extrinsic camera calibration is the circle pattern. Since the circles turn into ellipses, because of perspective distortion, when the pattern is viewed from different camera positions, a robust ellipse detection algorithm is needed in order to achieve proper pattern detection.
The correct position of the coordinate systems' origin can be found by sorting the ellipses in an rotation-invariant, unique order. Based on the white markers - located in the center of the ellipses - the number of white-black transitions, which are registered when following an ellipse shaped path within the ellipse, can be used to rank the detected ellipses in descending order.

![http://parsley.googlecode.com/svn/trunk/doc/frames/CircleCS.png](http://parsley.googlecode.com/svn/trunk/doc/frames/CircleCS.png)

Finally, the coordinate system can be fit into the center of the ellipse, which contains the marker with the highest number of white-black transitions. With reference to the given pattern example, the coordinate system is located in the center point of the ellipse, next to the top-left corner of the pattern. The orientation of the coordinate axes corresponds to the orientation of the chessboard coordinate system.

# Marker Pattern Coordinate System #
Marker tracking is based on known square shapes, which consist of a thick black boarder and a non-symmetric black/white coloured interior. Hereby, the pattern which should be found in the camera frame is provided as an ordinary png-image.

![http://parsley.googlecode.com/svn/trunk/doc/frames/MarkerCS.png](http://parsley.googlecode.com/svn/trunk/doc/frames/MarkerCS.png)

In order to find a possible marker, the camera image needs to be binarized and inverted, firstly. Secondly, a contour finding algorithm is used to extract contour points at black/white boarders in the image. After fitting polynom lines through the contours, only poly-lines containing four vertices can be treated as potential markers.
Using a perspective transformation (EmguCV algorithm), the pixels, located inside of the possible marker boarders (poly-lines), can be projected to a square image with the same size as the given marker reference image. This is done for every possible marker.
Finally, the warped image can be matched with the marker image, considering four different orientation possibilities (0°, 90°, 180°, 270°) of the markers' interior shape.
If a best fitting position can be found, which means that the normed sum of squared pixel intensity differences is less than a given error value, the corner points of the marker can be ordered according to the found orientation.

The advantage of marker detection is, that only one marker is needed in order to find the orientation of the pattern. Especially for object tracking, like it is needed to find the current orientation of a marker based positioner, using these patterns has turned out to be very useful.

# Composite Pattern Coordinate System #
_CompositePatterns_ currently consist of two **sub patterns**, which have been defined in advance. This pattern type is mainly used as positioner pattern, because its orientation can also be found if one sub pattern is hidden e.g. behind the scanning object.

Additionally to the sub patterns, the matrix transformation T<sub>12</sub> (rotational and translational component) between the two coordinate systems of the sub patterns is known. One of the sub patterns is declared as **main sub pattern**, and its coordinate system (defined as coordinate system 1) is considered as the coordinate system of the _CompositePattern_. Therefore the _ObjectPoints_ (e.g. corner points of marker patterns, center points of ellipses - defining the position of the point of origin of the coordinate system) of the _CompositePattern_ are equal to the _ObjectPoints_ of the _main sub pattern_. The image below shows the relations.

![http://parsley.googlecode.com/svn/trunk/doc/frames/CompositePattern.png](http://parsley.googlecode.com/svn/trunk/doc/frames/CompositePattern.png)

In order to detect the _CompositePattern_, we first try to find the **main sub pattern** by calling the, to the sub pattern type corresponding, function _findPattern_. If the _main sub pattern_ could be found the algorithm is already finished - _findPattern_ returns the corresponding 2D _ImagePoints_.
If only the second sub pattern can be found, the _ObjectPoints_ of the _main sub pattern_ will be transformed into the coordinate system 2, using the transformation matrix _Inverse(T<sub>21</sub>)_. Finally, by finding the **extrinsic matrix** describing the relation between _camera coordinate system_ and _coordinate system 2_, the _transformed ObjectPoints_ of the _main sub pattern_ can be projected into the **2D camera image coordinate system** and again the _ImagePoints_ of the _main sub pattern_ have been found.
Please find more algorithm details in the source code of different calibration patterns (see [CalibrationPatterns](http://code.google.com/p/parsley/source/browse/#svn/trunk/Parsley.Core.CalibrationPatterns)).

Note, that mainly **MarkerPatterns** are used as sub patterns in _CompositePatterns_, since markers allow to find out the orientation of a pattern easily, with a single marker. However, e.g. when using _CirclePatterns_, at least four circles need to be used to find the correct orientation of the pattern with satisfying stability.