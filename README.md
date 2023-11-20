 ## Create points on selected face and export them to text file
When we need a point set of a surface for testing purposes, we can create it within NX CADCAM and then export the points to a txt or csv file. This NX Open script is designed to create a point set on an NX surface with just a few clicks, as demonstrated in the video.

https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/Creating%20points.mp4

The coordinates of the points will be stored in a txt file "exported points.txt", the file
<br/>
## How it works
1. The operation "create point set on face" with NX CADCAM was recorded by NX journal. Some simplification is necessary for it to be run later.
  <p align="center" height="180">
  <img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/create%20pt%20on%20face.jpg height="200">
  </p>

 
<br/>

2. After points were created along the U, V directions of the selected untrimmed face (as per the NX default setting), we can observe some points filled in the trimmed area, we will calculate the distance to the face of each point, if the distance is greater than 0.001 mm, the point will be skipped when exporting to a text file.
<p align="center">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/check%20dist.jpg height="200">
</p>


The NXOpen.UF.Modeling.AskMinimumDist function is used to determine the distance from a point to a face.
<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/uf%20used.jpg height="120" >
</p>
## Some info from points

3. Some basic operations with the point set.




4. Max and min adjacent distance measurement of a point set, comparing NX versus KDTree calculation.
