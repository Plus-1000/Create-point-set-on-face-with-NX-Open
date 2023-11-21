## Create points on selected face and export them to text file
When we need a point set of a surface for testing purposes, we can create it within NX CADCAM and then export the points to a txt or csv file. This NX Open script is designed to create a point set on a NX surface with just a few clicks, as demonstrated in the video.


<a href="http://www.youtube.com/watch?feature=player_embedded&v=nmtSOpe3bGA 
" target="_blank"><img src="http://img.youtube.com/vi/nmtSOpe3bGA /0.jpg" 
alt="MOVIE" width="240" height="180" border="10" /></a>




The coordinates of the points will be stored in a txt file "exported points.txt", it is in the same folder as NX part file.
<br/>
<br/>
## How it works
1. The operation "create point set on face" with NX CADCAM was recorded by NX journal. Some simplification is necessary for it to be run later.
  <p align="center" height="180">
  <img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/create%20pt%20on%20face.jpg length="150">
  </p>

 
<br/>

2. After points were created along the original U, V grid of the selected face (as per the NX default setting), we can observe some points filled in the trimmed area, we will calculate the distance to the face of each point, if the distance is greater than 0.001 mm, the point will be skipped when exporting to a text file.
<p align="center">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/check%20dist.jpg length="150">
</p>

<br/>
3. The NXOpen.UF.Modeling.AskMinimumDist function is used to determine the distance from a point to a face.
<br/>
<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/Func%20ask%20dist.jpg length="280" >
</p>

<br/>

## Check point set with Python
1. Some basic operations with the point set.

<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/Check%20pts%20with%20Python.jpg length="280" >
</p>

<br/>

2. Max and min adjacent distance measurement of a point set, comparing NX distance check vs KDTree calculation.
<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Create-point-set-on-face-with-NX-Open/blob/main/image/Min%20max%20adjacent%20dist%20check.jpg length="280" >
</p>

<br/>
Thanks to my colleagues, who generously shared their wealth of knowledge, assisted me with kindness, provided support and advice, and helped me debug codes. For your valuable comments, please email me at wjian88@gmail.com, thank you. 
