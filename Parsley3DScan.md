# Introduction #
After the Parsley Options and _ScanWorkflow_ parameters have been adjusted (see [ParsleySetup](http://code.google.com/p/parsley/wiki/ParsleyOptionsMenu)), a 3D object scan can be performed.

# How to perform a 3D object scan #
When switching to the _Record_ tab, four **main functionalities** of the Parsley _ScanWorkflow_ can be recognized:
  * _Update Positioner Transformation_: By activating this button, the **transformation matrix**, which is needed to transform the captured 3D points to the correct location in the camera coordinate system, will be updated according to the new orientation of the positioner. Therefore, this function must be activated everytime after the positioners' orientation has changed. Otherwise, the scanned 3D points will not appear at the correct position.
  * _Take Texture Image_: Updates the color of the 3D points, which have been captured from the current view so far, using a texture image of the object. We recommend to update the texture everytime after the **transformation matrix** have been updated successfully.
  * _Clear Points_: Deletes the points which are stored in the pointcloud.
  * _Save Points:_ Saves the 3D points (stored in the pointcloud) to the specified file, including position data and the color of the point. These values are separated by the space character.

Before starting the 3D scan, open the _Live Image_ window and the _3D Image_ window. The scanned points will be displayed in the _3D Image_ window. Use the mouse cursor in the _3D image_ window to change the view: Left button + dragging the mouse ==> rotate the view; Right button + dragging the mouse ==> zoom the view.

Firstly, we need to update the **transformation matrix** by activating the button _Update Positioner Transformation_ (see class [MarkerPositioner](http://code.google.com/p/parsley/source/browse/trunk/Parsley.Core.BuildingBlocks/MarkerPositioner.cs) for details regarding the calculation algorithm). Since a **Marker Positioner** is used, the current position  and orientation of the positioner will be found automatically. Observe, that if both markers of _CompositePattern_ are hidden behind the object, i.e. there is no intervisibility given between camera and pattern, the matrix transformation cannot be calculated. Always watch the messages which are displayed in the **status bar** of the Parsley main window. By clicking on the status bar once, the logger window will be opened, showing the log messages.

Secondly, the texture should be taken in order to show the points in the correct color. If no texture has been taken, the points are shown in the standard color white. Note, that the texture can also been taken after the scanning in the current view (positioner orientation), since the point color will be updated.

Thirdly, make sure that the **region of interest** (ROI) is set properly. Navigate to the ROI property in the _Setup_ menu (section _ScanWorkflow_) and select the property. Then use the computer mouse to define a rectangle in the _Live Image_ window. Note, that only points which are located within the ROI are captured during the 3D scan.

Fourthly, use your laser (with a ray expansion lense ==> shape of a line) and sway the laser line accross the ROI area, slowly. Make sure that the included angle between laser plane and camera plane is greater than the _MinimumAngle_ which is set in the _Setup_ menu, otherwise the detected laser plane will be rejected and no points are extracted.
A green drawn laser line indicates that object points are currently captured. These points will be updated in the _3D Image_ window.

Finally, if you have scanned enough laser points from this point of view, change the positioners' orientation and repeat the steps, which are described above. If your 3D scan is finished hit the button _Save Points_ to save your scanning results.

It is worth mentioning that there is no necessity to scan the whole object at once. Try to store the resulting pointclouds of different point of views separately and continue scanning by deleting the pointcloud before the next scan, with the changed positioner orientation, is started. With the help of a visualization software, these pointcloud fragments can be concatenated and the resulting 3D object can be displayed. The open source software _Para View_ (see [ParaView](http://www.paraview.org)) can display different pointclouds using _Data Table to Point filter_.