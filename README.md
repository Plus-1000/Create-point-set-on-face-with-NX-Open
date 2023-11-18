 ## Create points on selected face and export them to text file
Some time you need a point clould of an object for testing porpose, you will have to get ready the camera and components then proceed.  With this app you may first create solid model or face with NX CADCAM, then generate point set on face and exported their coordinate to a text file.

The steps was automated with this app, select face and input the parameters, the point set file will be send to NX part file location with name "exported points.txt"
<br/>
## How it works
1. Open NX and create point set on face, the process was recorded with NX journal, some simplification is necessary to get it ready to run later.
<p align="left">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/create%20pt%20on%20face.jpg height="180" align=center>
</p>

 
<br/>

2. In NX, the point set created along U,V direction of the selected un_trimed face, we can find some points are there within trimed area, next we will calculate the distance to face of each point, if the distance is larger than 0.001 mm, the point will be skipped when export to text file.
<p align="left">
<img src="https://github.com/Plus-1000/Create-point-set-on-face-and-export-to-txt/blob/main/image-2.png" height="180" align=center>
</p>


In this app, use NXOpen.UF.Modeling.AskMinimumDist 
<br/>
<p align="left">
<img src="https://github.com/Plus-1000/Create-point-set-on-face-and-export-to-txt/blob/main/image-3.png" height="120" align=center>
</p>
