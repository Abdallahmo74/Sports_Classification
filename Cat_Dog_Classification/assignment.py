
import cv2
import os
import pandas as pd
import numpy as np
import random
from skimage.feature import hog
from sklearn import svm
from sklearn.model_selection import train_test_split
j = 0
training_path = 'E:\Photos'
categories =["Cat" , "Dog"]
category = []
imgNames = []
feats = []
for classs in categories:
 if 'Dog' == classs:
     lable = 1
 else:
     lable = 0
 path = os.path.join(training_path , classs)

 for train in os.listdir(path):
        imgNames.append(train)
        category.append(lable)
        img = cv2.imread(os.path.join(path, train))
        resized = cv2.resize(img, (128, 64))
        fd, hog_img = hog(resized, orientations=9, pixels_per_cell=(8, 8), cells_per_block=(2, 2), visualize=True, multichannel=True)

        feats.append(fd)

        j += 1
        if j == 2200:
             break

x = feats
y = category
Xtrain , Xtest , Ytrain , Ytest = train_test_split(x , y, test_size= 0.1 , random_state=False )

C = 0.1
svc = svm.SVC(kernel='poly',degree = 2 ,  C=C).fit(Xtrain, Ytrain)
predictions = svc.predict(Xtrain)
accTrain = np.mean(predictions == Ytrain)
testPred = svc.predict(Xtest)
accTest = np.mean(testPred == Ytest)
print("testing Accuracy : " + str(accTest))
print("Training Accuracy : " + str(accTrain))
