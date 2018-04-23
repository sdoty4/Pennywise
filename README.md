# Pennywise

PennySplit takes one image of 100 pennies and splits it into 100 images of one penny.

Methods of determining if a penny is rotated correctly...

RotateUp1 methodology: The area to the right of Lincoln's nose if a smooth with no raised elements. RotateUp1 checked a square area of pixels in this region of the penny for minimum difference in brightness.

RotateUp2 methodology: The word LIBERTY appears to the left of Lincoln just below the middle of the penny. The raised letters of the word will cause a wide variance in the grayscale brightness of pixels. RotateUp2 checks this area of the penny for a maximum brightness difference.

RotateUp3 methodology: The year of the penny is in front of Lincoln slighly below the LIBERTY text

Stats of various rotation attempts on 100 pennies of sheet 1:
RotateUp1: 54% of 100 rotated correctly
RotateUp2: 21% of 100 rotated correctly
RotateUp3: 70% of 100 rotated correctly
RotateUp4: 74% of 100 rotated correctly
RotateUp5: 82% of 100 rotated correctly



PennyIdentify is an application that displays a penny and allows the user to enter the year.
The year is associated with the image file of the penny.
