 ## Create points on selected face and export them to text file
 ## Create points on selected face and export them to text file
When we need a point set of a surface for testing purposes, we can create it within NX CADCAM and then export the points to a txt or csv file. This NX Open script is designed to create a point set on an NX surface with just a few clicks, as demonstrated in the video.

The coordinates of the points will be stored in a txt file "exported points.txt", which will be created in the same folder as the NX part file.
<br/>
## How it works
1. Open NX and create point set on face, the process was recorded with NX journal, some simplification is necessary to get it ready to run later.
  <p align="center" height="180">
  <img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/create%20pt%20on%20face.jpg height="200">
  </p>

 
<br/>

2. In NX, the point set created along U,V direction of the selected un_trimed face, we can find some points are there within trimed area, next we will calculate the distance to face of each point, if the distance is larger than 0.001 mm, the point will be skipped when export to text file.
<p align="center">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/check%20dist.jpg height="200">
</p>


In this app, use NXOpen.UF.Modeling.AskMinimumDist 
<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/uf%20used.jpg height="120" >
</p>
