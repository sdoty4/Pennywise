# Pennywise

PennySplit takes one image of 100 pennies and splits it into 100 images of one penny.

Methods of determining if a penny is rotated correctly...

RotateUp1 methodology: The area to the right of Lincoln's nose if a smooth with no raised elements. RotateUp1 checked a square area of pixels in this region of the penny for minimum difference in brightness.

RotateUp2 methodology: The word LIBERTY appears to the left of Lincoln just below the middle of the penny. The raised letters of the word will cause a wide variance in the grayscale brightness of pixels. RotateUp2 checks this area of the penny for a maximum brightness difference.

RotateUp3 methodology: The year of the penny is in front of Lincoln slighly below the LIBERTY text. This version adds the LIBERTY variance to the YEAR variance.

RotateUp4 methodology: On both sides of Lincoln's head is an area that should not have very much varience in brightness because there is no image or words. This version takes RotateUp3 variance and subtracts two stips of pixels on either side of the penny.  If there is a large variance in brightness then the penny may not be rotated correctly and a large number is subtracted from the total. 

RotateUp5 methodology: This is the same as RotateUp4 except that three stips of pixels are used. This is done because the penny may not be centered exactly in the center of the image. The theory is that multiple stips of pixels will catch the variance in the location of the penny in the image.

RESULTS...

Stats of various rotation attempts on 100 pennies of sheet 1:
RotateUp1: 54% of 100 rotated correctly
RotateUp2: 21% of 100 rotated correctly
RotateUp3: 70% of 100 rotated correctly
RotateUp4: 74% of 100 rotated correctly
RotateUp5: 82% of 100 rotated correctly

-------------------------------------------------------------------

PennyIdentify is an application that displays a penny and allows the user to enter the year.
The year is associated with the image file of the penny.
